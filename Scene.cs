using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenParcial
{
    public class Scene
    {
        public List<Model> Models { get; set; }
        public Vertex LightPos { get; set; }

        public Scene(List<Model> models, Vertex lightPos)
        {
            Models = models;
            LightPos = lightPos;
        }

        public void Render(Graphics g)
        {
            // Implement rendering logic here
        }
    }
}
