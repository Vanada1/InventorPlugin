using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FenceBuildingUI.Converters
{
	/// <summary>
	/// Класс конвертации видимости пользовательского контрола.
	/// </summary>
	public class VisibilityConverter : IValueConverter
	{
		/// <inheritdoc/>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <inheritdoc/>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}