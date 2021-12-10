using Builder;
using CommonTestClass;
using Core;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace TestBuilder
{
	/// <summary>
	/// Класс тестирования <see cref="Builder.FenceBuilder"/>.
	/// </summary>
	[TestFixture]
	public class TestFenceBuilder
	{
		/// <summary>
		/// Возвращает новый экземпляр класса <see cref="TestApiService"/>.
		/// </summary>
		private TestApiService TestApiService => new TestApiService();

		/// <summary>
		/// Возвращает новый экземпляр класса <see cref="FenceParameters"/>.
		/// </summary>
		private FenceParameters FenceParameters => new FenceParameters();

		/// <summary>
		/// Возвращает новый объект класса <see cref="Builder.FenceBuilder"/>.
		/// </summary>
		private FenceBuilder FenceBuilder => new FenceBuilder();

		[TestCase(TestName = "Тестирование построения корректного забора.")]
		public void TestBuildFence_DoesNotThrowException()
		{
			var fenceBuilder = FenceBuilder;
			var testApiService = TestApiService;
			var fenceParameters = FenceParameters;

			Assert.DoesNotThrow(() => fenceBuilder.BuildFence(fenceParameters, testApiService), "Произошла ошибка при построении.");
			Assert.IsTrue(testApiService.IsCreateDocument, "Документ не создан.");
			Assert.IsTrue(testApiService.IsCreateNewSketch, "Ни один эскиз не создан.");
			Assert.IsTrue(testApiService.IsCreatePoint, "Ни одна точка не создана.");
			Assert.IsTrue(testApiService.IsExtrude, "Ни одного выдавливания не выполнено.");
		}
	}
}
