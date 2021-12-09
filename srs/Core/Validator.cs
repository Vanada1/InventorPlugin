namespace Core
{
	/// <summary>
	/// Класс для валидации чисел.
	/// </summary>
	public static class Validator
	{
		/// <summary>
		/// Проверка значения на принадлежность промежутку <see cref="minValue"/> и <see cref="maxValue"/>.
		/// </summary>
		/// <param name="value">Проверяемое значение.</param>
		/// <param name="minValue">Минимально возможное значение.</param>
		/// <param name="maxValue">Максимально возможно значение.</param>
		/// <returns>True, если валидация пройдена.</returns>
		public static bool Validate(double value, double minValue, double maxValue)
		{
			return value >= minValue && value <= maxValue;
		}
	}
}