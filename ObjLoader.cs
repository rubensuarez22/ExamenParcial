using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ExamenParcial
{
    public class ObjLoader
    {
        public List<Vertex> Vertices { get; private set; }
        public List<Tuple<int, int, int>> Faces { get; private set; }

        public ObjLoader(string path)
        {
            Vertices = new List<Vertex>();
            Faces = new List<Tuple<int, int, int>>();
            Load(path);
        }

        private void Load(string path)
        {
            // Aquí asumiremos que tu archivo obj está en el mismo formato y no necesita tratamiento de errores complejo.
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (line.StartsWith("v "))
                {
                    string[] vertexData = line.Substring(2).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    float x = float.Parse(vertexData[0], CultureInfo.InvariantCulture);
                    float y = float.Parse(vertexData[1], CultureInfo.InvariantCulture);
                    float z = float.Parse(vertexData[2], CultureInfo.InvariantCulture);
                    Vertices.Add(new Vertex(new float[] { x, y, z }));
                }
                else if (line.StartsWith("f "))
                {
                    string[] faceData = line.Substring(2).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int vertex1 = int.Parse(faceData[0].Split('/')[0]) - 1; // .obj indices are 1-based
                    int vertex2 = int.Parse(faceData[1].Split('/')[0]) - 1;
                    int vertex3 = int.Parse(faceData[2].Split('/')[0]) - 1;
                    Faces.Add(new Tuple<int, int, int>(vertex1, vertex2, vertex3));
                }
            }
        }
    }
}
