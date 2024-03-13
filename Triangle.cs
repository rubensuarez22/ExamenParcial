using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenParcial
{
    public class Triangle
    {
        public Vertex V1 { get; }
        public Vertex V2 { get; }
        public Vertex V3 { get; }
        public Vertex Normal { get; }
        public Vertex Centroid { get; }

        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
            Normal = Vertex.CalculateNormal(v1, v2, v3);
            Centroid = CalculateCentroid(v1, v2, v3);
        }

        private static Vertex CalculateCentroid(Vertex a, Vertex b, Vertex c)
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
