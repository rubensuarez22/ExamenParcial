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
        private Bitmap bmp;
        private Scene scene;
        private Canvas canvas;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            PCT_CANVAS.Image = bmp;

            
            CHBX_ROTX.CheckedChanged += CHBX_ROTX_CheckedChanged;
            CHBX_ROTY.CheckedChanged += CHBX_ROTY_CheckedChanged;
            CHBX_ROTZ.CheckedChanged += CHBX_ROTZ_CheckedChanged;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string pathToObj = Path.Combine(Application.StartupPath, "sph.obj");
            ObjLoader objLoader = new ObjLoader(pathToObj);
            scene = new Scene(objLoader);

            TIMER.Interval = 16;
            TIMER.Start();
        }


        private void RenderScene()
        {
            if (bmp.Width != PCT_CANVAS.Width || bmp.Height != PCT_CANVAS.Height)
            {
                bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            }

            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                // Asigna el color base o gestiona la lógica para cambiarlo según sea necesario.
                Color colorBase = Color.FromArgb(0, 182, 0);
                scene.Projection(graphics, PCT_CANVAS, colorBase); 
            }

            // Actualiza la imagen del PictureBox con el nuevo bitmap.
            PCT_CANVAS.Image = bmp;
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            scene.Update(); // Actualiza la escena, si es necesario.
            RenderScene(); // Llama al nuevo método de renderizado.
        }


        private void CHBX_ROTX_CheckedChanged(object sender, EventArgs e)
        {
            scene.RotateXEnabled = CHBX_ROTX.Checked;
            RenderScene();
        }

        private void CHBX_ROTY_CheckedChanged(object sender, EventArgs e)
        {
            scene.RotateYEnabled = CHBX_ROTY.Checked;
            RenderScene();
        }

        private void CHBX_ROTZ_CheckedChanged(object sender, EventArgs e)
        {

            scene.RotateZEnabled = CHBX_ROTZ.Checked;
            RenderScene();
        }



        private void BTN_1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Antes de la traslación:");
            scene.PrintVerticesPositions();

            // Aplicar una traslación más pequeña
            scene.TranslateAllMeshes(1, 0.5f, 0);

            Console.WriteLine("Después de la traslación:");
            scene.PrintVerticesPositions();
        }

        private void BTN_ESCALAR_Click(object sender, EventArgs e)
        {
                // Aplica un factor de escala a toda la escena
                scene.ScaleAllMeshes(0.9f, 0.9f, 0.9f); // Esto escalará todos los objetos en la escena un 10% más grandes

                RenderScene(); // Asegúrate de volver a renderizar la escena para ver los cambios

        }

    }
}