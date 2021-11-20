using System;
using System.Runtime.InteropServices;

namespace InventorApi
{
	using Inventor;

	/// <summary>
	/// Класс для работы с Inventor.
	/// </summary>
	public class InventorWrapper
	{
		#region -- Properties --

		/// <summary>
		/// Ссылка на работу с документацией АПИ.
		/// </summary>
		public PartDocument PartDoc { get; private set; }

		/// <summary>
		/// Ссылка на приложение Inventor.
		/// </summary>
		public Application InvApp { get; private set; }

		/// <summary>
		/// Описание документа.
		/// </summary>
		public PartComponentDefinition PartDefinition { get; private set; }

		/// <summary>
		/// Геометрия приложения.
		/// </summary>
		public TransientGeometry TransientGeometry { get; private set; }

		#endregion

		#region -- Public Methods --

		/// <summary>
		/// Создание нового документа.
		/// </summary>
		public void CreateNewDocument()
		{
			InvApp = null;
			try
			{
				InvApp = (Application)Marshal.GetActiveObject("Inventor.Application");
			}
			catch (COMException)
			{
				try
				{
					//Если не получилось перехватить приложение - выкинется исключение на то,
					//что такого активного приложения нет. Попробуем создать приложение вручную.
					var invAppType = Type.GetTypeFromProgID("Inventor.Application");

					InvApp = (Application)Activator.CreateInstance(invAppType);
					InvApp.Visible = true;
				}
				catch (Exception)
				{
					throw new ApplicationException(@"Не получилось запустить Inventor.");
				}
			}

			// В открытом приложении создаем документ
			PartDoc = (PartDocument)InvApp.Documents.Add
			(DocumentTypeEnum.kPartDocumentObject,
				InvApp.FileManager.GetTemplateFile
				(DocumentTypeEnum.kPartDocumentObject,
					SystemOfMeasureEnum.kMetricSystemOfMeasure));

			// Описание документа
			PartDefinition = PartDoc.ComponentDefinition;
			// Инициализация метода геометрии
			TransientGeometry = InvApp.TransientGeometry; 
		}

		/// <summary>
		/// Создает новый эскиз на рабочей плоскости.
		/// </summary>
		/// <param name="n">1 - ZY; 2 - ZX; 3 - XY.</param>
		/// <param name="offset">Расстояние от поверхности.</param>
		/// <returns>Новый эскиз.</returns>
		public PlanarSketch MakeNewSketch(int n, double offset)
		{
			var mainPlane = PartDefinition.WorkPlanes[n];       
			var offsetPlane = PartDefinition.WorkPlanes.AddByPlaneAndOffset(
				mainPlane, offset, false);
			offsetPlane.Visible = false;
			var sketch = PartDefinition.Sketches.Add(offsetPlane, false);
			return sketch;
		}

        /// <summary>
        /// Выдавливание.
        /// </summary>
        /// <param name="sketch">Эскиз.</param>
        /// <param name="distance">Значение, на которое происходит выдавливание.</param>
        public void Extrude(PlanarSketch sketch, double distance)
		{
			sketch.Visible = false;
			var sketchProfile = sketch.Profiles.AddForSolid();
			var extrudeDef =
				PartDefinition.Features.ExtrudeFeatures
					.CreateExtrudeDefinition(sketchProfile,
						PartFeatureOperationEnum.kJoinOperation);
			extrudeDef.SetDistanceExtent(distance,
				PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
			var extrude = PartDefinition.Features.ExtrudeFeatures.Add(extrudeDef);
			var objectCollection = CreateObjectCollection();
			objectCollection.Add(extrude);
		}

		/// <summary>
		/// Создает новый эскиз на поверхности детали.
		/// </summary>
		/// <param name="face">Плоскость.</param>
		/// <param name="offset">Расстояние от плоскости, на котором будет создана новая поверхность.</param>
		/// <returns>Новый эскиз.</returns>
		public PlanarSketch MakeNewSketch(object face, double offset)
		{
			var offsetPlane = PartDefinition.WorkPlanes
				.AddByPlaneAndOffset(face, offset, false);
			var sketch = PartDefinition.Sketches.Add(offsetPlane, false);
			return sketch;
		}

		/// <summary>
		/// Создание объекта коллекции.
		/// </summary>
		/// <returns>Коллекция объектов</returns>
		public ObjectCollection CreateObjectCollection()
		{
			return InvApp.TransientObjects.CreateObjectCollection();
		}

		#endregion
	}
}