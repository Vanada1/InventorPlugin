using System.Windows;
using Kompas6API5;
using Kompas6Constants3D;
using Services;

namespace KompasApi
{
	/// <summary>
	/// Класс эскиза Компас 3D.
	/// </summary>
	public class KompasSketch : ISketch
	{
		/// <summary>
		/// 2D документ.
		/// </summary>
		private ksDocument2D _document2D;

		/// <summary>
		/// Определенны эскиз.
		/// </summary>
		private readonly ksSketchDefinition _sketchDefinition;

		/// <summary>
		/// Возвращает эскиз.
		/// </summary>
		public ksEntity Sketch { get; }

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="part"></param>
		public KompasSketch(ksPart part)
		{
			ksEntity plane = part.GetDefaultEntity((int)Obj3dType.o3d_planeXOZ);
			Sketch = part.NewEntity((int)Obj3dType.o3d_sketch);
			_sketchDefinition = Sketch.GetDefinition();
			_sketchDefinition.SetPlane(plane);
			Sketch.Create();
			_document2D = _sketchDefinition.BeginEdit();
		}

		public void EndEdit()
		{
			_sketchDefinition.EndEdit();
		}

		/// <inheritdoc/>
		public void CreateTwoPointRectangle(Point point1, Point point2)
		{
			_document2D.ksLineSeg(point1.X, -point1.Y, point2.X, -point1.Y, 1);
			_document2D.ksLineSeg(point2.X, -point1.Y, point2.X, -point2.Y, 1);
			_document2D.ksLineSeg(point1.X, -point2.Y, point2.X, -point2.Y, 1);
			_document2D.ksLineSeg(point1.X, -point1.Y, point1.X, -point2.Y, 1);
		}
	}
}