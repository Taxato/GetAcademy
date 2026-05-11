using System.Collections.ObjectModel;
using _315F_SlidingTiles.Models;
using CommunityToolkit.Mvvm.Input;

namespace _315F_SlidingTiles.ViewModels;

public class PuzzleViewModel
{
	private const int Size = 4;

	public PuzzleViewModel()
	{
		TileClickCommand = new RelayCommand<Tile>(MoveTile);

		InitBoard();
	}

	public ObservableCollection<Tile> Tiles { get; } = [];

	public IRelayCommand<Tile> TileClickCommand { get; }

	private void InitBoard()
	{
		Tiles.Clear();

		var value = 1;

		for (var r = 0; r < Size; r++)
		for (var c = 0; c < Size; c++)
			if (r == Size - 1 && c == Size - 1) Tiles.Add(new Tile { Value = 0, Row = r, Col = c });
			else Tiles.Add(new Tile { Value = value++, Row = r, Col = c });
	}

	private void MoveTile(Tile? tile)
	{
		if (tile == null || tile.IsEmpty) return;

		var empty = Tiles.First(t => t.IsEmpty);

		var isAdjacent = (Math.Abs(tile.Row - empty.Row) == 1 && tile.Col == empty.Col) ||
		                 (Math.Abs(tile.Col - empty.Col) == 1 && tile.Row == empty.Row);

		if (!isAdjacent) return;

		// Swap values
		Console.WriteLine(tile.Value);
		(tile.Value, empty.Value) = (empty.Value, tile.Value);
	}
}