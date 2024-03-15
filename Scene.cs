using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace ExamenParcial
{
    public class Scene
    {
        public Mesh Mesh { get; private set; }
        public List<Tuple<int, int, int>> Indices { get; set; }

        public List<Mesh> meshes;
        private float rotationAngle;
        public float RotationAngle => rotationAngle; // Propiedad de solo lectura en Scene

        private Vertex cameraPosition = new Vertex(new float[] { 0, 0, -1 });
        private Vertex lightPosition = new Vertex(new float[] { -1, -1, -1f });

        public bool RotateXEnabled { get; set; } = false;
        public bool RotateYEnabled { get; set; } = false;
        public bool RotateZEnabled { get; set; } = false;

        public bool RenderWireframe { get; set; } = false;



        public void Update()
        {
            float angle = 0.5f; // Ángulo de rotación, ajusta según necesites

            if (RotateXEnabled)
            {
                foreach (var mesh in meshes)
                {
                    RotateX(mesh, angle);
                }
            }
            if (RotateYEnabled)
            {
                foreach (var mesh in meshes)
                {
                    RotateY(mesh, angle);
                }
            }
            if (RotateZEnabled)
            {
                foreach (var mesh in meshes)
                {
                    RotateZ(mesh, angle);
                }
            }
        }



        //

        public Scene(ObjLoader objLoader)
        {
            Mesh = objLoader.Mesh;
            meshes = new List<Mesh> { Mesh };

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

            Vertex luz = new Vertex(new float[]
            {
        lightPosition.Values[0],
        lightPosition.Values[1],
        lightPosition.Values[2]
            });
            luz.Normalize();

            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;

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
                        var y = pictureBox.Height - (vertex.Values[1] * scale + offsetY);
                        points[i] = new PointF(x, y);
                    }

                    var normal = Vertex.CalculateNormal(face.V1, face.V2, face.V3);
                    var d = Vertex.Dot(normal, luz);
                    var intensity = Clamp(d, 0, 1);
                    // Asegura que la intensidad esté entre 0 y 1
                    float clampedIntensity = Math.Max(0, Math.Min(1, intensity));

                    // Calcula los componentes de color finales y los clampa entre 0 y 255
                    int r = (int)(colorBase.R * clampedIntensity);
                    int g = (int)(colorBase.G * clampedIntensity);
                    int b = (int)(colorBase.B * clampedIntensity);

                    r = Math.Max(0, Math.Min(255, r));
                    g = Math.Max(0, Math.Min(255, g));
                    b = Math.Max(0, Math.Min(255, b));

                    var colorFinal = Color.FromArgb(r, g, b);

                    if (RenderWireframe)
                    {
                        // Solo dibuja el wireframe
                        using (Pen pen = new Pen(Color.White))
                        {
                            graphics.DrawPolygon(pen, points);
                        }
                    }
                    else
                    {
                        // Dibuja caras rellenas
                        using (SolidBrush brush = new SolidBrush(colorFinal))
                        {
                            graphics.FillPolygon(brush, points);
                        }
                        // Opcional: Dibuja el contorno de cada cara para mejorar la distinción visual
                        using (Pen pen = new Pen(Color.Black))
                        {
                            graphics.DrawPolygon(pen, points);
                        }
                    }
                }
            }
        }



        public void Rotate(float angleX, float angleY, float angleZ, Mesh mesh)
        {
            float radX = angleX * (float)Math.PI / 180f;
            float radY = angleY * (float)Math.PI / 180f;
            float radZ = angleZ * (float)Math.PI / 180f;

            float cosX = (float)Math.Cos(radX), sinX = (float)Math.Sin(radX);
            float cosY = (float)Math.Cos(radY), sinY = (float)Math.Sin(radY);
            float cosZ = (float)Math.Cos(radZ), sinZ = (float)Math.Sin(radZ);

            float centerX = mesh.Vertices.Average(v => v.Values[0]);
            float centerY = mesh.Vertices.Average(v => v.Values[1]);
            float centerZ = mesh.Vertices.Average(v => v.Values[2]);

            List<Vertex> rotatedVertices = new List<Vertex>();

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                var vertex = mesh.Vertices[i];
                float x = vertex.Values[0] - centerX;
                float y = vertex.Values[1] - centerY;
                float z = vertex.Values[2] - centerZ;

                // Aplicar rotación en X
                float dy = y * cosX - z * sinX;
                float dz = y * sinX + z * cosX;

                // Aplicar rotación en Y
                float newX = x * cosY + dz * sinY;
                dz = dz * cosY - x * sinY;

                // Aplicar rotación en Z
                float newY = newX * cosZ - dy * sinZ;
                newY = newX * sinZ + dy * cosZ; // Esta línea estaba incorrecta, usaba variables sin inicializar

                newX += centerX;
                newY += centerY;
                dz += centerZ;

                rotatedVertices.Add(new Vertex(new float[] { newX, newY, dz }));
            }

            mesh.Vertices = rotatedVertices;
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


        public void TranslateAllMeshes(float deltaX, float deltaY, float deltaZ)
        {
            foreach (var mesh in meshes)
            {
                TranslateMesh(deltaX, deltaY, deltaZ, mesh);
            }

        }

        private void TranslateMesh(float deltaX, float deltaY, float deltaZ, Mesh mesh)
        {
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Vertex vertex = mesh.Vertices[i];
                // Traslación de vértices
                float x = vertex.Values[0] + deltaX;
                float y = vertex.Values[1] + deltaY;
                float z = vertex.Values[2] + deltaZ;

                // Actualiza el vértice en la malla con las nuevas coordenadas
                mesh.Vertices[i] = new Vertex(new float[] { x, y, z });
            }
        }


        public void PrintVerticesPositions()
        {
            Console.WriteLine("Imprimiendo posiciones de vértices:");
            foreach (var mesh in meshes)
            {
                foreach (var vertex in mesh.Vertices)
                {
                    Console.WriteLine($"Vértice: X={vertex.Values[0]}, Y={vertex.Values[1]}, Z={vertex.Values[2]}");
                }
            }
            Console.WriteLine("Fin de la impresión de posiciones.");
        }

        public void ScaleAllMeshes(float scaleX, float scaleY, float scaleZ)
        {
            foreach (var mesh in meshes)
            {
                ScaleMesh(scaleX, scaleY, scaleZ, mesh);
            }
        }

        private void ScaleMesh(float scaleX, float scaleY, float scaleZ, Mesh mesh)
        {
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                Vertex vertex = mesh.Vertices[i];
                // Escalado de vértices
                float x = vertex.Values[0] / scaleX;
                float y = vertex.Values[1] / scaleY;
                float z = vertex.Values[2] / scaleZ;

                // Actualiza el vértice en la malla con las nuevas coordenadas
                mesh.Vertices[i] = new Vertex(new float[] { x, y, z });
            }
        }

        public void Rotate90DegreesYAxis(Mesh mesh)
        {
            float angleY = 90; // Rotar 90 grados en el eje Y
            float radY = angleY * (float)Math.PI / 180f;

            float cosY = (float)Math.Cos(radY), sinY = (float)Math.Sin(radY);

            List<Vertex> rotatedVertices = new List<Vertex>();

            foreach (var vertex in mesh.Vertices)
            {
                float x = vertex.Values[0];
                float z = vertex.Values[2];

                // Aplicar rotación en Y
                float newX = x * cosY - z * sinY;
                float newZ = x * sinY + z * cosY;

                // No cambia el valor de Y
                float newY = vertex.Values[1];

                rotatedVertices.Add(new Vertex(new float[] { newX, newY, newZ }));
            }

            mesh.Vertices = rotatedVertices;
        }

        public void RotateY(Mesh mesh, float degrees)
        {
            float radians = degrees * (float)Math.PI / 180;
            foreach (var vertex in mesh.Vertices)
            {
                float x = vertex.Values[0];
                float z = vertex.Values[2];

                vertex.Values[0] = x * (float)Math.Cos(radians) + z * (float)Math.Sin(radians);
                vertex.Values[2] = -x * (float)Math.Sin(radians) + z * (float)Math.Cos(radians);
            }
        }

        public void RotateX(Mesh mesh, float degrees)
        {
            float radians = degrees * (float)Math.PI / 180f;
            foreach (var vertex in mesh.Vertices)
            {
                var rotatedVertex = Rotaciones.Rot(radians, vertex, 'x');
                vertex.Values[0] = rotatedVertex.Values[0];
                vertex.Values[1] = rotatedVertex.Values[1];
                vertex.Values[2] = rotatedVertex.Values[2];
            }
        }

        public void RotateZ(Mesh mesh, float degrees)
        {
            float radians = degrees * (float)Math.PI / 180;
            foreach (var vertex in mesh.Vertices)
            {
                float x = vertex.Values[0];
                float y = vertex.Values[1];

                vertex.Values[0] = x * (float)Math.Cos(radians) - y * (float)Math.Sin(radians);
                vertex.Values[1] = x * (float)Math.Sin(radians) + y * (float)Math.Cos(radians);
            }
        }   


    }






}
