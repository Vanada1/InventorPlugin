using Services;
using System.Windows;

namespace CommonTestClass
{
	/// <summary>
	/// Тестовый сервис API.
	/// </summary>
	public class TestApiService : IApiService
	{
		/// <summary>
		/// Флаг создания документа.
		/// </summary>
		public bool IsCreateDocument { get; private set; } = false;

		/// <summary>
		/// Флаг создания точки.
		/// </summary>
		public bool IsCreatePoint { get; private set; } = false;

		/// <summary>
		/// Флаг создания эскиза.
		/// </summary>
		public bool IsCreateNewSketch { get; private set; } = false;

		/// <summary>
		/// Флаг выдавливания.
		/// </summary>
		public bool IsExtrude { get; private set; } = false;

		/// <inheritdoc/>
		public double Unit => 1.0;

		/// <inheritdoc/>
		public void CreateDocument()
		{
			IsCreateDocument = true;
		}

		/// <inheritdoc/>
		public Point CreatePoint(double x, double y)
		{
			IsCreatePoint = true;
			return new Point(x, y);
		}

		/// <inheritdoc/>
		public ISketch CreateNewSketch(int n, double offset)
		{
			IsCreateNewSketch = true;
			return new TestSketch();
		}

		/// <inheritdoc/>
		public void Extrude(ISketch sketch, double distance)
		{
			IsExtrude = true;
		}
	}
}