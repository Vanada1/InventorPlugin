using Services;
using Inventor;
using Point = System.Windows.Point;

namespace InventorApi
{
	/// <summary>
	/// Класс эскиза для Inventor API.
	/// </summary>
	internal class Sketch : ISketch
	{
		
		/// <summary>
		/// Геометрия.
		/// </summary>
		private readonly TransientGeometry _transientGeometry;

		/// <summary>
		/// Возвращает эскиз.
		/// </summary>
		public PlanarSketch PlanarSketch { get; }

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="planarSketch">Эскиз.</param>
		/// <param name="transientGeometry">Геометрия приложения.</param>
		public Sketch(PlanarSketch planarSketch, TransientGeometry transientGeometry)
		{
			PlanarSketch = planarSketch;
			_transientGeometry = transientGeometry;
		}

		/// <inheritdoc/>
		public void CreateTwoPointRectangle(Point point1, Point point2)
		{
			var newPoint1 = _transientGeometry.CreatePoint2d(point1.X, point1.Y);
			var newPoint2 = _transientGeometry.CreatePoint2d(point2.X, point2.Y);
			PlanarSketch.SketchLines.AddAsTwoPointRectangle(newPoint1, newPoint2);
		}
	}
}