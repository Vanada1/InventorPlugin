namespace FenceBuildingVm
{
	/// <summary>
	/// Сервис для вызова окна сообщения.
	/// </summary>
	public interface IMessageBoxService
	{
		void Show(string message, MessageType type);
	}
}