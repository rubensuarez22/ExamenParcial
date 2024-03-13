using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenParcial
{
    public class Mesh
    {
        public List<Vertex> Vertices { get; set; }
        public List<Tuple<int, int, int>> Indices { get; set; }

        public Mesh(List<Vertex> vertices, List<Tuple<int, int, int>> indices)
        {
            Vertices = vertices;
            Indices = indices;
        }
    }
}

