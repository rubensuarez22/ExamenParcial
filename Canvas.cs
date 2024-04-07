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
        private Bitmap bitmap;
        private Scene scene;

        // Propiedades para controlar el estado del renderizado, por ejemplo:
        public bool RotateX { get; set; }
        public bool RotateY { get; set; }
        public bool RotateZ { get; set; }
        public bool RenderLines { get; set; }
        public bool ApplyFlatShading { get; set; }

        // Constructor que inicializa el Canvas con la escena
        public Canvas(Scene scene)
        {
            this.scene = scene;
        }

        // Método para actualizar el bitmap basado en el estado actual
        public Bitmap UpdateBitmap(int width, int height)
        {
            // Crear un nuevo bitmap si es necesario
            if (bitmap == null || bitmap.Width != width || bitmap.Height != height)
            {
                bitmap = new Bitmap(width, height);
            }

            // Aquí podrías realizar operaciones preliminares de dibujo,
            // como aplicar transformaciones globales, antes de llamar a Scene.Projection.
            // Este es un buen lugar para gestionar la lógica de rotaciones o escala, por ejemplo.

            // No dibujas directamente aquí, sino que preparas todo para el dibujo.
            // La llamada a Scene.Projection se hará desde el formulario, usando este bitmap.

            return bitmap;
        }

        // Otros métodos útiles para manejar el estado del dibujo, como rotaciones o escala.
    }


}

