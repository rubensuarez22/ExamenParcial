using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ExamenParcial
{
    public partial class Form1 : Form
    {
        private Canvas canvas;
        private Bitmap bmp;
        private Timer renderTimer;
        private ObjLoader loader; // Esta variable mantiene tus vértices y caras cargadas
        private float rotationAngle = 0.0f;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            var scene = InitializeScene(); // Suponiendo que tienes un método que prepara y devuelve la escena
            canvas = new Canvas(bmp, scene);
            string pathToObj = Path.Combine(Application.StartupPath, "sph.obj");
            loader = new ObjLoader(pathToObj);
        }
        private Scene InitializeScene()
        {
            // Aquí deberías construir tu escena, modelos, mallas, etc.
            // Este es solo un ejemplo esquelético.
            var vertices = new List<Vertex>
    {
        new Vertex(new float[] { 100, 100, 0 }),
        new Vertex(new float[] { 200, 100, 0 }),
        new Vertex(new float[] { 150, 200, 0 })
    };
            var indices = new List<Tuple<int, int, int>> { Tuple.Create(0, 1, 2) };
            var mesh = new Mesh(vertices, indices);
            var model = new Model(new List<Mesh> { mesh });
            var scene = new Scene(new List<Model> { model }, new Vertex(new float[] { 0, 1, -1 }));
            return scene;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Suponiendo que ya tienes las clases Vertex, Mesh, Model, y Scene definidas.
            var vertices = new List<Vertex>
        {
            new Vertex(new float[] { 100, 100, 0 }),
            new Vertex(new float[] { 200, 100, 0 }),
            new Vertex(new float[] { 150, 200, 0 })
        };
            var indices = new List<Tuple<int, int, int>> { Tuple.Create(0, 1, 2) };
            var mesh = new Mesh(vertices, indices);
            var model = new Model(new List<Mesh> { mesh });
            var scene = new Scene(new List<Model> { model }, new Vertex(new float[] { 0, 1, -1 }));

            canvas = new Canvas(bmp, scene);

            TIMER.Start();
        }


        private void TIMER_Tick(object sender, EventArgs e)
        {
            rotationAngle += 0.01f;

            if (PCT_CANVAS.Width > 0 && PCT_CANVAS.Height > 0)
            {
                bmp?.Dispose(); // Dispose of the old bitmap
                bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.CornflowerBlue); // Fondo azul

                    // Asegúrate de que loadedObj está inicializado y tiene datos válidos
                    if (loader != null)
                    {
                        Rotaciones rotaciones = new Rotaciones();

                        foreach (var face in loader.Faces)
                        {
                            // Aplica las transformaciones y luego la proyección
                            Vertex v1 = loader.Vertices[face.Item1];
                            Vertex v2 = loader.Vertices[face.Item2];
                            Vertex v3 = loader.Vertices[face.Item3];

                            // Rota los vértices
                            Vertex rotatedV1 = rotaciones.Rot(rotationAngle, v1, 'x');
                            Vertex rotatedV2 = rotaciones.Rot(rotationAngle, v2, 'x');
                            Vertex rotatedV3 = rotaciones.Rot(rotationAngle, v3, 'x');

                            // Proyecta los vértices en 2D
                            Point p1 = PerspectiveProject(rotatedV1);
                            Point p2 = PerspectiveProject(rotatedV2);
                            Point p3 = PerspectiveProject(rotatedV3);

                            // Dibuja el triángulo con los vértices proyectados
                            g.DrawLine(Pens.White, p1, p2);
                            g.DrawLine(Pens.White, p2, p3);
                            g.DrawLine(Pens.White, p3, p1);
                        }
                    }
                }
                PCT_CANVAS.Image = bmp; // Esto actualizará tu PictureBox con el nuevo bitmap
            }
        }

        private Point PerspectiveProject(Vertex vertex)
        {
            // Ajusta estos valores para controlar la perspectiva
            float focalLength = 100.0f; // Prueba con diferentes valores para encontrar uno adecuado para tu modelo.
            float z = vertex.Values[2] == 0 ? 0.001f : vertex.Values[2];

            // Asegúrate de que el rango de z de tus vértices no sea demasiado pequeño,
            // de lo contrario, podrías obtener una proyección extremadamente distorsionada.
            float x = (vertex.Values[0] * focalLength) / z + (PCT_CANVAS.Width / 2);
            float y = (vertex.Values[1] * focalLength) / z + (PCT_CANVAS.Height / 2);

            return new Point((int)x, (int)y);
        }



    }

}