using System.Windows;
using FenceBuildingVm;

namespace FenceBuildingUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// VM главного окна.
		/// </summary>
		public MainWindowVm _mainWindowVm;

		public MainWindow()
		{
			InitializeComponent();
			_mainWindowVm = new MainWindowVm(new MessageBoxService());
			DataContext = _mainWindowVm;
		}
	}
}
