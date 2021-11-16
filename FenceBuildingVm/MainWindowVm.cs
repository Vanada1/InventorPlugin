﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InventorApi;

namespace FenceBuildingVm
{
	// Background="#eaa" цвет для ошибки.

	public class MainWindowVm : ViewModelBase, INotifyDataErrorInfo
	{
		#region -- Fields --

		/// <summary>
		/// Возвращает словарь ошибок.
		/// </summary>
		private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

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

		/// <summary>
		/// Ширины столбика.
		/// </summary>
		private string _columnWidth = string.Empty;

		/// <summary>
		/// Расстояние между нижними перегородками.
		/// </summary>
		private string _distanceLowerBaffles = string.Empty;

		/// <summary>
		/// Расстояние между верхними перегородками.
		/// </summary>
		private string _distanceUpperBaffles = string.Empty;

		/// <summary>
		/// Высота забора.
		/// </summary>
		private string _fenceLength = string.Empty;

		/// <summary>
		/// Глубина погружения.
		/// </summary>
		private string _immersionDepth = string.Empty;

		/// <summary>
		/// Высота верхней части.
		/// </summary>
		private string _topFenceHeight = string.Empty;

		#endregion

		#region -- Properties --

		/// <summary>
		/// Возвращает и задает значение ширины столбика.
		/// </summary>
		public string ColumnWidth
		{
			get => _columnWidth;
			set
			{
				const string propertyName = nameof(ColumnWidth);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.ColumnWidth = doubleValue;
					RaisePropertyChanged(propertyName);
				}

				Set(ref _columnWidth, value);
				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение расстояния между нижними перегородками. 
		/// </summary>
		public string DistanceLowerBaffles
		{
			get => _distanceLowerBaffles;
			set
			{
				const string propertyName = nameof(DistanceLowerBaffles);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.DistanceLowerBaffles = doubleValue;
				}

				Set(ref _distanceLowerBaffles, value);
				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение расстояния между верхними перегородками. 
		/// </summary>
		public string DistanceUpperBaffles
		{
			get => _distanceUpperBaffles;
			set
			{
				const string propertyName = nameof(DistanceUpperBaffles);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.DistanceUpperBaffles = doubleValue;
				}

				Set(ref _distanceUpperBaffles, value);
				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение высоты забора.
		/// </summary>
		public string FenceLength
		{
			get => _fenceLength;
			set
			{
				const string propertyName = nameof(FenceLength);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.FenceLength = doubleValue;
				}

				Set(ref _fenceLength, value);
				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение глубины погружения.
		/// </summary>
		public string ImmersionDepth
		{
			get => _immersionDepth;
			set
			{
				const string propertyName = nameof(ImmersionDepth);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.ImmersionDepth = doubleValue;
				}

				Set(ref _immersionDepth, value);
				RaisePropertyChanged(nameof(ErrorText));
			}
		}

		/// <summary>
		/// Возвращает и устанавливает значение высоты верхней части забора.
		/// </summary>
		public string TopFenceHeight
		{
			get => _topFenceHeight;
			set
			{
				const string propertyName = nameof(TopFenceHeight);
				if (CanChangeValue(value, propertyName, out var doubleValue))
				{
					_fenceParameters.TopFenceHeight = doubleValue;
					RaisePropertyChanged(propertyName);
				}

				Set(ref _topFenceHeight, value);
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
		public ICommand BuildCommand { get; }

		#endregion

		#region -- INotifyDataErrorInfo --

		/// <inheritdoc/>
		public bool HasErrors => _errors.Any();

		/// <inheritdoc/>
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		/// <inheritdoc/>
		public IEnumerable GetErrors(string propertyName)
		{
			return _errors.ContainsKey(propertyName) ? _errors[propertyName] : string.Empty;
		}

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
			SetValues();
		}

		#endregion

		#region -- Private Methods --

		/// <summary>
		/// Устанавливает значения из <see cref="FenceParameters"/>
		/// </summary>
		private void SetValues()
		{
			ColumnWidth = _fenceParameters.ColumnWidth.ToString();
			DistanceLowerBaffles = _fenceParameters.DistanceLowerBaffles.ToString();
			DistanceUpperBaffles = _fenceParameters.DistanceUpperBaffles.ToString();
			FenceLength = _fenceParameters.FenceLength.ToString();
			ImmersionDepth = _fenceParameters.ImmersionDepth.ToString();
			TopFenceHeight = _fenceParameters.TopFenceHeight.ToString();
		}

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
				_errors.Add(nameProperty,
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
			//return _fenceParameters.Errors.Keys.Aggregate(string.Empty,
			//	(current, key) => current + (_russianFields[key] + _fenceParameters.Errors[key]) + '\n');
			var errorMessage = string.Empty;
			for (var i = 0; i < _errors.Keys.Count; i++)
			{
				var key = _errors.Keys.ToArray()[i];
				errorMessage += _russianFields[key] + _errors[key];
				if (i != _errors.Keys.Count - 1)
				{
					errorMessage += '\n';
				}
			}

			return errorMessage;
		}

		/// <summary>
		/// Построить забор.
		/// </summary>
		private void BuildFence()
		{
			if (HasErrors)
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


		/// <summary>
		/// Добавить ошибку.
		/// </summary>
		/// <param name="propertyName">Имя свойства.</param>
		/// <param name="errorMessage">Сообщение об ошибке.</param>
		private void AddError(string propertyName, string errorMessage)
		{
			if (!_errors.ContainsKey(propertyName))
			{
				_errors.Add(propertyName, errorMessage);
			}
			else
			{
				_errors[propertyName] = errorMessage;
			}

			OnErrorsChanged(propertyName);
		}

		/// <summary>
		/// Удаление ошибки.
		/// </summary>
		/// <param name="propertyName">Имя свойства.</param>
		private void ClearErrors(string propertyName)
		{
			_errors.Remove(propertyName);
			OnErrorsChanged(propertyName);
		}

		/// <summary>
		/// Вызов события изменения ошибки.
		/// </summary>
		/// <param name="propertyName">Имя свойства.</param>
		private void OnErrorsChanged(string propertyName)
		{
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}

		#endregion
	}
}