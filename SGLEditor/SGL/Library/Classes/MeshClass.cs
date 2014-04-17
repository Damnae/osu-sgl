using System;
using System.Collections.Generic;
using SGL.Elements;
using System.IO;
using System.Globalization;

namespace SGL.Library.Classes {
	internal class MeshClass : AbstractClass {
		private readonly String path;

		private List<Vertex> vertices = new List<Vertex>();
		private List<Face> faces = new List<Face>();
		private List<Edge> edges = new List<Edge>();

		// used for class registration
		public MeshClass() {
		}

		private MeshClass(String path) {
			this.path = path;

			Load(path);
		}

		public override string Name {
			get { return "Mesh"; }
		}

		public override object CreateInstance(List<Value> param) {
			if (Value.TypeCompare(param, ValType.String))
				return new MeshClass(param[0].StringValue);
			else
				throw new CompilerException(-1, 312);
		}

		public override Value InvokeMethod(String name, List<Value> param) {
			switch (name) {
				case "getVertexCount":
					return new Value(vertices.Count, ValType.Integer);

				case "getVertex":
					if (Value.TypeCompare(param, ValType.Integer)) {
						return new Value(vertices[param[0].IntValue], ValType.Object);
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				case "getEdgeCount":
					return new Value(edges.Count, ValType.Integer);

				case "getEdge":
					if (Value.TypeCompare(param, ValType.Integer)) {
						return new Value(edges[param[0].IntValue], ValType.Object);
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				case "getFaceCount":
					return new Value(faces.Count, ValType.Integer);

				case "getFace":
					if (Value.TypeCompare(param, ValType.Integer)) {
						return new Value(faces[param[0].IntValue], ValType.Object);
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				default:
					throw new CompilerException(-1, 314, Name, name);
			}
		}

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
							face.VertexIndexes.Add(int.Parse(values[i], CultureInfo.InvariantCulture) - 1);
						faces.Add(face);
					}
				}
			}

			foreach (var face in faces) {
				var previousIndex = -1;
				foreach (var vertexIndex in face.VertexIndexes) {
					if (previousIndex >= 0)
						addEdge(previousIndex, vertexIndex);
					previousIndex = vertexIndex;
				}
				addEdge(previousIndex, face.VertexIndexes[0]);
			}
		}

		protected void addEdge(int v0, int v1) {
			foreach (var edge in edges)
				if ((edge.V0 == v0 && edge.V1 == v1) || (edge.V0 == v1 && edge.V1 == v0))
					return;

			var e = new Edge(this, v0, v1);
			edges.Add(e);
		}

		class Face : AbstractClass {
			private MeshClass mesh;
			protected List<int> vertexIndexes = new List<int>();

			public List<int> VertexIndexes { get { return vertexIndexes; } }

			public Face(MeshClass mesh) {
				this.mesh = mesh;
			}

			public override string Name {
				get { return "Face"; }
			}

			public override Value InvokeMethod(string name, List<Value> param) {
				switch (name) {
					case "getVertexCount":
						return new Value(vertexIndexes.Count, ValType.Integer);

					case "getVertex":
						if (Value.TypeCompare(param, ValType.Integer)) {
							return new Value(mesh.vertices[vertexIndexes[param[0].IntValue]], ValType.Object);
						} else
							throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

					case "getVertexIndex":
						if (Value.TypeCompare(param, ValType.Integer)) {
							return new Value(vertexIndexes[param[0].IntValue], ValType.Integer);
						} else
							throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

					default:
						throw new CompilerException(-1, 314, Name, name);
				}
			}

			public override object CreateInstance(List<Value> parameters) {
				// This should never be called
				throw new NotSupportedException();
			}
		}

		class Edge : AbstractClass {
			private MeshClass mesh;
			protected int v0;
			protected int v1;

			public Edge(MeshClass mesh, int v0, int v1) {
				this.mesh = mesh;
				this.v0 = v0;
				this.v1 = v1;
			}

			public override string Name {
				get { return "Edge"; }
			}

			public int V0 { get { return v0; } }
			public int V1 { get { return v1; } }

			public override Value InvokeMethod(string name, List<Value> parameters) {
				switch (name) {
					case "getV0":
					case "getVertex0":
						return new Value(mesh.vertices[v0], ValType.Object);

					case "getV1":
					case "getVertex1":
						return new Value(mesh.vertices[v1], ValType.Object);

					case "getV0Index":
					case "getVertex0Index":
						return new Value(v0, ValType.Integer);

					case "getV1Index":
					case "getVertex1Index":
						return new Value(v1, ValType.Integer);

					default:
						throw new CompilerException(-1, 314, Name, name);
				}
			}

			public override object CreateInstance(List<Value> parameters) {
				// This should never be called
				throw new NotSupportedException();
			}
		}

		class Vertex : AbstractClass {
			protected double x;
			protected double y;
			protected double z;

			public Vertex(double x, double y, double z) {
				this.x = x;
				this.y = y;
				this.z = z;
			}

			public override string Name {
				get { return "Vertex"; }
			}

			public override Value InvokeMethod(string name, List<Value> parameters) {
				switch (name) {
					case "getX":
						return new Value(x, ValType.Double);

					case "getY":
						return new Value(y, ValType.Double);

					case "getZ":
						return new Value(z, ValType.Double);

					default:
						throw new CompilerException(-1, 314, Name, name);
				}
			}

			public override object CreateInstance(List<Value> parameters) {
				// This should never be called
				throw new NotSupportedException();
			}
		}
	}
}