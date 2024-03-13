using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExamenParcial
{
    public class Scene
    {
        public Mesh Mesh { get; private set; }
        public List<Tuple<int, int, int>> Indices { get; set; }

        private List<Mesh> meshes;
        private float rotationAngle;
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
        }



        public void Render(Graphics g, int canvasWidth, int canvasHeight)
        {
            float scaleFactor = 100.0f;
            int centerX = canvasWidth / 2;
            int centerY = canvasHeight / 2;

            g.Clear(Color.CornflowerBlue); // Ajusta el color de fondo según prefieras

            foreach (var mesh in meshes)
            {
                var orderedTriangles = mesh.Triangles.OrderByDescending(tri => tri.Centroid.Values[2]);

                foreach (var index in Mesh.Indices)
                {
                    Vertex v1 = Rotaciones.Rot(rotationAngle, Mesh.Vertices[index.Item1], 'z'); // Ejemplo de rotación en Z
                    Vertex v2 = Rotaciones.Rot(rotationAngle, Mesh.Vertices[index.Item2], 'z');
                    Vertex v3 = Rotaciones.Rot(rotationAngle, Mesh.Vertices[index.Item3], 'z');
                    // Aplica rotación a los vértices del triángulo
                 
                    // Calcula el centroide y la normal del triángulo rotado
                    Vertex centroid = Mesh.CalculateCentroid(v1, v2, v3);
                    Vertex normal = Vertex.CalculateNormal(v1, v2, v3); // Asegúrate de recalcular la normal después de la rotación

                    // Dirección hacia la cámara y normalización
                    Vertex cameraDirection = cameraPosition - centroid;
                    cameraDirection.Normalize();

                    Console.WriteLine($"Centroid: {centroid}");
                    Console.WriteLine($"Normal: {normal}");
                    Console.WriteLine($"Direction to Camera (normalized): {cameraDirection}");
                    Console.WriteLine($"Dot Product (for back-face culling): {Vertex.Dot(normal, cameraDirection)}");

                    if (Vertex.Dot(normal, cameraDirection) > 0)
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


