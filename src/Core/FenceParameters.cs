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
		/// Словарь параметров.
		/// </summary>
		private readonly Dictionary<ParameterType, Parameter> _parameters =
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

		#region -- Constructors --

		/// <summary>
		/// Конструктор.
		/// </summary>
		public FenceParameters()
		{

		}

		#endregion

		#region -- Public Methods --

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
						: _parameters[ParameterType.ColumnWidth].Value;
					maxValue = (double.IsNaN(
						_parameters[ParameterType.FenceLength].Value)
						? 3000.0
						: _parameters[ParameterType.FenceLength].Value) / 2;
					break;
				}
				case ParameterType.ImmersionDepth:
				{
					minValue = (double.IsNaN(
						_parameters[ParameterType.TopFenceHeight].Value)
						? MinHeight
						: _parameters[ParameterType.TopFenceHeight].Value) / 3;
					maxValue = (double.IsNaN(
						_parameters[ParameterType.TopFenceHeight].Value)
						? MinHeight
						: _parameters[ParameterType.TopFenceHeight].Value) * 0.5;
					break;
				}
				case ParameterType.TopFenceHeight:
				{
					minValue = (double.IsNaN(
						_parameters[ParameterType.ImmersionDepth].Value)
						? MinHeight / 3
						: _parameters[ParameterType.ImmersionDepth].Value) * 2;
					maxValue = (double.IsNaN(
						_parameters[ParameterType.ImmersionDepth].Value)
						? MinHeight / 2
						: _parameters[ParameterType.ImmersionDepth].Value) * 3;
					break;
				}
			}

			_parameters[parameterType].MinValue = minValue;
			_parameters[parameterType].MaxValue = maxValue;
			_parameters[parameterType].Value = value;
			if (parameterType == ParameterType.TopFenceHeight || parameterType == ParameterType.ImmersionDepth)
			{
				CheckFenceHeight();
			}
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

		#endregion

		#region -- Private Methods --

		/// <summary>
		/// Проверка общей высоты забора.
		/// </summary>
		private void CheckFenceHeight()
		{
			var currentHeight = _parameters[ParameterType.ImmersionDepth].Value +
			                    _parameters[ParameterType.TopFenceHeight].Value;
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