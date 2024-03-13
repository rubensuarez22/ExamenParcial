using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace ExamenParcial
{
    public class Canvas
    {
        public Bitmap Bitmap { get; private set; }
        public Scene Scene { get; set; }

        public bool RenderLines { get; set; } = false;
        public bool ApplyFlatShading { get; set; } = false;
        public bool RotateX { get; set; } = false;
        public bool RotateY { get; set; } = false;
        public bool RotateZ { get; set; } = false;

        public void SetBitmap(Bitmap newBitmap)
        {
            Bitmap = newBitmap;
        }
        public Canvas(Bitmap bitmap, Scene scene)
        {
            Bitmap = bitmap;
            Scene = scene;
        }

        public void Render()
        {
            using (var g = Graphics.FromImage(Bitmap))
            {
                g.Clear(Color.Black); // Limpiar el canvas con un color de fondo
                Scene.Render(g, Bitmap.Width, Bitmap.Height, RenderLines, ApplyFlatShading); // Implementar el renderizado de la escena aquí
            }
        }
    }

}

