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
		void BuildFence(FenceParameters fenceParameters);
	}
}