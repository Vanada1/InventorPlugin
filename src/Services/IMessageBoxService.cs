namespace Services
{
	/// <summary>
	/// Сервис для вызова окна сообщения.
	/// </summary>
	public interface IMessageBoxService
	{
		/// <summary>
		/// Показать окно сообщения.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		/// <param name="caption">Заголовок.</param>
		/// <param name="type">Тип окна сообщения.</param>
		void Show(string message, string caption, MessageType type);
	}
}