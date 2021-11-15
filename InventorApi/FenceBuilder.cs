using System.Collections.Generic;
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
			var middleStickY = fenceHeight - 2 * columnWidth;
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
				_inventorWrapper.TransientGeometry.CreatePoint2d(columnWidth, middleStickY),
				_inventorWrapper.TransientGeometry.CreatePoint2d(fenceLength - columnWidth, middleStickY - columnWidth),
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

			var sketchProfile = sketchXy.Profiles.AddForSolid();

			var extrudeDef =
				_inventorWrapper.PartDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(sketchProfile,
					PartFeatureOperationEnum.kJoinOperation);
			extrudeDef.SetDistanceExtent(columnWidth, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
			var extrude = _inventorWrapper.PartDefinition.Features.ExtrudeFeatures.Add(extrudeDef);
			var objectCollection = _inventorWrapper.CreateObjectCollection();
			objectCollection.Add(extrude);
		}

		/// <summary>
		/// Построить нижнюю часть забора.
		/// </summary>
		private void BuildLowerInnerPart()
		{

		}

		/// <summary>
		/// Построить верхнюю часть забора.
		/// </summary>
		private void BuildUpperInnerPart()
		{

		}

		#endregion
	}
}