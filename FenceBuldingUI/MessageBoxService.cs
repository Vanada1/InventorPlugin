using System.Collections.Generic;
using System.Windows;
using FenceBuildingVm;

namespace FenceBuildingUI
{
	/// <summary>
	/// Класс сервис окна сообщения.
	/// </summary>
	public class MessageBoxService : IMessageBoxService
	{
		/// <summary>
		/// Словарь ассоциаций типа окна сообщения.
		/// </summary>
		private readonly Dictionary<MessageType, MessageBoxImage> _boxImages = new Dictionary<MessageType, MessageBoxImage>()
		{
			{ MessageType.Error, MessageBoxImage.Error },
			{ MessageType.Info, MessageBoxImage.Information },
			{ MessageType.Warning, MessageBoxImage.Warning }
		};

		/// <inheritdoc/>
		public void Show(string message, string caption, MessageType type)
		{
			MessageBox.Show(message, caption, MessageBoxButton.OK, _boxImages[type]);
		}
	}
}