using Core;

namespace Services
{
	/// <summary>
	/// Сервис для построения забора.
	/// </summary>
	public interface IBuildFenceService
	{
		/// <summary>
		/// Построить забор.
		/// </summary>
		/// <param name="fenceParameters">Параметры забора.</param>
		/// <param name="apiService">Используемое API.</param>
		void BuildFence(FenceParameters fenceParameters, IApiService apiService);
	}
}