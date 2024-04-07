using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExamenParcial.Mesh;

namespace ExamenParcial
{
    public class Mesh
    {
        public List<Vertex> Vertices { get; set; }
        public List<Triangle> Triangles { get; set; }
        public List<Tuple<int, int, int>> Indices { get; private set; }

        public Mesh(List<Vertex> vertices, List<Tuple<int, int, int>> indices)
        {
            Vertices = vertices;
            Indices = indices;
            Triangles = new List<Triangle>();
            foreach (var index in indices)
            {
                Triangles.Add(new Triangle(vertices[index.Item1], vertices[index.Item2], vertices[index.Item3]));
            }
        }




        public static Vertex CalculateCentroid(Vertex a, Vertex b, Vertex c)
        {
            return new Vertex(new float[]
            {
            (a.Values[0] + b.Values[0] + c.Values[0]) / 3,
            (a.Values[1] + b.Values[1] + c.Values[1]) / 3,
            (a.Values[2] + b.Values[2] + c.Values[2]) / 3
            });
        }
    }
}
    
  


