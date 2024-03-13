using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace ExamenParcial
{
    internal class Canvas
    {
        public Bitmap Bitmap { get; private set; }
        public Scene Scene { get; set; }

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
                Scene.Render(g); // Implementar el renderizado de la escena aquí
            }
        }
    }
}

