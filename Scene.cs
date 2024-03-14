using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenParcial
{
    public class Scene
    {
        public Mesh Mesh { get; private set; }
        public List<Tuple<int, int, int>> Indices { get; set; }

        private List<Mesh> meshes;
        private float rotationAngle;
        public float RotationAngle => rotationAngle; // Propiedad de solo lectura en Scene

        private Vertex cameraPosition = new Vertex(new float[] { 0, 0, -1 });
        private Vertex lightPosition = new Vertex(new float[] { -1, -1, -1f });

        // En Scene.cs
        private bool rotatingX = false;
        private bool rotatingY = false;
        private bool rotatingZ = false;

        public void SetRotatingX(bool state) { rotatingX = state; }
        public void SetRotatingY(bool state) { rotatingY = state; }
        public void SetRotatingZ(bool state) { rotatingZ = state; }

        public void Update()
        {
            float angle = 0.5f; // Este es el ángulo de rotación en grados, ajusta según necesites
            if (rotatingX) RotateAllMeshes(angle, 0, 0);
            if (rotatingY) RotateAllMeshes(0, angle, 0);
            if (rotatingZ) RotateAllMeshes(0, 0, angle);
        }



        public void RotateMesh(float angleX, float angleY, float angleZ, Mesh mesh)
        {
            // Conversión de ángulos de grados a radianes
            float radX = angleX * (float)Math.PI / 180f;
            float radY = angleY * (float)Math.PI / 180f;
            float radZ = angleZ * (float)Math.PI / 180f;

            // Pre-cálculo de los senos y cosenos para cada eje
            float cosX = (float)Math.Cos(radX), sinX = (float)Math.Sin(radX);
            float cosY = (float)Math.Cos(radY), sinY = (float)Math.Sin(radY);
            float cosZ = (float)Math.Cos(radZ), sinZ = (float)Math.Sin(radZ);

            // Asumimos que centerX, centerY, centerZ representan el centro de la malla correctamente calculado
            float centerX = mesh.Vertices.Average(v => v.Values[0]);
            float centerY = mesh.Vertices.Average(v => v.Values[1]);
            float centerZ = mesh.Vertices.Average(v => v.Values[2]);

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Vertex vertex = mesh.Vertices[i];
                // Traslación de vértices al origen para rotación
                float x = vertex.Values[0] - centerX;
                float y = vertex.Values[1] - centerY;
                float z = vertex.Values[2] - centerZ;

                // Aplicar rotación en X
                float dy = y * cosX - z * sinX;
                float dz = y * sinX + z * cosX;

                // Aplicar rotación en Y
                float dx = x * cosY + dz * sinY;
                dz = dz * cosY - x * sinY;

                // Aplicar rotación en Z
                x = dx * cosZ - dy * sinZ;
                y = dx * sinZ + dy * cosZ;

                // Traslada los vértices rotados de
                x += centerX;
                y += centerY;
                z += centerZ;

                // Actualiza el vértice en la malla con las nuevas coordenadas
                mesh.Vertices[i] = new Vertex(new float[] { x, y, z });
            }
        }



        public Scene(ObjLoader objLoader)
        {
            Mesh = objLoader.Mesh;
            meshes = new List<Mesh> { Mesh};

        }

        public void drawTriangle(Graphics graphics, PointF[] points, Color color)
        {
            //
            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics.FillPolygon(brush, points);
            }
            // Dibujando el contorno después de rellenar el polígono asegura que el contorno sea visible
            using (Brush brush = new SolidBrush(color))
            {
                graphics.FillPolygon(brush, points);
            }
        }


        public void Projection(Graphics graphics, PictureBox pictureBox, Color colorBase)
        {
            graphics.Clear(Color.Black);
            colorBase = Color.FromArgb(255, 0, 0);

            // Utiliza el vector de luz actualizado y lo normaliza
            Vertex luz = new Vertex(new float[] 
            { 
                lightPosition.Values[0], 
                lightPosition.Values[1], 
                lightPosition.Values[2] 
            });

            luz.Normalize();

            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;

            // Asumiendo que tienes una manera de obtener todos los vértices de tus mallas
            foreach (var mesh in meshes)
            {
                foreach (var vertex in mesh.Vertices)
                {
                    minX = Math.Min(minX, vertex.Values[0]);
                    minY = Math.Min(minY, vertex.Values[1]);
                    maxX = Math.Max(maxX, vertex.Values[0]);
                    maxY = Math.Max(maxY, vertex.Values[1]);
                }
            }

            float scaleX = pictureBox.Width / (maxX - minX);
            float scaleY = pictureBox.Height / (maxY - minY);
            float scale = Math.Min(scaleX, scaleY) * 0.9f;
            float offsetX = (pictureBox.Width - (maxX + minX) * scale) / 2;
            float offsetY = (pictureBox.Height - (maxY + minY) * scale) / 2;

            foreach (var mesh in meshes)
            {
                foreach (var face in mesh.Triangles)
                {
                    PointF[] points = new PointF[3];
                    for (int i = 0; i < 3; i++)
                    {
                        Vertex vertex = (i == 0) ? face.V1 : (i == 1) ? face.V2 : face.V3;
                        var x = vertex.Values[0] * scale + offsetX;
                        var y = pictureBox.Height - (vertex.Values[1] * scale + offsetY); // Ajuste de coordenadas Y
                        points[i] = new PointF(x, y);
                    }

                    // Lógica para culling de caras traseras y shading simplificado
                    var edge1 = new PointF(points[1].X - points[0].X, points[1].Y - points[0].Y);
                    var edge2 = new PointF(points[2].X - points[1].X, points[2].Y - points[1].Y);
                    var crossProduct = edge1.X * edge2.Y - edge1.Y * edge2.X;

                    if (crossProduct > 0) // Condición simple para culling de caras traseras
                    {
                        Vertex normal = Vertex.CalculateNormal(face.V1, face.V2, face.V3);
                        float d = Math.Max(0, Math.Min(1, Vertex.Dot(normal, luz)));

                        Color colorFinal = Color.FromArgb(
                            (int)(colorBase.R * d),
                            (int)(colorBase.G * d),
                            (int)(colorBase.B * d));

                        drawTriangle(graphics, points, colorFinal);
                    }
                }
            }
        }




        public void RotateAllMeshes(float angleX, float angleY, float angleZ)
        {
            foreach (var mesh in meshes)
            {
                RotateMesh(angleX, angleY, angleZ, mesh);
            }
        }



        public void RotateX(float angle)
        {
            foreach (var mesh in meshes)
            {
                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    mesh.Vertices[i] = Rotaciones.Rot(angle, mesh.Vertices[i], 'x');
                }
            }
        }

        public void RotateY(float angle)
        {
            foreach (var mesh in meshes)
            {
                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    mesh.Vertices[i] = Rotaciones.Rot(angle, mesh.Vertices[i], 'y');
                }
            }
        }

        public void RotateZ(float angle)
        {
            foreach (var mesh in meshes)
            {
                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    mesh.Vertices[i] = Rotaciones.Rot(angle, mesh.Vertices[i], 'z');
                }
            }
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }
        public Color CalculateColor(Vertex normal, Vertex lightDirection, Color objectColor)
        {
            float intensity = Math.Max(0, Vertex.Dot(normal, lightDirection));
            intensity = Clamp(intensity, 0, 1);
            // Ajusta el color del objeto en función de la intensidad de la luz
            int r = (int)(objectColor.R * intensity);
            int g = (int)(objectColor.G * intensity);
            int b = (int)(objectColor.B * intensity);

            // Clampa los valores de color para que estén entre 0 y 255
            r = Math.Min(255, Math.Max(0, r));
            g = Math.Min(255, Math.Max(0, g));
            b = Math.Min(255, Math.Max(0, b));

            return Color.FromArgb(objectColor.A, r, g, b);
        }
    }
   




}


