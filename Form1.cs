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
            CHBX_LINES.CheckedChanged += CHBX_LINES_CheckedChanged;
            CHBX_COLOR.CheckedChanged += CHBX_COLOR_CheckedChanged;
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
                Color colorBase = Color.FromArgb(255, 182, 203);
                scene.Projection(graphics, PCT_CANVAS, colorBase); // Asegúrate de ajustar la firma de Projection según sea necesario.
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
            if (CHBX_ROTX.Checked)
            {
                // Ejemplo: Rota 5 grados en X. Convierte grados a radianes.
                float angleX = 0.01f;
                scene.RotateAllMeshes(angleX, 0, 0);
            }
            else
            {
                // Si deseas hacer aalgo cuando el checkbox es desmarcado, colócalo aquí.
            }

            RenderScene();
        }

        private void CHBX_ROTY_CheckedChanged(object sender, EventArgs e)
        {
            scene.SetRotatingX(CHBX_ROTX.Checked);
        }

        private void CHBX_ROTZ_CheckedChanged(object sender, EventArgs e)
        {
            canvas.RotateZ = CHBX_ROTZ.Checked;
            RenderScene();
        }

        private void CHBX_LINES_CheckedChanged(object sender, EventArgs e)
        {
            canvas.RenderLines = CHBX_LINES.Checked;
            RenderScene();
        }

        private void CHBX_COLOR_CheckedChanged(object sender, EventArgs e)
        {
            canvas.ApplyFlatShading = CHBX_COLOR.Checked;
            RenderScene();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}