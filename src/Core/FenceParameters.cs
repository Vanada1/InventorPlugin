using System;
using System.Collections.Generic;

namespace Core
{
	/// <summary>
	/// Класс параметров забора.
	/// </summary>
	public class FenceParameters
	{
		#region -- Constants --

		/// <summary>
		/// Минимальная высота забора.
		/// </summary>
		private const double MinHeight = 1000.0;

		/// <summary>
		/// Максимальная высота забора.
		/// </summary>
		private const double MaxHeight = 2000.0;

		#endregion

		#region -- Fields --

		/// <summary>
		/// Ширины столбика.
		/// </summary>
		private double _columnWidth = double.NaN;

		/// <summary>
		/// Расстояние между нижними перегородками.
		/// </summary>
		private double _distanceLowerBaffles = double.NaN;

		/// <summary>
		/// Расстояние между верхними перегородками.
		/// </summary>
		private double _distanceUpperBaffles = double.NaN;

		/// <summary>
		/// Высота забора.
		/// </summary>
		private double _fenceLength = double.NaN;

		/// <summary>
		/// Глубина погружения.
		/// </summary>
		private double _immersionDepth = double.NaN;

		/// <summary>
		/// Высота верхней части.
		/// </summary>
		private double _topFenceHeight = double.NaN;

		/// <summary>
		/// Словарь параметров.
		/// </summary>
		private Dictionary<ParameterType, Parameter> _parameters =
			new Dictionary<ParameterType, Parameter>
		{
			{ ParameterType.ColumnWidth, new Parameter(10.0,
				70.0, 10.0) },
			{ ParameterType.DistanceLowerBaffles, new Parameter(
				10.0, 1500.0, 30.0) },
			{ ParameterType.DistanceUpperBaffles, new Parameter(
				10.0, 1500.0, 40.0) },
			{ ParameterType.FenceLength, new Parameter(
				1000.0, 3000.0, 2000.0) },
			{ ParameterType.ImmersionDepth, new Parameter(
				MinHeight / 3, MinHeight * 0.5, 500.0) },
			{ ParameterType.TopFenceHeight, new Parameter(
				MinHeight / 3 * 2, MinHeight * 1.5, 1000.0) },
		};

		#endregion

		#region -- Properties --

