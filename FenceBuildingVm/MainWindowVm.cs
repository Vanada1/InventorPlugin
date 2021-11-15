using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InventorApi;

namespace FenceBuildingVm
{
	public class MainWindowVm : ViewModelBase
	{
		#region -- Fields --

		/// <summary>
		/// Параметры забора.
		/// </summary>
		private readonly FenceParameters _fenceParameters;

		/// <summary>
		/// Сервис окна сообщения.
		/// </summary>
		private readonly IMessageBoxService _messageBoxService;

		/// <summary>
		/// Словарь русских полей.
		/// </summary>
		private readonly Dictionary<string, string> _russianFields;
		
		#endregion

		#region -- Properties --

		/// <summary>
		/// Возвращает и задает значение ширины столбика.
		/// </summary>
		public string ColumnWidth
		{
			get => _fenceParameters.ColumnWidth.ToString();
			set
			{
				const string propertyName = nameof(ColumnWidth);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.TopFenceHeight = doubleValue;
					RaisePropertyChanged(propertyName);
				}

				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение расстояния между нижними перегородками. 
		/// </summary>
		public string DistanceLowerBaffles
		{
			get => _fenceParameters.DistanceLowerBaffles.ToString();
			set
			{
				const string propertyName = nameof(DistanceLowerBaffles);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.TopFenceHeight = doubleValue;
					RaisePropertyChanged(propertyName);
				}

				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение расстояния между верхними перегородками. 
		/// </summary>
		public string DistanceUpperBaffles
		{
			get => _fenceParameters.DistanceUpperBaffles.ToString();
			set
			{
				const string propertyName = nameof(DistanceUpperBaffles);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.TopFenceHeight = doubleValue;
					RaisePropertyChanged(propertyName);
				}

				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение высоты забора.
		/// </summary>
		public string FenceLength
		{
			get => _fenceParameters.FenceLength.ToString();
			set
			{
				const string propertyName = nameof(FenceLength);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.TopFenceHeight = doubleValue;
					RaisePropertyChanged(propertyName);
				}

				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение глубины погружения.
		/// </summary>
		public string ImmersionDepth
		{
			get => _fenceParameters.ImmersionDepth.ToString();
			set
			{
				const string propertyName = nameof(ImmersionDepth);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.TopFenceHeight = doubleValue;
					RaisePropertyChanged(propertyName); 
				}

				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение высоты верхней части забора.
		/// </summary>
		public string TopFenceHeight
		{
			get => _fenceParameters.TopFenceHeight.ToString();
			set
			{
				const string propertyName = nameof(TopFenceHeight);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.TopFenceHeight = doubleValue;
					RaisePropertyChanged(propertyName);
				}

				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает строку со всеми ошибками.
		/// </summary>
		public string ErrorText => GetAllErrors();

		#endregion

		#region -- Commands --

		/// <summary>
		/// Команда создания забора.
		/// </summary>
		public ICommand BuildCommand;

		#endregion

		#region -- Constructors --

		public MainWindowVm(IMessageBoxService messageBoxService)
		{
			_russianFields = new Dictionary<string, string>
			{
				{ nameof(ColumnWidth), "Ширина столбика" },
				{ nameof(DistanceLowerBaffles), "Расстояние между нижними перегородками" },
				{ nameof(DistanceUpperBaffles), "Расстояние между верхними перегородками" },
				{ nameof(FenceLength), "Длина забора" },
				{ nameof(ImmersionDepth), "Глубина погружения" },
				{ nameof(TopFenceHeight), "Высота верхней части забора" },
				{ nameof(TopFenceHeight) + nameof(ImmersionDepth), "Глубина погружения и Высота верхней части забора" },
			};

			_messageBoxService = messageBoxService;
			_fenceParameters = new FenceParameters();
			BuildCommand = new RelayCommand(BuildFence);
		}

		#endregion

		#region -- Private Methods --

		/// <summary>
		/// Пробует спарсить значение.
		/// </summary>
		/// <param name="value">Строковое значение.</param>
		/// <param name="nameProperty">Название свойства.</param>
		/// <param name="doubleValue">Значение в типе <see cref="double"/>.</param>
		/// <returns>True, если получилось спарсить.</returns>
		private bool CanChangeValue(string value, string nameProperty, out double doubleValue)
		{
			if (!double.TryParse(value, out doubleValue))
			{
				_fenceParameters.Errors.Add(nameProperty,
					": введенное значение не является вещественным числом.");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Получить строку со всеми ошибками.
		/// </summary>
		/// <returns>Строку с ошибками.</returns>
		private string GetAllErrors()
		{
			return _fenceParameters.Errors.Keys.Aggregate(string.Empty,
				(current, key) => current + (_russianFields[key] + _fenceParameters.Errors[key]));
		}

		/// <summary>
		/// Построить забор.
		/// </summary>
		private void BuildFence()
		{
			if (_fenceParameters.HasErrors)
			{
				_messageBoxService.Show("Не все ошибки исправлены!", "Ошибка!", MessageType.Error);
				return;
			}

			var fenceBuilder = new FenceBuilder(_fenceParameters);
			try
			{
				fenceBuilder.BuildFence();
			}
			catch (ApplicationException e)
			{
				_messageBoxService.Show(e.Message, "Ошибка!", MessageType.Error);
			}
		}

		#endregion
	}
}