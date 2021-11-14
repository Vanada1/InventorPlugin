using Core;
using NUnit.Framework;

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

		#region -- Test Property Errors --

		[Test(Description = "Проверка корректного возвращения свойства Error.")]
		public void TestErrors_CorrectGet()
		{
			var fenceParameters = FenceParameters;

			var expected = true;

			var actual = fenceParameters.Errors != null;

			Assert.AreEqual(expected, actual, $"Свойство {nameof(fenceParameters.Errors)} не создано и равно null.");
		}

		#endregion

		#region -- Test INotifyDataErrorInfo --

		[Test(Description = "Проверка корректного получения ошибки через метод GetErrors.")]
		public void TestGetErrors_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			var key = "TestKey";
			var value = "TestValue";
			fenceParameters.Errors.Add(key, value);

			var expected = value;

			var actual = fenceParameters.GetErrors(key).ToString();

			Assert.AreEqual(expected, actual, "Вернулась неверное описание ошибки.");
		}

		[Test(Description = "Проверка получения пустой строки ошибки через метод GetErrors.")]
		public void TestGetErrors_GetNull()
		{
			var fenceParameters = FenceParameters;
			var key = "TestKey";

			var expected = string.Empty;

			var actual = fenceParameters.GetErrors(key).ToString();

			Assert.AreEqual(expected, actual, $"Вернулось сообщение об ошибки: {actual}");
		}

		[Test(Description = "Проверка получения значения True при вызове свойства HasErrors.")]
		public void TestHasErrors_GetTrue()
		{
			var fenceParameters = FenceParameters;
			var key = "TestKey";
			var value = "TestValue";
			fenceParameters.Errors.Add(key, value);

			var expected = true;

			var actual = fenceParameters.HasErrors;

			Assert.AreEqual(expected, actual,
				$"Свойство {nameof(fenceParameters.HasErrors)} показало, что нет ошибок.");
		}

		[Test(Description = "Проверка получения значения False при вызове свойства HasErrors.")]
		public void TestHasErrors_GetFalse()
		{
			var fenceParameters = FenceParameters;

			var expected = false;

			var actual = fenceParameters.HasErrors;

			Assert.AreEqual(expected, actual,
				$"Свойство {nameof(fenceParameters.HasErrors)} показало, что есть ошибок.");
		}

		#endregion

		#region -- Test ColumnWidth --

		[Test(Description = "Проверка корректного получения значения свойства ColumnWidth.")]
		public void TestColumnWidth_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			var expected = value;

			fenceParameters.ColumnWidth = value;

			var actual = fenceParameters.ColumnWidth;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[Test(Description = "Проверка корректной записи значения свойства ColumnWidth. Не должно выбрасывать исключения.")]
		public void TestColumnWidth_CorrectSetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			Assert.DoesNotThrow(() => fenceParameters.ColumnWidth = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства ColumnWidth, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства ColumnWidth, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestColumnWidth_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;

			var expected = fenceParameters.ColumnWidth;

			fenceParameters.ColumnWidth = value;

			var actual = fenceParameters.ColumnWidth;

			Assert.AreEqual(expected, actual,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test DistanceLowerBaffles --

		[Test(Description = "Проверка корректного получения значения свойства DistanceLowerBaffles.")]
		public void TestDistanceLowerBaffles_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			var expected = value;

			fenceParameters.DistanceLowerBaffles = value;

			var actual = fenceParameters.DistanceLowerBaffles;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[Test(Description = "Проверка корректной записи значения свойства DistanceLowerBaffles. Не должно выбрасывать исключения.")]
		public void TestDistanceLowerBaffles_CorrectSetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			Assert.DoesNotThrow(() => fenceParameters.DistanceLowerBaffles = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства DistanceLowerBaffles, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства DistanceLowerBaffles, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestDistanceLowerBaffles_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;

			var expected = fenceParameters.DistanceLowerBaffles;

			fenceParameters.DistanceLowerBaffles = value;

			var actual = fenceParameters.DistanceLowerBaffles;

			Assert.AreEqual(expected, actual,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test DistanceUpperBaffles --

		[Test(Description = "Проверка корректного получения значения свойства DistanceUpperBaffles.")]
		public void TestDistanceUpperBaffles_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			var expected = value;

			fenceParameters.DistanceUpperBaffles = value;

			var actual = fenceParameters.DistanceUpperBaffles;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[Test(Description = "Проверка корректной записи значения свойства DistanceUpperBaffles. Не должно выбрасывать исключения.")]
		public void TestDistanceUpperBaffles_CorrectSetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			Assert.DoesNotThrow(() => fenceParameters.DistanceUpperBaffles = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства DistanceUpperBaffles, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства DistanceUpperBaffles, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestDistanceUpperBaffles_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;

			var expected = fenceParameters.DistanceLowerBaffles;

			fenceParameters.DistanceLowerBaffles = value;

			var actual = fenceParameters.DistanceLowerBaffles;

			Assert.AreEqual(expected, actual,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test FenceLength --

		[Test(Description = "Проверка корректного получения значения свойства FenceLength.")]
		public void TestFenceLength_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 1000.0;

			var expected = value;

			fenceParameters.FenceLength = value;

			var actual = fenceParameters.FenceLength;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[Test(Description = "Проверка корректной записи значения свойства FenceLength. Не должно выбрасывать исключения.")]
		public void TestFenceLength_CorrectSetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			Assert.DoesNotThrow(() => fenceParameters.FenceLength = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства FenceLength, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства FenceLength, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestFenceLength_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;

			var expected = fenceParameters.FenceLength;

			fenceParameters.FenceLength = value;

			var actual = fenceParameters.FenceLength;

			Assert.AreEqual(expected, actual,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test ImmersionDepth --

		[Test(Description = "Проверка корректного получения значения свойства ImmersionDepth.")]
		public void TestImmersionDepth_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 500;

			var expected = value;

			fenceParameters.ImmersionDepth = value;

			var actual = fenceParameters.ImmersionDepth;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[Test(Description = "Проверка корректной записи значения свойства ImmersionDepth. Не должно выбрасывать исключения.")]
		public void TestImmersionDepth_CorrectSetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			Assert.DoesNotThrow(() => fenceParameters.ImmersionDepth = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства ImmersionDepth, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства ImmersionDepth, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestImmersionDepth_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;

			var expected = fenceParameters.ImmersionDepth;

			fenceParameters.ImmersionDepth = value;

			var actual = fenceParameters.ImmersionDepth;

			Assert.AreEqual(expected, actual,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test TopFenceHeight --

		[Test(Description = "Проверка корректного получения значения свойства TopFenceHeight.")]
		public void TestTopFenceHeight_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 1000;

			var expected = value;

			fenceParameters.TopFenceHeight = value;

			var actual = fenceParameters.TopFenceHeight;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[Test(Description = "Проверка корректной записи значения свойства TopFenceHeight. Не должно выбрасывать исключения.")]
		public void TestTopFenceHeight_CorrectSetValue()
		{
			var fenceParameters = FenceParameters;
			var value = 10.0;

			Assert.DoesNotThrow(() => fenceParameters.TopFenceHeight = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства TopFenceHeight, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства TopFenceHeight, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestTopFenceHeight_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;

			var expected = fenceParameters.TopFenceHeight;

			fenceParameters.TopFenceHeight = value;

			var actual = fenceParameters.TopFenceHeight;

			Assert.AreEqual(expected, actual,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test SetValue --

		[Test(Description = "Повторное присвоение некорректного значения.")]
		public void TestSetValue_DoubleIncorrectSet()
		{
			var fenceParameters = FenceParameters;
			var value = 100.0;
			fenceParameters.ColumnWidth = value;
			fenceParameters.ColumnWidth = value;

			var expected = true;

			var errorMessage = fenceParameters.GetErrors(nameof(fenceParameters.ColumnWidth)).ToString();

			var actual = !string.IsNullOrEmpty(errorMessage);

			Assert.AreEqual(expected, actual, "При повторном присвоение некорректного значения значение было присвоено.");
		}

		[Test(Description = "Проверка исправления значения на корректное.")]
		public void TestSetValue_DoubleCorrectSet()
		{
			var fenceParameters = FenceParameters;
			fenceParameters.ColumnWidth = 100.0;

			fenceParameters.ColumnWidth = 40.0;

			var expected = string.Empty;

			var errorMessage = fenceParameters.GetErrors(nameof(fenceParameters.ColumnWidth)).ToString();

			var actual = errorMessage;

			Assert.AreEqual(expected, actual, "При повторном присвоение некорректного значения значение было присвоено.");
		}

		#endregion

		#region -- Test CheckFenceHeight --

		[Test(Description = "Сравнение значения суммарной высоты с общей высотой забора.")]
		public void TestCheckFenceHeight_CorrectHeight()
		{
			var fenceParameters = FenceParameters;
			fenceParameters.TopFenceHeight = 1000.0;
			fenceParameters.ImmersionDepth = 400.0;

			var expected = string.Empty;

			var errorMessage = fenceParameters.GetErrors(nameof(fenceParameters.TopFenceHeight)).ToString();

			var actual = errorMessage;

			Assert.AreEqual(expected, actual, "Значение суммарной высоты не входит диапазон общей высоты забора.");
		}

		#endregion
	}
}