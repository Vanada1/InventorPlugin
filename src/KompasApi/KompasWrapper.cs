using Kompas6API5;
using Kompas6Constants3D;
using Services;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace KompasApi
{
	/// <summary>
	/// Класс построения забора в Компас 3D.
	/// </summary>
	public class KompasWrapper : IApiService
	{
		/// <summary>
		/// Объект Компас 3D.
		/// </summary>
		private KompasObject _kompasObject;

		/// <summary>
		/// 3D документ компаса.
		/// </summary>
		private ksDocument3D _document3D;

		/// <summary>
		/// Часть документа.
		/// </summary>
		private ksPart _part;

		/// <inheritdoc/>
		public double Unit => 1;

		/// <inheritdoc/>
		public void CreateDocument()
		{
			if (_kompasObject == null)
			{
				var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
				_kompasObject = (KompasObject)Activator.CreateInstance(kompasType);
			}

			if (_kompasObject != null)
			{
				var retry = true;
				short tried = 0;
				while (retry)
				{
					try
					{
						tried++;
						_kompasObject.Visible = true;
						retry = false;
					}
					catch (COMException)
					{
						var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
						_kompasObject =
							(KompasObject)Activator.CreateInstance(kompasType);

						if (tried > 3)
						{
							retry = false;
						}
					}
				}

				_kompasObject.ActivateControllerAPI();
				_document3D = _kompasObject.Document3D();
				_document3D.Create();
				_part = _document3D.GetPart((int)Part_Type.pTop_Part);
			}
		}

		/// <inheritdoc/>
		public Point CreatePoint(double x, double y)
		{
			return new Point(x, y);
		}

		/// <inheritdoc/>
		public ISketch CreateNewSketch(int n, double offset)
		{
			return new KompasSketch(_part);
		}

		/// <inheritdoc/>
		public void Extrude(ISketch sketch, double distance)
		{
			if (!(sketch is KompasSketch kompasSketch))
			{
				throw new TypeAccessException($"Неверный тип эскиза." +
											  $" Нужный тип эскиза {nameof(KompasSketch)}.");
			}

			kompasSketch.EndEdit();
			ksEntity extrude = _part.NewEntity((int)Obj3dType.o3d_bossExtrusion);
			ksBossExtrusionDefinition extrudeDefinition = extrude.GetDefinition();
			extrudeDefinition.directionType = (int)Direction_Type.dtNormal;
			extrudeDefinition.SetSketch(kompasSketch.Sketch);
			ksExtrusionParam extrudeParam = extrudeDefinition.ExtrusionParam();
			extrudeParam.depthNormal = distance;
			extrude.Create();
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return "Kompas 3D";
		}
	}
}
