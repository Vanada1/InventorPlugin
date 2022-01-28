using Services;

namespace TestFenceBuildingVm
{
	/// <summary>
	/// Тестовый сервис диалогового окна <see cref="IMessageBoxService"/>.
	/// </summary>
	public class TestMessageBoxService : IMessageBoxService
	{
		/// <summary>
		/// Возвращает True, если окно открылось.
		/// </summary>
		public bool IsOpened { get; private set; }

		/// <inheritdoc/>
		public void Show(string message, string caption, MessageType type)
		{
			IsOpened = true;
		}
	}
}