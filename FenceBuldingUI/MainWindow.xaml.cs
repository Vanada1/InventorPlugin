using System.Windows;

namespace FenceBuildingUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Есть ошибки. Проверьте!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