		/// <summary>
		/// Возвращает и задает значение ширины столбика.
		/// </summary>
		public double ColumnWidth
		{
			get => _columnWidth;
			set
			{
				const double minValue = 10.0;
				const double maxValue = 70.0;
				SetValue(ref _columnWidth, value, minValue,
					maxValue);
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение расстояния между нижними перегородками. 
		/// </summary>
		public double DistanceLowerBaffles
		{
			get => _distanceLowerBaffles;
			set
			{
				var minValue = double.IsNaN(ColumnWidth) ? 10.0 : ColumnWidth;
				var maxValue = (double.IsNaN(FenceLength) ? 3000.0 : FenceLength) / 2;
				SetValue(ref _distanceLowerBaffles, value, minValue,
					maxValue);
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение расстояния между верхними перегородками. 
		/// </summary>
		public double DistanceUpperBaffles
		{
			get => _distanceUpperBaffles;
			set
			{
				var minValue = double.IsNaN(ColumnWidth) ? 10.0 : ColumnWidth;
				var maxValue = (double.IsNaN(FenceLength) ? 3000.0 : FenceLength) / 2;
				SetValue(ref _distanceUpperBaffles, value, minValue,
					maxValue);
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение высоты забора.
		/// </summary>
		public double FenceLength
		{
			get => _fenceLength;
			set
			{
				const double minValue = 1000.0;
				const double maxValue = 3000.0;
				SetValue(ref _fenceLength, value, minValue,
					maxValue);
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение глубины погружения.
		/// </summary>
		public double ImmersionDepth
		{
			get => _immersionDepth;
			set
			{
				var minValue = (int)(double.IsNaN(TopFenceHeight) ? MinHeight :
					TopFenceHeight) / 3;
				var maxValue = (double.IsNaN(TopFenceHeight) ? MinHeight :
					TopFenceHeight) * 0.5;
				SetValue(ref _immersionDepth, value, minValue,
					maxValue);
				CheckFenceHeight();
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение высоты верхней части забора.
		/// </summary>
		public double TopFenceHeight
		{
			get => _topFenceHeight;
			set
			{
				var minValue = (double.IsNaN(ImmersionDepth) ? MinHeight / 3 :
					ImmersionDepth) * 2;
				var maxValue = (double.IsNaN(ImmersionDepth) ? MinHeight / 2 :
					ImmersionDepth) * 3;
				SetValue(ref _topFenceHeight, value, minValue,
					maxValue);
				CheckFenceHeight();
			}
		}

		/// <summary>
		/// Возвращает общую высоту забора.
		/// </summary>
		public double FenceHeight => TopFenceHeight + ImmersionDepth;

		#endregion


		#region -- Constructors --

		/// <summary>
		/// Конструктор.
		/// </summary>
		public FenceParameters()
		{
			FenceLength = 2000.0;
			ColumnWidth = 10.0;
			DistanceLowerBaffles = 30.0;
			DistanceUpperBaffles = 40.0;
			ImmersionDepth = 500.0;
			TopFenceHeight = 1000.0;
		}

		#endregion

		/// <summary>
		/// Установить значение параметра.
		/// </summary>
		/// <param name="parameterType">Тип параметра..</param>
		/// <param name="value">Значение параметра.</param>
		public void SetValue(ParameterType parameterType, double value)
		{
			var minValue = _parameters[parameterType].MinValue;
			var maxValue = _parameters[parameterType].MaxValue;

			switch (parameterType)
			{
				case ParameterType.FenceLength:
				case ParameterType.ColumnWidth:
				{
					break;
				}
				case ParameterType.DistanceUpperBaffles:
				case ParameterType.DistanceLowerBaffles:
				{
					minValue = double.IsNaN(
						_parameters[ParameterType.ColumnWidth].Value)
						? 10.0
						: ColumnWidth;
					maxValue = (double.IsNaN(
						_parameters[ParameterType.FenceLength].Value)
						? 3000.0
						: FenceLength) / 2;
					break;
				}
				case ParameterType.ImmersionDepth:
				{
					minValue = (double.IsNaN(
							_parameters[ParameterType.TopFenceHeight].Value)
							? MinHeight
							: TopFenceHeight) / 3;
					maxValue = (double.IsNaN(
							_parameters[ParameterType.TopFenceHeight].Value) ?
							MinHeight :
							TopFenceHeight) * 0.5;
						break;
				}
				case ParameterType.TopFenceHeight:
				{
					minValue = (double.IsNaN(
						_parameters[ParameterType.ImmersionDepth].Value)
						? MinHeight / 3
						: ImmersionDepth) * 2;
					maxValue = (double.IsNaN(
						_parameters[ParameterType.ImmersionDepth].Value)
						? MinHeight / 2
						: ImmersionDepth) * 3;
						break;
				}
				default:
				{
					throw new ArgumentOutOfRangeException(nameof(parameterType), parameterType, null);
				}
			}

			_parameters[parameterType].MinValue = minValue;
			_parameters[parameterType].MaxValue = maxValue;
			_parameters[parameterType].Value = value;
		}

		/// <summary>
		/// Получить значение параметра.
		/// </summary>
		/// <param name="parameterType">Тип параметра.</param>
		/// <returns>Значение параметра.</returns>
		public double GetValue(ParameterType parameterType)
		{
			return _parameters[parameterType].Value;
		}

		#region -- Private Methods --

		/// <summary>
		/// Установить значение параметру.
		/// </summary>
		/// <param name="field">Поле для записи значения.</param>
		/// <param name="value">Значение, которое нужно записать.</param>
		/// <param name="minValue">Минимально возможное значение.</param>
		/// <param name="maxValue">Максимально возможное значение.</param>
		private void SetValue(ref double field, double value, double minValue,
			double maxValue)
		{
			field = value;

			if (!Validator.Validate(value, minValue, maxValue))
			{
				throw new ArgumentException($"значение не входит диапазон {minValue} — {maxValue}");
			}
		}

		/// <summary>
		/// Проверка общей высоты забора.
		/// </summary>
		private void CheckFenceHeight()
		{
			var currentHeight = ImmersionDepth + TopFenceHeight;
			if (currentHeight >= MinHeight && currentHeight <= MaxHeight || double.IsNaN(currentHeight))
			{
				return;
			}

			var errorMessage = $"сумма значений не входит диапазон {MinHeight} — {MaxHeight}";
			throw new ArgumentException(errorMessage);
		}

		#endregion

	}
}