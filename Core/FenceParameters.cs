using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using GalaSoft.MvvmLight;

namespace Core
{
	/// <summary>
	/// Класс параметров забора.
	/// </summary>
	public class FenceParameters : ViewModelBase, INotifyDataErrorInfo
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

		#endregion

		#region -- Properties --

		/// <summary>
		/// Возвращает словарь ошибок.
		/// </summary>
		public Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();

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
					maxValue, nameof(ColumnWidth));
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
				var maxValue = double.IsNaN(FenceLength) ? 3000.0 : FenceLength;
				SetValue(ref _distanceLowerBaffles, value, minValue,
					maxValue, nameof(DistanceLowerBaffles));
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
				var maxValue = double.IsNaN(FenceLength) ? 3000.0 : FenceLength;
				SetValue(ref _distanceUpperBaffles, value, minValue,
					maxValue, nameof(DistanceUpperBaffles));
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
					maxValue, nameof(FenceLength));
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
				var minValue = (int)(double.IsNaN(TopFenceHeight) ? MinHeight : TopFenceHeight) / 3 + 1;
				var maxValue = (double.IsNaN(TopFenceHeight) ? MinHeight : TopFenceHeight) * 0.5;
				SetValue(ref _immersionDepth, value, minValue,
					maxValue, nameof(ImmersionDepth));
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
				var minValue = (double.IsNaN(TopFenceHeight) ? MinHeight / 3 : ImmersionDepth) * 2;
				var maxValue = (double.IsNaN(TopFenceHeight) ? MinHeight / 2 : ImmersionDepth) * 3;
				SetValue(ref _topFenceHeight, value, minValue,
					maxValue, nameof(TopFenceHeight));
				CheckFenceHeight();
			}
		}

		/// <summary>
		/// Возвращает общую высоту забора.
		/// </summary>
		public double FenceHeight => TopFenceHeight + ImmersionDepth;

		#endregion

		#region -- Constructors --

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

		#region -- Private Methods --

		/// <summary>
		/// Установить значение параметру.
		/// </summary>
		/// <param name="field">Поле для записи значения.</param>
		/// <param name="value">Значение, которое нужно записать.</param>
		/// <param name="minValue">Минимально возможное значение.</param>
		/// <param name="maxValue">Максимально возможное значение.</param>
		/// <param name="propertyName">Имя свойства.</param>
		private void SetValue(ref double field, double value, double minValue, double maxValue, string propertyName)
		{
			if (!Validator.Validate(value, minValue, maxValue))
			{
				if (!Errors.ContainsKey(propertyName))
				{
					Errors.Add(propertyName,
						$": значение не входит диапазон {minValue} — {maxValue}");
					return;
				}
			}
			else
			{
				if (Errors.ContainsKey(propertyName))
				{
					Errors.Remove(propertyName);
				}
			}

			Set(ref field, value);
		}

		/// <summary>
		/// Проверка общей высоты забора.
		/// </summary>
		private void CheckFenceHeight()
		{
			var currentHeight = ImmersionDepth + TopFenceHeight;
			const string key = nameof(TopFenceHeight) + nameof(ImmersionDepth);
			if (currentHeight >= MinHeight && currentHeight <= MaxHeight)
			{
				if (Errors.ContainsKey(key))
				{
					Errors.Remove(key);
				}

				return;
			}

			var errorMessage = $": сумма значений не входит диапазон {MinHeight} — {MaxHeight}";
			if (!Errors.ContainsKey(key))
			{
				Errors.Add(key, errorMessage);
			}
		}

		#endregion

		#region -- INotifyDataErrorInfo --

		/// <inheritdoc/>
		public bool HasErrors => Errors.Any();

		/// <inheritdoc/>
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		/// <inheritdoc/>
		public IEnumerable GetErrors(string propertyName)
		{
			return Errors.ContainsKey(propertyName) ? Errors[propertyName] : string.Empty;
		}

		#endregion

	}
}