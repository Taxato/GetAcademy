using CommunityToolkit.Mvvm.ComponentModel;

namespace _315F_SlidingTiles.Models;

public partial class Tile : ObservableObject
{
	[ObservableProperty] public partial int Value { get; set; }

	public int Row { get; init; }
	public int Col { get; init; }

	public bool IsEmpty => Value == 0;

	// ReSharper disable UnusedParameterInPartialMethod
	partial void OnValueChanged(int oldValue, int newValue)
	{
		OnPropertyChanged(nameof(IsEmpty));
	}
}