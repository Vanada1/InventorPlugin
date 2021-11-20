using System;
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

		#region -- Test ColumnWidth --

		[TestCase(TestName = "Проверка корректного получения значения свойства ColumnWidth.")]
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
			Assert.Throws<ArgumentException>(()=> fenceParameters.ColumnWidth = value,
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
			Assert.Throws<ArgumentException>(() => fenceParameters.DistanceLowerBaffles = value,
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
			Assert.Throws<ArgumentException>(() => fenceParameters.DistanceLowerBaffles = value,
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
			var value = 1500.0;

			Assert.DoesNotThrow(() => fenceParameters.FenceLength = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства FenceLength, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства FenceLength, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestFenceLength_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;
			Assert.Throws<ArgumentException>(() => fenceParameters.FenceLength = value,
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
			var value = 500.0;

			Assert.DoesNotThrow(() => fenceParameters.ImmersionDepth = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства ImmersionDepth, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства ImmersionDepth, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestImmersionDepth_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;
			Assert.Throws<ArgumentException>(() => fenceParameters.ImmersionDepth = value,
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
			var value = 1000.0;

			Assert.DoesNotThrow(() => fenceParameters.TopFenceHeight = value,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, Description = "Проверка некорректной передачи значения свойства TopFenceHeight, меньшему минимальному. Некорректное значение не должно присваивается.")]
		[TestCase(10000.0, Description = "Проверка некорректной передачи значения свойства TopFenceHeight, большему максимальному. Некорректное значение не должно присваивается.")]
		public void TestTopFenceHeight_IncorrectSetValue(double value)
		{
			var fenceParameters = FenceParameters;
			Assert.Throws<ArgumentException>(() => fenceParameters.TopFenceHeight = value,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test FenceHeight --

		[Test(Description = "Проверка корректного получения значения свойства FenceHeight.")]
		public void TestFenceHeight_CorrectGetValue()
		{
			var fenceParameters = FenceParameters;
			fenceParameters.TopFenceHeight = 1000;
			fenceParameters.ImmersionDepth = 500;

			var expected = fenceParameters.TopFenceHeight + fenceParameters.ImmersionDepth;

			var actual = fenceParameters.FenceHeight;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		#endregion

		#region -- Test CheckFenceHeight --

		[Test(Description = "Сравнение значения суммарной высоты с общей разрешенной высотой забора. Должно выкинуться исключение.")]
		public void TestCheckFenceHeight_InCorrectHeight()
		{
			var fenceParameters = FenceParameters;
			fenceParameters.TopFenceHeight = 1500.0;
			Assert.Throws<ArgumentException>(() => fenceParameters.ImmersionDepth = 750.0,
				"Значение суммарной высоты входит диапазон общей высоты забора.");
		}

		#endregion
	}
}