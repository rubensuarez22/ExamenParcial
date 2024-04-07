using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenParcial
{
    public class Mtx
    {
        const int X = 0;
        const int Y = 1;
        const int Z = 2;
        private float[,] Mat;


        public float this[int x, int y]
        {
            get { return Mat[x, y]; }
            set { Mat[x, y] = value; }
        }

        public Mtx(float[,] Mat)
        {
            this.Mat = Mat;
        }

        public Vertex Mul(Vertex vector)
        {
            Vertex pts;

            pts = new Vertex(new float[3]);

            pts[X] = (this[X, X] * vector[X]) + (this[Y, X] * vector[Y]) + (this[Z, X] * vector[Z]);
            pts[Y] = (this[X, Y] * vector[X]) + (this[Y, Y] * vector[Y]) + (this[Z, Y] * vector[Z]);
            pts[Z] = (this[X, Z] * vector[X]) + (this[Y, Z] * vector[Y]) + (this[Z, Z] * vector[Z]);


            return pts;
        }
    }
}
