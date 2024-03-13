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
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string pathToObj = Path.Combine(Application.StartupPath, "sph.obj");
            ObjLoader objLoader = new ObjLoader(pathToObj);
            scene = new Scene(objLoader);
            canvas = new Canvas(bmp, scene);

            TIMER.Interval = 16; // Aproximadamente 60 FPS
            TIMER.Tick += TIMER_Tick;
            TIMER.Start();
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
    }
}