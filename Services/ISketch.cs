using System.Windows;

namespace Services
{
	/// <summary>
	/// Интерфейс эскиза.
	/// </summary>
	public interface ISketch
	{
		/// <summary>
		/// Построение на эскизе прямоугольника через две точки.
		/// </summary>
		/// <param name="point1">Первая точка.</param>
		/// <param name="point2">Вторая точка.</param>
		void CreateRectangleByTwoPoint(Point point1, Point point2);
	}
}