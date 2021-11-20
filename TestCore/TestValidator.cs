using Core;
using NUnit.Framework;

namespace TestCore
{
	/// <summary>
	/// Класс для тестирования класса <see cref="Validator"/>.
	/// </summary>
	[TestFixture]
	public class TestValidator
	{
		[TestCase(TestName = "Проверка валидации " +
		                     "при корректном введенном значении.")]
		public void TestValidate_CorrectValue()
		{
			const double minValue = 10.0;
			const double maxValue = 100.0;
			var value = 50.0;

			Assert.IsTrue(Validator.Validate(value, minValue, maxValue),
				$"Значение {value} не входит в диапазон {minValue} — {maxValue}");
		}

		[TestCase(9.0, TestName = "Проверка валидации при" +
		                          " значении меньшим минимального.")]
		[TestCase(110.0, TestName = "Проверка валидации при" +
		                            " значении больше максимального.")]
		public void TestValidate_IncorrectValue(double value)
		{
			const double minValue = 10.0;
			const double maxValue = 100.0;

			Assert.IsFalse(Validator.Validate(value, minValue, maxValue),
				$"Значение {value} входит в диапазон {minValue} — {maxValue}");
		}
	}
}
