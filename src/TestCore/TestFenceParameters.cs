using Core;
using NUnit.Framework;
using System;

namespace TestCore
{
	/// <summary>
	/// Класс для тестирования класса <see cref="Core.FenceParameters"/>.
	/// </summary>
	[TestFixture]
	public class TestFenceParameters
	{
		/// <summary>
		/// Возвращает новый экземпляр класса <see cref="Core.FenceParameters"/>.
		/// </summary>
		private FenceParameters FenceParameters => new FenceParameters();

		[TestCase(ParameterType.ColumnWidth, 20.0,
			TestName = "Проверка корректного получения" +
							 " значения свойства ColumnWidth.")]
		[TestCase(ParameterType.DistanceLowerBaffles, 40.0,
			TestName = "Проверка корректного получения" +
					   " значения свойства DistanceLowerBaffles.")]
		[TestCase(ParameterType.DistanceUpperBaffles, 50.0,
			TestName = "Проверка корректного получения" +
					   " значения свойства DistanceUpperBaffles.")]
		[TestCase(ParameterType.FenceLength, 1000.0,
			TestName = "Проверка корректного получения" +
					   " значения свойства FenceLength.")]
		[TestCase(ParameterType.ImmersionDepth, 400.0,
			TestName = "Проверка корректного получения" +
					   " значения свойства ImmersionDepth.")]
		[TestCase(ParameterType.TopFenceHeight, 1100.0,
			TestName = "Проверка корректного получения" +
					   " значения свойства TopFenceHeight.")]
		public void TestGetValue_CorrectGetValue(ParameterType parameterType, double value)
		{
			var fenceParameters = FenceParameters;

			var expected = value;

			fenceParameters.SetValue(parameterType, value);

			var actual = fenceParameters.GetValue(parameterType);

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[TestCase(ParameterType.ColumnWidth, 20.0, 
			TestName = "Проверка корректной записи значения свойства ColumnWidth." +
			           " Не должно выбрасывать исключения.")]
		[TestCase(ParameterType.DistanceLowerBaffles, 40.0,
			TestName = "Проверка корректной записи значения свойства DistanceLowerBaffles." +
		                     " Не должно выбрасывать исключения.")]
		[TestCase(ParameterType.DistanceUpperBaffles, 50.0,
			TestName = "Проверка корректной записи значения свойства DistanceUpperBaffles." +
		                     " Не должно выбрасывать исключения.")]
		[TestCase(ParameterType.FenceLength, 1000.0,
			TestName = "Проверка корректной записи значения свойства FenceLength." +
		                     " Не должно выбрасывать исключения.")]
		[TestCase(ParameterType.ImmersionDepth, 400.0,
			TestName = "Проверка корректной записи значения свойства ImmersionDepth." +
		                     " Не должно выбрасывать исключения.")]
		[TestCase(ParameterType.TopFenceHeight, 1100.0,
			TestName = "Проверка корректной записи значения свойства TopFenceHeight." +
		                     " Не должно выбрасывать исключения.")]
		public void TestSetValue_CorrectSetValue(ParameterType parameterType, double value)
		{
			var fenceParameters = FenceParameters;

			Assert.DoesNotThrow(() => fenceParameters.SetValue(parameterType, value),
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(ParameterType.ColumnWidth, 9.0,
			TestName = "Проверка некорректной передачи значения свойства ColumnWidth," +
			           " меньшему минимальному." +
			           "  Должно выбросить исключение.")]
		[TestCase(ParameterType.ColumnWidth, 10000.0, 
			TestName = "Проверка некорректной передачи значения свойства ColumnWidth," +
			           " большему максимальному." +
			           " Должно выбросить исключение.")]
		[TestCase(ParameterType.DistanceLowerBaffles, 9.0,
			TestName = "Проверка некорректной передачи значения свойства DistanceLowerBaffles," +
			           " меньшему минимальному." +
			           "  Должно выбросить исключение.")]
		[TestCase(ParameterType.DistanceLowerBaffles, 10000.0,
			TestName = "Проверка некорректной передачи значения свойства DistanceLowerBaffles," +
			           " большему максимальному." +
			           " Должно выбросить исключение.")]
		[TestCase(ParameterType.DistanceUpperBaffles, 9.0,
			TestName = "Проверка некорректной передачи значения свойства DistanceUpperBaffles," +
			           " меньшему минимальному." +
			           "  Должно выбросить исключение.")]
		[TestCase(ParameterType.DistanceUpperBaffles, 10000.0,
			TestName = "Проверка некорректной передачи значения свойства DistanceUpperBaffles," +
			           " большему максимальному." +
			           " Должно выбросить исключение.")]
		[TestCase(ParameterType.FenceLength, 9.0,
			TestName = "Проверка некорректной передачи значения свойства FenceLength," +
			           " меньшему минимальному." +
			           "  Должно выбросить исключение.")]
		[TestCase(ParameterType.FenceLength, 10000.0,
			TestName = "Проверка некорректной передачи значения свойства FenceLength," +
			           " большему максимальному." +
			           " Должно выбросить исключение.")]
		[TestCase(ParameterType.ImmersionDepth, 9.0,
			TestName = "Проверка некорректной передачи значения свойства ImmersionDepth," +
			           " меньшему минимальному." +
			           "  Должно выбросить исключение.")]
		[TestCase(ParameterType.ImmersionDepth, 10000.0,
			TestName = "Проверка некорректной передачи значения свойства ImmersionDepth," +
			           " большему максимальному." +
			           " Должно выбросить исключение.")]
		[TestCase(ParameterType.TopFenceHeight, 9.0,
			TestName = "Проверка некорректной передачи значения свойства TopFenceHeight," +
			           " меньшему минимальному." +
			           "  Должно выбросить исключение.")]
		[TestCase(ParameterType.TopFenceHeight, 10000.0,
			TestName = "Проверка некорректной передачи значения свойства TopFenceHeight," +
			           " большему максимальному." +
			           " Должно выбросить исключение.")]
		public void TestSetValue_IncorrectSetValue(ParameterType parameterType,double value)
		{
			var fenceParameters = FenceParameters;
			Assert.Throws<ArgumentException>(() => fenceParameters.SetValue(parameterType, value),
				$"Присвоило значение не входящие в диапазон.");
		}

		[TestCase(TestName = "Сравнение значения суммарной" +
		                     " высоты с общей разрешенной высотой забора." +
		                     " Должно выкинуться исключение.")]
		public void TestCheckFenceHeight_IncorrectHeight()
		{
			var fenceParameters = FenceParameters;

			fenceParameters.SetValue(ParameterType.TopFenceHeight, 1500.0);

			Assert.Throws<ArgumentException>(() => fenceParameters.SetValue(
					ParameterType.ImmersionDepth, 750.0),
				"Значение суммарной высоты входит диапазон общей высоты забора.");
		}
	}
}