﻿using System.Windows;
using FenceBuildingVm;
using InventorApi;

namespace FenceBuildingUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Конструктор.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			var mainWindowVm = new MainWindowVm(new MessageBoxService(),
				new FenceBuilder());
			DataContext = mainWindowVm;
		}
	}
}
