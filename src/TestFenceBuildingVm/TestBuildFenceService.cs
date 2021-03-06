using Core;
using Services;
using System;

namespace TestFenceBuildingVm
{
	/// <summary>
	/// Тестовый класс для сервиса создания забора <see cref="IBuildFenceService"/>.
	/// </summary>
	public class TestBuildFenceService : IBuildFenceService
	{
		/// <summary>
		/// Возвращает True, если произошло построение.
		/// </summary>
		public bool IsBuilt { get; private set; }

		/// <summary>
		/// Флаг, показывающий — есть ли ошибка при открытии САПР.
		/// </summary>
		public bool CanOpen { private get; set; } = true;

		/// <inheritdoc/>
		public void BuildFence(FenceParameters fenceParameters, IApiService apiService)
		{
			if (CanOpen)
			{
				IsBuilt = true;
			}
			else
			{
				throw new ApplicationException("Test Message");
			}
		}
	}
}