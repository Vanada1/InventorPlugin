namespace Core
{
	/// <summary>
	/// Класс параметра.
	/// </summary>
	public class Parameter
	{
		/// <summary>
		/// Значение параметра.
		/// </summary>
		private double _value;

		/// <summary>
		/// Возвращает максимальное значение.
		/// </summary>
		public double MaxValue { get; internal set; }

		/// <summary>
		/// Возвращает минимальное значение.
		/// </summary>
		public double MinValue { get; internal set; }

		/// <summary>
		/// Возвращает и устанавливает значение параметра.
		/// </summary>
		public double Value
		{
			get => _value;
			set
			{
				_value = value;
				Validator.Validate(_value, MinValue, MaxValue);
			}
		}

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="minValue">Минимальное значение.</param>
		/// <param name="maxValue">Максимальное значение.</param>
		/// <param name="value">Значение параметра.</param>
		public Parameter(double minValue, double maxValue, double value)
		{
			MinValue = minValue;
			MaxValue = maxValue;
			Value = value;
		}
	}
}