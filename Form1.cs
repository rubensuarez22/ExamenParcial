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

 
namespace BASICFORM
{
    public partial class Form1 : Form
    {
        private Bitmap bmp;

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
           
            }

            // Actualiza la imagen del PictureBox con el nuevo bitmap.
            PCT_CANVAS.Image = bmp;
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
       
        }


        private void CHBX_ROTX_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void CHBX_ROTY_CheckedChanged(object sender, EventArgs e)
        {
      
        }

        private void CHBX_ROTZ_CheckedChanged(object sender, EventArgs e)
        {

     
        }



        private void BTN_1_Click(object sender, EventArgs e)
        {

        }

        private void BTN_ESCALAR_Click(object sender, EventArgs e)
        {
            

        }

    }
}