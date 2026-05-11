using CommunityToolkit.Mvvm.ComponentModel;

namespace _315F_SlidingTiles.Models;

public partial class Tile : ObservableObject
{
	[ObservableProperty] public partial int Value { get; set; }
	[ObservableProperty] public partial double X { get; set; }
	[ObservableProperty] public partial double Y { get; set; }


	public int Row { get; set; }
	public int Col { get; set; }

	public bool IsEmpty => Value == 0;

	// ReSharper disable UnusedParameterInPartialMethod
	partial void OnValueChanged(int oldValue, int newValue)
	{
		OnPropertyChanged(nameof(IsEmpty));
	}
}