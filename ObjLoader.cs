using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ExamenParcial
{
    public class ObjLoader
    {
        // Exponer la Mesh en lugar de listas separadas
        public Mesh Mesh { get; private set; }

        public ObjLoader(string path)
        {
            Load(path);
        }

        private void Load(string path)
        {
            var vertices = new List<Vertex>();
            var indices = new List<Tuple<int, int, int>>();

            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (line.StartsWith("v "))
                {
                    // Cargar vértices como antes
                    var vertexData = line.Substring(2).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    float x = float.Parse(vertexData[0], CultureInfo.InvariantCulture);
                    float y = float.Parse(vertexData[1], CultureInfo.InvariantCulture);
                    float z = float.Parse(vertexData[2], CultureInfo.InvariantCulture);
                    vertices.Add(new Vertex(new float[] { x, y, z }));
                }
                else if (line.StartsWith("f "))
                {
                    // Cargar caras como antes
                    var faceData = line.Substring(2).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int v1 = int.Parse(faceData[0].Split('/')[0]) - 1;
                    int v2 = int.Parse(faceData[1].Split('/')[0]) - 1;
                    int v3 = int.Parse(faceData[2].Split('/')[0]) - 1;
                    indices.Add(new Tuple<int, int, int>(v1, v2, v3));
                }
            }

            // Construir la Mesh con los vértices y caras cargadas
            Mesh = new Mesh(vertices, indices);
        }
    }
}
