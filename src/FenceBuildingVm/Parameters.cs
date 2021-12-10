namespace FenceBuildingVm
{
	/// <summary>
	/// Перечисление параметров забора.
	/// </summary>
	public enum Parameters
	{
		/// <summary>
		/// Ширина столбика.
		/// </summary>
		ColumnWidth,

		/// <summary>
		/// Расстояние между нижними столбиками.
		/// </summary>
		DistanceLowerBaffles,

		/// <summary>
		/// Расстояние между верхними столбиками.
		/// </summary>
		DistanceUpperBaffles,

		/// <summary>
		/// Длина забора.
		/// </summary>
		FenceLength,

		/// <summary>
		/// Глубина погружения забора.
		/// </summary>
		ImmersionDepth,

		/// <summary>
		/// Высота верхней (видимой) части забора.
		/// </summary>
		TopFenceHeight,
	}
}