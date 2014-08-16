using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;

namespace SGL.Framework {
	public class Mesh {
		private List<Face> faces = new List<Face>();
		private List<Edge> edges = new List<Edge>();
		private List<Vertex> vertices = new List<Vertex>();

		public Mesh(String path) {
			Load(path);
		}

		public List<Face> Faces { get { return faces; } }
		public List<Edge> Edges { get { return edges; } }
		public List<Vertex> Vertices { get { return vertices; } }

		public void Load(String path) {
			using (var streamReader = new StreamReader(path)) {
				while (!streamReader.EndOfStream) {
					var line = streamReader.ReadLine().Trim();
					var values = line.Split(' ');

					if (values[0].Equals("v")) {
						// Not sure why y and z are swapped here
						var x = double.Parse(values[1], CultureInfo.InvariantCulture);
						var y = -double.Parse(values[3], CultureInfo.InvariantCulture);
						var z = double.Parse(values[2], CultureInfo.InvariantCulture);
						vertices.Add(new Vertex(x, y, z));

					} else if (values[0].Equals("f")) {
						var face = new Face(this);
						for (var i = 1; i < values.Length; ++i)
							face.VertexIndices.Add(int.Parse(values[i], CultureInfo.InvariantCulture) - 1);
						faces.Add(face);
					}
				}
			}

			foreach (var face in faces) {
				var previousIndex = -1;
				foreach (var vertexIndex in face.VertexIndices) {
					if (previousIndex >= 0)
						addEdge(previousIndex, vertexIndex);
					previousIndex = vertexIndex;
				}
				addEdge(previousIndex, face.VertexIndices[0]);
			}
		}

		protected void addEdge(int v0, int v1) {
			foreach (var edge in edges)
				if ((edge.V0Index == v0 && edge.V1Index == v1) || (edge.V0Index == v1 && edge.V1Index == v0))
					return;

			edges.Add(new Edge(this, v0, v1));
		}

		public class Face {
			private Mesh mesh;
			protected List<int> vertexIndices = new List<int>();

			public Face(Mesh mesh) {
				this.mesh = mesh;
			}

			public List<int> VertexIndices { get { return vertexIndices; } }

			public Vertex getVertex(int index) {
				return mesh.vertices[vertexIndices[index]];
			}
		}

		public class Edge {
			private Mesh mesh;
			protected int v0;
			protected int v1;

			public Edge(Mesh mesh, int v0, int v1) {
				this.mesh = mesh;
				this.v0 = v0;
				this.v1 = v1;
			}

			public int V0Index { get { return v0; } }
			public int V1Index { get { return v1; } }
			public Vertex V0 { get { return mesh.vertices[v0]; } }
			public Vertex V1 { get { return mesh.vertices[v1]; } }
		}

		public class Vertex {
			protected double x;
			protected double y;
			protected double z;

			public Vertex(double x, double y, double z) {
				this.x = x;
				this.y = y;
				this.z = z;
			}

			public double X { get { return x; } }
			public double Y { get { return y; } }
			public double Z { get { return z; } }
		}
	}
}
