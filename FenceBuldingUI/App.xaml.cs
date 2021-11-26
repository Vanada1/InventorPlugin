using System;
using System.IO;
using System.Windows;
using Builder;
using FenceBuildingVm;
using InventorApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace FenceBuildingUI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Все созданные сервисы.
		/// </summary>
		public IServiceProvider ServiceProvider { get; private set; }

		/// <summary>
		/// Конфигурация приложения.
		/// </summary>
		public IConfiguration Configuration { get; private set; }

		/// <inheritdoc/>
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

			Configuration = builder.Build();

			var serviceCollection = new ServiceCollection();
			ConfigureService(serviceCollection);

			ServiceProvider = serviceCollection.BuildServiceProvider();

			var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
			mainWindow.ShowDialog();
		}

		/// <summary>
		/// Добавление всех сервисов.
		/// </summary>
		/// <param name="service">Коллекция сервисов.</param>
		private void ConfigureService(ServiceCollection service)
		{
			service.AddScoped<IBuildFenceService, FenceBuilder>();
			service.AddScoped<IMessageBoxService, MessageBoxService>();
			service.AddSingleton<IApiService, InventorWrapper>();
			service.AddTransient<MainWindowVm>();
			service.AddTransient(provider => new MainWindow
				{ DataContext = provider.GetService<MainWindowVm>() });
		}
	}
}
