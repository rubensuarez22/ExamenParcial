using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamenParcial
{
    public class Vertex
    {
        const int X = 0; 
        const int Y = 1; 
        const int Z = 2; 
        public float[] Values;

        public float this[int i]
        {
            get { return Values[i]; }
            set { Values[i] = value; }
        }

        public Vertex(float[] values)
        {
            this.Values = values;
        }

        public static Vertex operator *(Vertex a, Vertex b) { 
            return new Vertex(new float[] { a[X] * b[X], a[Y] * b[Y], a[Z] * b[Z] }); 
        }
        public static Vertex operator +(Vertex a, Vertex b) { 
            return new Vertex(new float[] { a[X] + b[X], a[Y] + b[Y], a[Z] + b[Z] }); 
        }

        public static Vertex operator -(Vertex a, Vertex b)
        {
            return new Vertex(new float[] { a.Values[0] - b.Values[0], a.Values[1] - b.Values[1], a.Values[2] - b.Values[2] });
        }

        public static Vertex operator /(Vertex v, float scalar)
        {
            return new Vertex(new float[] {
                v.Values[0] / scalar, v.Values[1] / scalar, v.Values[2] / scalar });
        }
        public override string ToString() { 
            return this[X] + ", " + this[Y] + ", " + this[Z] + " "; 
        }


        public static Vertex CalculateNormal(Vertex a, Vertex b, Vertex c)
        {
            Vertex edge1 = b - a;
            Vertex edge2 = c - a;
            Vertex normal = Vertex.Cross(edge1, edge2);

            normal.Normalize(); // Asegura que la normal es un vector unitario
            return normal;
        }

        public void Normalize()
        {
            float mag = Magnitude();
                Values[0] /= mag;
                Values[1] /= mag;
                Values[2] /= mag;
        }
        public static float Dot(Vertex a, Vertex b)
        {
            return a.Values[0] * b.Values[0] + a.Values[1] * b.Values[1] + a.Values[2] * b.Values[2];
        }


        public static Vertex Cross(Vertex a, Vertex b)
        {
            return new Vertex(new float[]
            {
                a.Values[1] * b.Values[2] - a.Values[2] * b.Values[1],
                a.Values[2] * b.Values[0] - a.Values[0] * b.Values[2],
                a.Values[0] * b.Values[1] - a.Values[1] * b.Values[0]
            });
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(Values[0] * Values[0] + Values[1] * Values[1] + Values[2] * Values[2]);
        }


    }
}


