using Core;
using Services;
using System.Collections.Generic;

namespace Builder
{

	//TODO: Попробовать отделить от Inventor-a (+)
	/// <summary>
	/// Класс для создания забора.
	/// </summary>
	public class FenceBuilder : IBuildFenceService
	{

		#region -- Fields --

		/// <summary>
		/// Экземпляр класса для работы с API.
		/// </summary>
		private IApiService _apiService;

		/// <summary>
		/// Параметры забора.
		/// </summary>
		private FenceParameters _fenceParameters;

		#endregion

		#region -- Properties --

		/// <summary>
		/// Возвращает координату средней палки в мм.
		/// </summary>
		private double MiddleStickY => 0.75 * _fenceParameters.TopFenceHeight / _apiService.Unit
									   + _fenceParameters.ImmersionDepth / _apiService.Unit;

		/// <summary>
		/// Возвращает значение ширины столбика в мм.
		/// </summary>
		private double ColumnWidth => _fenceParameters.ColumnWidth / _apiService.Unit;

		/// <summary>
		/// Возвращает значение расстояния между нижними перегородками в мм. 
		/// </summary>
		private double DistanceLowerBaffles => _fenceParameters.DistanceLowerBaffles / _apiService.Unit;

		/// <summary>
		/// Возвращает значение расстояния между верхними перегородками в мм. 
		/// </summary>
		private double DistanceUpperBaffles => _fenceParameters.DistanceUpperBaffles / _apiService.Unit;

		/// <summary>
		/// Возвращает значение высоты забора в мм.
		/// </summary>
		private double FenceLength => _fenceParameters.FenceLength / _apiService.Unit;

		/// <summary>
		/// Возвращает значение глубины погружения в мм.
		/// </summary>
		private double ImmersionDepth => _fenceParameters.ImmersionDepth / _apiService.Unit;

		/// <summary>
		/// Возвращает значение высоты верхней части забора в мм.
		/// </summary>
		private double TopFenceHeight => _fenceParameters.TopFenceHeight / _apiService.Unit;

		/// <summary>
		/// Возвращает общую высоту забора в мм.
		/// </summary>
		private double FenceHeight => TopFenceHeight + ImmersionDepth;

		#endregion

		#region -- Constructors --

		/// <summary>
		/// Конструктор.
		/// </summary>
		public FenceBuilder() { }

		#endregion

		#region -- Public Methods --

		/// <inheritdoc/>
		public void BuildFence(FenceParameters fenceParameters, IApiService apiService)
		{
			_apiService = apiService;
			_fenceParameters = fenceParameters;
			_apiService.CreateDocument();
			BuildCarcass();
			BuildLowerInnerPart();
			BuildUpperInnerPart();
		}

		#endregion

		#region -- Private Methods --

		/// <summary>
		/// Построить каркас забора.
		/// </summary>
		private void BuildCarcass()
		{
			var columnWidth = ColumnWidth;
			var fenceLength = FenceLength;
			var fenceHeight = FenceHeight;
			var immersionDepth = ImmersionDepth;
			var points = new List<System.Windows.Point>
			{
				_apiService.CreatePoint(0, 0),
				_apiService.CreatePoint(columnWidth, fenceHeight),
				_apiService.CreatePoint(fenceLength, 0),
				_apiService.CreatePoint(fenceLength - columnWidth, fenceHeight),
				_apiService.CreatePoint(columnWidth, fenceHeight - columnWidth),
				_apiService.CreatePoint(columnWidth, immersionDepth),
				_apiService.CreatePoint(fenceLength - columnWidth,
					immersionDepth + columnWidth),
				_apiService.CreatePoint(columnWidth, MiddleStickY),
				_apiService.CreatePoint(fenceLength - columnWidth,
					MiddleStickY - columnWidth),
			};

			var sketchXy = _apiService.CreateNewSketch(3, 0);

			sketchXy.CreateTwoPointRectangle(points[0], points[1]);
			sketchXy.CreateTwoPointRectangle(points[2], points[3]);
			sketchXy.CreateTwoPointRectangle(points[3], points[4]);
			sketchXy.CreateTwoPointRectangle(points[5], points[6]);
			sketchXy.CreateTwoPointRectangle(points[7], points[8]);

			_apiService.Extrude(sketchXy, ColumnWidth);
		}

		/// <summary>
		/// Построить нижнюю часть забора.
		/// </summary>
		private void BuildLowerInnerPart()
		{
			var columnWidth = ColumnWidth;
			var fenceHeight = FenceHeight;
			var immersionDepth = ImmersionDepth;
			var y1 = immersionDepth + columnWidth;
			var y2 = MiddleStickY - columnWidth;

			BuildInnerPart(y1, y2, DistanceLowerBaffles);
		}

		/// <summary>
		/// Построить верхнюю часть забора.
		/// </summary>
		private void BuildUpperInnerPart()
		{
			var columnWidth = ColumnWidth;
			var fenceHeight = FenceHeight;
			var y1 = MiddleStickY;
			var y2 = fenceHeight - columnWidth;

			BuildInnerPart(y1, y2, DistanceUpperBaffles);
		}

		/// <summary>
		/// Создает внутренние верхние и нижние части забора.
		/// </summary>
		/// <param name="y1">Первое начальное положение по Y.</param>
		/// <param name="y2">Второе начальное положение по Y.</param>
		/// <param name="distance">Расстояние между прутьями.</param>
		private void BuildInnerPart(double y1, double y2, double distance)
		{
			var columnWidth = ColumnWidth;
			var fenceLength = FenceLength;
			var deltaX = columnWidth + distance;

			var currentPoint1 = _apiService.CreatePoint(deltaX, y1);
			var currentPoint2 = _apiService.CreatePoint(deltaX + columnWidth, y2);
			var sketchXy = _apiService.CreateNewSketch(3, 0);

			while (fenceLength - columnWidth - currentPoint2.X > columnWidth)
			{
				sketchXy.CreateTwoPointRectangle(currentPoint1, currentPoint2);
				currentPoint1 =
					_apiService.CreatePoint(currentPoint2.X + distance,
						currentPoint1.Y);
				currentPoint2 =
					_apiService.CreatePoint(currentPoint1.X + columnWidth,
						currentPoint2.Y);
			}

			_apiService.Extrude(sketchXy, ColumnWidth);
		}

		#endregion
	}
}