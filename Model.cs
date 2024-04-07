using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenParcial
{
    public class Model
    {
        public List<Mesh> Meshes { get; set; }

        public Model(List<Mesh> meshes)
        {
            Meshes = meshes;
        }
    }
}

