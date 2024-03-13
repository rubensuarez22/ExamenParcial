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

        private Vertex cameraPosition = new Vertex(new float[] { 0, 1, -1 });
        private Vertex lightPosition = new Vertex(new float[] { 0, 0, -2f });



        public Scene(ObjLoader objLoader)
        {
            Mesh = objLoader.Mesh;
            meshes = new List<Mesh> { Mesh};

        }

        public void Update()
        {
            // Aquí manejarías actualizaciones de la escena, como animaciones o cambios en la rotación
            rotationAngle += 0.01f;
            rotationAngle %= (float)(2 * Math.PI);
        }




        public void Render(Graphics g, int canvasWidth, int canvasHeight, bool renderLines, bool applyFlatShading, bool RotateX, bool RotateY, bool RotateZ)
        {
            float scaleFactor = 100.0f;
            int centerX = canvasWidth / 2;
            int centerY = canvasHeight / 2;

            g.Clear(Color.CornflowerBlue); // Ajusta el color de fondo

            foreach (var mesh in meshes)
            {
                var orderedTriangles = mesh.Triangles.OrderByDescending(tri => tri.Centroid.Values[2]).ToList();

                foreach (var triangle in orderedTriangles)
                {
                    Vertex v1 = triangle.V1;
                    Vertex v2 = triangle.V2;
                    Vertex v3 = triangle.V3;

                    if (RotateX)
                    {
                        v1 = Rotaciones.Rot(rotationAngle, v1, 'x');
                        v2 = Rotaciones.Rot(rotationAngle, v2, 'x');
                        v3 = Rotaciones.Rot(rotationAngle, v3, 'x');
                    }
                    if (RotateY)
                    {
                        v1 = Rotaciones.Rot(rotationAngle, v1, 'y');
                        v2 = Rotaciones.Rot(rotationAngle, v2, 'y');
                        v3 = Rotaciones.Rot(rotationAngle, v3, 'y');
                    }
                    if (RotateZ)
                    {
                        v1 = Rotaciones.Rot(rotationAngle, v1, 'z');
                        v2 = Rotaciones.Rot(rotationAngle, v2, 'z');
                        v3 = Rotaciones.Rot(rotationAngle, v3, 'z');
                    }

                    // Aplica rotación a los vértices del triángulo

                    // Calcula el centroide y la normal del triángulo rotado
                    Vertex centroid = Mesh.CalculateCentroid(v1, v2, v3);
                    Vertex normal = Vertex.CalculateNormal(v1, v2, v3);

                    // Dirección hacia la cámara y normalización
                    Vertex cameraDirection = cameraPosition - centroid;
                    cameraDirection.Normalize();

                    Console.WriteLine($"Centroid: {centroid}");
                    Console.WriteLine($"Normal: {normal}");
                    Console.WriteLine($"Direction to Camera (normalized): {cameraDirection}");
                    Console.WriteLine($"Dot Product (for back-face culling): {Vertex.Dot(normal, cameraDirection)}");

                    if (renderLines)
                    {
                        // Dibuja solo las líneas de los triángulos
                        Point p1 = ScaleAndCenter(v1, centerX, centerY, scaleFactor);
                        Point p2 = ScaleAndCenter(v2, centerX, centerY, scaleFactor);
                        Point p3 = ScaleAndCenter(v3, centerX, centerY, scaleFactor);

                        g.DrawLine(Pens.White, p1, p2);
                        g.DrawLine(Pens.White, p2, p3);
                        g.DrawLine(Pens.White, p3, p1);
                    }
                    else if (applyFlatShading)
                    {


                        if (Vertex.Dot(normal, cameraDirection) < 0)
                        {
                            // Calcula la dirección de la luz y normalízala
                            Vertex lightDir = lightPosition - centroid;
                            lightDir.Normalize();

                            Console.WriteLine($"Normalized Light Direction: {lightDir}");
                            Console.WriteLine($"Dot Product (light intensity): {Vertex.Dot(normal, lightDir)}");

                            // Calcula la intensidad de la luz y ajusta el color en función de esta
                            float lightIntensity = Math.Max(0, Vertex.Dot(normal, lightDir));
                            Color faceColor = CalculateColor(normal, lightDir, Color.White);

                            Point p1 = ScaleAndCenter(v1, centerX, centerY, scaleFactor);
                            Point p2 = ScaleAndCenter(v2, centerX, centerY, scaleFactor);
                            Point p3 = ScaleAndCenter(v3, centerX, centerY, scaleFactor);

                            Console.WriteLine($"Drawing Triangle: p1: {p1}, p2: {p2}, p3: {p3}, Color: {faceColor}");

                            using (Brush brush = new SolidBrush(faceColor))
                            {
                                g.FillPolygon(brush, new[] { p1, p2, p3 });
                            }
                        }
                    }
                }
            }
        }



        private Point ScaleAndCenter(Vertex vertex, int centerX, int centerY, float scale)
        {
            int x = (int)(vertex.Values[0] * scale + centerX);
            int y = (int)(vertex.Values[1] * scale + centerY);
            return new Point(x, y);
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


