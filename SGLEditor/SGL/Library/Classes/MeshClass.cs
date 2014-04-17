using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SGL.Elements;
using SGL.Framework;

namespace SGL.Library.Classes {
	internal class MeshClass : AbstractClass {
		private Mesh mesh;

		// used for class registration
		public MeshClass() {
		}

		private MeshClass(String path) {
			mesh = new Mesh(path);
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
					return new Value(mesh.Vertices.Count, ValType.Integer);

				case "getVertex":
					if (Value.TypeCompare(param, ValType.Integer)) {
						return new Value(new VertexClass(mesh.Vertices[param[0].IntValue]), ValType.Object);
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				case "getEdgeCount":
					return new Value(mesh.Edges.Count, ValType.Integer);

				case "getEdge":
					if (Value.TypeCompare(param, ValType.Integer)) {
						return new Value(new EdgeClass(mesh.Edges[param[0].IntValue]), ValType.Object);
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				case "getFaceCount":
					return new Value(mesh.Faces.Count, ValType.Integer);

				case "getFace":
					if (Value.TypeCompare(param, ValType.Integer)) {
						return new Value(new FaceClass(mesh.Faces[param[0].IntValue]), ValType.Object);
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				default:
					throw new CompilerException(-1, 314, Name, name);
			}
		}

		public class FaceClass : AbstractClass {
			private Mesh.Face face;

			public FaceClass(Mesh.Face face) {
				this.face = face;
			}

			public override string Name {
				get { return "Face"; }
			}

			public override Value InvokeMethod(string name, List<Value> param) {
				switch (name) {
					case "getVertexCount":
						return new Value(face.VertexIndices.Count, ValType.Integer);

					case "getVertex":
						if (Value.TypeCompare(param, ValType.Integer)) {
							return new Value(new VertexClass(face.getVertex(param[0].IntValue)), ValType.Object);
						} else
							throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

					case "getVertexIndex":
						if (Value.TypeCompare(param, ValType.Integer)) {
							return new Value(face.VertexIndices[param[0].IntValue], ValType.Integer);
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

		public class EdgeClass : AbstractClass {
			private Mesh.Edge edge;

			public EdgeClass(Mesh.Edge edge) {
				this.edge = edge;
			}

			public override string Name {
				get { return "Edge"; }
			}

			public override Value InvokeMethod(string name, List<Value> parameters) {
				switch (name) {
					case "getV0":
					case "getVertex0":
						return new Value(new VertexClass(edge.V0), ValType.Object);

					case "getV1":
					case "getVertex1":
						return new Value(new VertexClass(edge.V1), ValType.Object);

					case "getV0Index":
					case "getVertex0Index":
						return new Value(edge.V0Index, ValType.Integer);

					case "getV1Index":
					case "getVertex1Index":
						return new Value(edge.V0Index, ValType.Integer);

					default:
						throw new CompilerException(-1, 314, Name, name);
				}
			}

			public override object CreateInstance(List<Value> parameters) {
				// This should never be called
				throw new NotSupportedException();
			}
		}

		public class VertexClass : AbstractClass {
			private Mesh.Vertex vertex;

			public VertexClass(Mesh.Vertex vertex) {
				this.vertex = vertex;
			}

			public override string Name {
				get { return "Vertex"; }
			}

			public override Value InvokeMethod(string name, List<Value> parameters) {
				switch (name) {
					case "getX":
						return new Value(vertex.X, ValType.Double);

					case "getY":
						return new Value(vertex.Y, ValType.Double);

					case "getZ":
						return new Value(vertex.Z, ValType.Double);

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