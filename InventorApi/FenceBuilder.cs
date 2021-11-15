﻿using System.Collections.Generic;
using Core;
using Inventor;

namespace InventorApi
{
	/// <summary>
	/// Класс для создания забора.
	/// </summary>
	public class FenceBuilder
	{
		#region -- Fields --

		/// <summary>
		/// Экземпляр класса для работы с Inventor — <see cref="InventorWrapper"/>.
		/// </summary>
		private readonly InventorWrapper _inventorWrapper;

		/// <summary>
		/// Параметры забора.
		/// </summary>
		private readonly FenceParameters _fenceParameters;

		#endregion

		#region -- Properties --

		/// <summary>
		/// Координата средней палки.
		/// </summary>
		private double MiddleStickY => _fenceParameters.FenceHeight - 2 * _fenceParameters.ColumnWidth;

		#endregion

		#region -- Constructors --

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="fenceParameters"></param>
		public FenceBuilder(FenceParameters fenceParameters)
		{
			_fenceParameters = fenceParameters;
			_inventorWrapper = new InventorWrapper();
		}

		#endregion

		#region -- Public Methods --

		/// <summary>
		/// Построить забор.
		/// </summary>
		public void BuildFence()
		{
			_inventorWrapper.CreateNewDocument();
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
			var columnWidth = _fenceParameters.ColumnWidth;
			var fenceLength = _fenceParameters.FenceLength;
			var fenceHeight = _fenceParameters.FenceHeight;
			var immersionDepth = _fenceParameters.ImmersionDepth;
			var points = new List<Point2d>
			{
				_inventorWrapper.TransientGeometry.CreatePoint2d(0, 0),
				_inventorWrapper.TransientGeometry.CreatePoint2d(columnWidth, fenceHeight),
				_inventorWrapper.TransientGeometry.CreatePoint2d(fenceLength, 0),
				_inventorWrapper.TransientGeometry.CreatePoint2d(fenceLength - columnWidth, fenceHeight),
				_inventorWrapper.TransientGeometry.CreatePoint2d(columnWidth, fenceHeight - columnWidth),
				_inventorWrapper.TransientGeometry.CreatePoint2d(columnWidth, immersionDepth),
				_inventorWrapper.TransientGeometry.CreatePoint2d(fenceLength - columnWidth,
					immersionDepth + columnWidth),
				_inventorWrapper.TransientGeometry.CreatePoint2d(columnWidth, MiddleStickY),
				_inventorWrapper.TransientGeometry.CreatePoint2d(fenceLength - columnWidth, MiddleStickY - columnWidth),
			};

			var sketchXy = _inventorWrapper.MakeNewSketch(1, 0);

			var rectangles = new List<SketchEntitiesEnumerator>
			{
				sketchXy.SketchLines.AddAsTwoPointRectangle(points[0],points[1]),
				sketchXy.SketchLines.AddAsTwoPointRectangle(points[2],points[3]),
				sketchXy.SketchLines.AddAsTwoPointRectangle(points[3],points[4]),
				sketchXy.SketchLines.AddAsTwoPointRectangle(points[5],points[6]),
				sketchXy.SketchLines.AddAsTwoPointRectangle(points[7],points[8]),
			};

			Extrude(sketchXy);
		}

		/// <summary>
		/// Выдавливание.
		/// </summary>
		/// <param name="sketch">Эскиз.</param>
		private void Extrude(PlanarSketch sketch)
		{
			var sketchProfile = sketch.Profiles.AddForSolid();

			var extrudeDef =
				_inventorWrapper.PartDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(sketchProfile,
					PartFeatureOperationEnum.kJoinOperation);
			extrudeDef.SetDistanceExtent(_fenceParameters.ColumnWidth, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
			var extrude = _inventorWrapper.PartDefinition.Features.ExtrudeFeatures.Add(extrudeDef);
			var objectCollection = _inventorWrapper.CreateObjectCollection();
			objectCollection.Add(extrude);
		}

		/// <summary>
		/// Построить нижнюю часть забора.
		/// </summary>
		private void BuildLowerInnerPart()
		{
			var columnWidth = _fenceParameters.ColumnWidth;
			var fenceHeight = _fenceParameters.FenceHeight;
			var immersionDepth = _fenceParameters.ImmersionDepth;
			var y1 = immersionDepth + columnWidth;
			var y2 = MiddleStickY - columnWidth;

			BuildInnerPart(y1, y2, _fenceParameters.DistanceLowerBaffles);
		}

		/// <summary>
		/// Построить верхнюю часть забора.
		/// </summary>
		private void BuildUpperInnerPart()
		{
			var columnWidth = _fenceParameters.ColumnWidth;
			var fenceHeight = _fenceParameters.FenceHeight;
			var y1 = MiddleStickY;
			var y2 = fenceHeight - columnWidth;

			BuildInnerPart(y1, y2, _fenceParameters.DistanceUpperBaffles);
		}

		/// <summary>
		/// Создает внутренние верхние и нижние части забора.
		/// </summary>
		/// <param name="y1">Первое начальное положение по Y.</param>
		/// <param name="y2">Второе начальное положение по Y.</param>
		/// <param name="distance">Расстояние между прутьями.</param>
		private void BuildInnerPart(double y1, double y2, double distance)
		{
			var columnWidth = _fenceParameters.ColumnWidth;
			var fenceLength = _fenceParameters.FenceLength;
			var deltaX = columnWidth + distance;

			var currentPoint1 = _inventorWrapper.TransientGeometry.CreatePoint2d(deltaX, y1);
			var currentPoint2 = _inventorWrapper.TransientGeometry.CreatePoint2d(deltaX + columnWidth, y2);
			var sketchXy = _inventorWrapper.MakeNewSketch(1, 0);
			var rectangles = new List<SketchEntitiesEnumerator>();

			while (fenceLength - columnWidth - currentPoint2.X > columnWidth)
			{
				rectangles.Add(sketchXy.SketchLines.AddAsTwoPointRectangle(currentPoint1,
					currentPoint2));
				currentPoint1 =
					_inventorWrapper.TransientGeometry.CreatePoint2d(currentPoint2.X + distance,
						currentPoint1.Y);
				currentPoint2 =
					_inventorWrapper.TransientGeometry.CreatePoint2d(currentPoint1.X + columnWidth,
						currentPoint2.Y);
			}

			Extrude(sketchXy);
		}

		#endregion
	}
}