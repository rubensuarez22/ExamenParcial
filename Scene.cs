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

        public List<Mesh> meshes;



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

            // Utiliza el vector de luz actualizado y lo normaliza
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

        public void RotateY(Mesh mesh, float degrees)
        {
            float radians = degrees * (float)Math.PI / 180f;
            foreach (var vertex in mesh.Vertices)
            {
                var rotatedVertex = Rotaciones.Rot(radians, vertex, 'y');
                vertex.Values[0] = rotatedVertex.Values[0];
                vertex.Values[1] = rotatedVertex.Values[1];
                vertex.Values[2] = rotatedVertex.Values[2];
            }
        }

        public void RotateZ(Mesh mesh, float degrees)
        {
            float radians = degrees * (float)Math.PI / 180f;
            foreach (var vertex in mesh.Vertices)
            {
                var rotatedVertex = Rotaciones.Rot(radians, vertex, 'z');
                vertex.Values[0] = rotatedVertex.Values[0];
                vertex.Values[1] = rotatedVertex.Values[1];
                vertex.Values[2] = rotatedVertex.Values[2];
            }
        }


        //Debug
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



    }






}
