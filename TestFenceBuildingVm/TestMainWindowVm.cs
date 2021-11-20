using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FenceBuildingVm;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace TestFenceBuildingVm
{
	/// <summary>
	/// Класс тестирования <see cref="MainWindowVm"/>.
	/// </summary>
	[TestFixture]
	public class TestMainWindowVm
	{
		/// <summary>
		/// Возвращает новый экземпляр класса <see cref="MainWindowVm"/>
		/// </summary>
		private MainWindowVm ViewModel => new MainWindowVm(new TestMessageBoxService());

		// TODO: добавить тестов для интерфейса.
		#region -- Test INotifyDataErrorInfo --

		[TestCase(TestName = "Проверка свойства HasErrors — " +
		                     "должно вернуться значение типа bool." +
		                     " Не должно вызываться исключение")]
		public void TestHasErrors_CorrectGet()
		{
			var viewModel = ViewModel;

			Assert.DoesNotThrow(()=>
			{
				var hasError = viewModel.HasErrors;
			},
				"Вылетело исключение при попытке" +
				$" получения значения свойства {nameof(viewModel.HasErrors)}");
		}

		#endregion

		#region -- Test ColumnWidth --

		[TestCase(TestName = "Проверка корректного получения" +
		                     " значения свойства ColumnWidth.")]
		public void TestColumnWidth_CorrectGetValue()
		{
			var viewModel = ViewModel;
			var value = 10.0;

			var expected = value.ToString();

			viewModel.ColumnWidth = value.ToString();

			var actual = viewModel.ColumnWidth;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[TestCase(TestName = "Проверка корректной записи значения свойства ColumnWidth." +
							 " Должно свойство HasErrors быть в значении False.")]
		public void TestColumnWidth_CorrectSetValue()
		{
			var viewModel = ViewModel;
			var value = 10.0;

			viewModel.ColumnWidth = value.ToString();

			Assert.IsFalse(viewModel.HasErrors,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, TestName = "Проверка некорректной передачи значения свойства ColumnWidth," +
		                          " меньшему минимальному." +
								  " Должно свойство HasErrors быть в значении True.")]
		[TestCase(10000.0, TestName = "Проверка некорректной передачи значения свойства ColumnWidth," +
		                              " большему максимальному." +
									  " Должно свойство HasErrors быть в значении True.")]
		public void TestColumnWidth_IncorrectSetValue(double value)
		{
			var viewModel = ViewModel;

			viewModel.ColumnWidth = value.ToString();

			Assert.IsTrue(viewModel.HasErrors,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test DistanceLowerBaffles --

		[TestCase(TestName = "Проверка корректного получения " +
							 "значения свойства DistanceLowerBaffles.")]
		public void TestDistanceLowerBaffles_CorrectGetValue()
		{
			var fenceParameters = ViewModel;
			var value = 10.0;

			var expected = value.ToString();

			fenceParameters.DistanceLowerBaffles = value.ToString();

			var actual = fenceParameters.DistanceLowerBaffles;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[TestCase(TestName = "Проверка корректной записи значения " +
							 "свойства DistanceLowerBaffles." +
							 " Должно свойство HasErrors быть в значении False.")]
		public void TestDistanceLowerBaffles_CorrectSetValue()
		{
			var viewModel = ViewModel;
			var value = 10.0;

			viewModel.DistanceLowerBaffles = value.ToString();

			Assert.IsFalse(viewModel.HasErrors,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, TestName = "Проверка некорректной передачи значения свойства DistanceLowerBaffles," +
								  " меньшему минимальному." +
								  " Должно свойство HasErrors быть в значении True.")]
		[TestCase(10000.0, TestName = "Проверка некорректной передачи значения свойства DistanceLowerBaffles," +
									  " большему максимальному." +
									  " Должно свойство HasErrors быть в значении True.")]
		public void TestDistanceLowerBaffles_IncorrectSetValue(double value)
		{
			var viewModel = ViewModel;

			viewModel.DistanceLowerBaffles = value.ToString();

			Assert.IsTrue(viewModel.HasErrors,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test DistanceUpperBaffles --

		[TestCase(TestName = "Проверка корректного получения" +
							 " значения свойства DistanceUpperBaffles.")]
		public void TestDistanceUpperBaffles_CorrectGetValue()
		{
			var viewModel = ViewModel;
			var value = 10.0;

			var expected = value.ToString();

			viewModel.DistanceUpperBaffles = value.ToString();

			var actual = viewModel.DistanceUpperBaffles;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[TestCase(TestName = "Проверка корректной записи значения" +
							 " свойства DistanceUpperBaffles." +
							 " Должно свойство HasErrors быть в значении False.")]
		public void TestDistanceUpperBaffles_CorrectSetValue()
		{
			var viewModel = ViewModel;
			var value = 10.0;

			viewModel.DistanceUpperBaffles = value.ToString();

			Assert.IsFalse(viewModel.HasErrors,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, TestName = "Проверка некорректной передачи значения свойства DistanceUpperBaffles," +
								  " меньшему минимальному." +
								  " Должно свойство HasErrors быть в значении True.")]
		[TestCase(10000.0, TestName = "Проверка некорректной передачи значения свойства DistanceUpperBaffles," +
									  " большему максимальному." +
									  " Должно свойство HasErrors быть в значении True.")]
		public void TestDistanceUpperBaffles_IncorrectSetValue(double value)
		{
			var viewModel = ViewModel;

			viewModel.DistanceLowerBaffles = value.ToString();

			Assert.IsTrue(viewModel.HasErrors,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test FenceLength --

		[TestCase(TestName = "Проверка корректного получения" +
							 " значения свойства FenceLength.")]
		public void TestFenceLength_CorrectGetValue()
		{
			var viewModel = ViewModel;
			var value = 1000.0;

			var expected = value.ToString();

			viewModel.FenceLength = value.ToString();

			var actual = viewModel.FenceLength;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[TestCase(TestName = "Проверка корректной записи значения " +
							 "свойства FenceLength. Должно свойство HasErrors быть в значении False.")]
		public void TestFenceLength_CorrectSetValue()
		{
			var viewModel = ViewModel;
			var value = 1500.0;

			viewModel.FenceLength = value.ToString();

			Assert.IsFalse(viewModel.HasErrors,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, TestName = "Проверка некорректной передачи значения свойства FenceLength," +
								  " меньшему минимальному." +
								  " Должно свойство HasErrors быть в значении True.")]
		[TestCase(10000.0, TestName = "Проверка некорректной передачи значения свойства FenceLength," +
									  " большему максимальному." +
									  " Должно свойство HasErrors быть в значении True.")]
		public void TestFenceLength_IncorrectSetValue(double value)
		{
			var viewModel = ViewModel;

			viewModel.FenceLength = value.ToString();

			Assert.IsTrue(viewModel.HasErrors,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test ImmersionDepth --

		[TestCase(TestName = "Проверка корректного получения" +
							 " значения свойства ImmersionDepth.")]
		public void TestImmersionDepth_CorrectGetValue()
		{
			var viewModel = ViewModel;
			var value = 500;

			var expected = value.ToString();

			viewModel.ImmersionDepth = value.ToString();

			var actual = viewModel.ImmersionDepth;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[TestCase(TestName = "Проверка корректной записи значения " +
							 "свойства ImmersionDepth." +
							 " Должно свойство HasErrors быть в значении False.")]
		public void TestImmersionDepth_CorrectSetValue()
		{
			var viewModel = ViewModel;
			var value = 500.0;

			viewModel.ImmersionDepth = value.ToString();

			Assert.IsFalse(viewModel.HasErrors,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, TestName = "Проверка некорректной передачи значения свойства ImmersionDepth," +
								  " меньшему минимальному." +
								  " Должно свойство HasErrors быть в значении True.")]
		[TestCase(10000.0, TestName = "Проверка некорректной передачи значения свойства ImmersionDepth," +
									  " большему максимальному." +
									  " Должно свойство HasErrors быть в значении True.")]
		public void TestImmersionDepth_IncorrectSetValue(double value)
		{
			var viewModel = ViewModel;

			viewModel.ImmersionDepth = value.ToString();

			Assert.IsTrue(viewModel.HasErrors,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion

		#region -- Test TopFenceHeight --

		[TestCase(TestName = "Проверка корректного получения" +
							 " значения свойства TopFenceHeight.")]
		public void TestTopFenceHeight_CorrectGetValue()
		{
			var viewModel = ViewModel;
			var value = 1000;

			var expected = value.ToString();

			viewModel.TopFenceHeight = value.ToString();

			var actual = viewModel.TopFenceHeight;

			Assert.AreEqual(expected, actual, "Вернулось некорректное значение.");
		}

		[TestCase(TestName = "Проверка корректной записи " +
							 "значения свойства TopFenceHeight. " +
							 "Должно свойство HasErrors быть в значении False.")]
		public void TestTopFenceHeight_CorrectSetValue()
		{
			var viewModel = ViewModel;
			var value = 1000.0;

			viewModel.TopFenceHeight = value.ToString();

			Assert.IsFalse(viewModel.HasErrors,
				"Не удалось присвоить корректное значение.");
		}

		[TestCase(9.0, TestName = "Проверка некорректной передачи значения" +
								  " свойства TopFenceHeight, меньшему минимальному." +
								  " Должно свойство HasErrors быть в значении True.")]
		[TestCase(10000.0, TestName = "Проверка некорректной передачи значения" +
									  " свойства TopFenceHeight, большему максимальному." +
									  " Должно свойство HasErrors быть в значении True.")]
		public void TestTopFenceHeight_IncorrectSetValue(double value)
		{
			var viewModel = ViewModel;

			viewModel.TopFenceHeight = value.ToString();

			Assert.IsTrue(viewModel.HasErrors,
				$"Присвоило значение не входящие в диапазон.");
		}

		#endregion
	}
}
