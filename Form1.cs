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
            canvas = new Canvas(bmp, scene);

            TIMER.Interval = 16; 
            TIMER.Tick += TIMER_Tick;
            TIMER.Start();
        }


        private void CHBX_ROTX_CheckedChanged(object sender, EventArgs e)
        {
            canvas.RotateX = CHBX_ROTX.Checked;
            canvas.Render(); // Asume que tienes un método para forzar el renderizado
        }

        private void CHBX_ROTY_CheckedChanged(object sender, EventArgs e)
        {
            canvas.RotateY = CHBX_ROTY.Checked;
            canvas.Render();
        }

        private void CHBX_ROTZ_CheckedChanged(object sender, EventArgs e)
        {
            canvas.RotateZ = CHBX_ROTZ.Checked;
            canvas.Render();
        }

        private void CHBX_LINES_CheckedChanged(object sender, EventArgs e)
        {
            canvas.RenderLines = CHBX_LINES.Checked;
            canvas.Render();
        }

        private void CHBX_COLOR_CheckedChanged(object sender, EventArgs e)
        {
            canvas.ApplyFlatShading = CHBX_COLOR.Checked;
            canvas.Render();
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            if (bmp.Width != PCT_CANVAS.Width || bmp.Height != PCT_CANVAS.Height)
            {
                bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
                canvas.SetBitmap(bmp);
            }
            scene.Update();
            canvas.Render();
            PCT_CANVAS.Image = bmp;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}