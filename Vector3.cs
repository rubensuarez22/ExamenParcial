using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenParcial
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Normalize()
        {
            float mag = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vector3(X / mag, Y / mag, Z / mag);
        }

        public Vertex ToVertex()
        {
            return new Vertex(new float[] { X, Y, Z });

        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
    }
}

