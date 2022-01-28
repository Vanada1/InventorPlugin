using Services;
using System.Windows;

namespace CommonTestClass
{
	/// <summary>
	/// Тестовый класс эскиза.
	/// </summary>
	public class TestSketch : ISketch
	{
		/// <summary>
		/// Флаг создания прямоугольника.
		/// </summary>
		public bool IsCreateTwoPointRectangle { get; private set; } = false;

		/// <inheritdoc/>
		public void CreateTwoPointRectangle(Point point1, Point point2)
		{
			IsCreateTwoPointRectangle = true;
		}
	}
}