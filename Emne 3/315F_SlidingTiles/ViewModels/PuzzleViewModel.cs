using System.Collections.ObjectModel;
using System.Windows;
using _315F_SlidingTiles.Models;
using CommunityToolkit.Mvvm.Input;

namespace _315F_SlidingTiles.ViewModels;

public class PuzzleViewModel
{
	private const int Size = 4;
	private const int ShuffleAmount = 500;

	private readonly Random _random = new();

	public PuzzleViewModel()
	{
		TileClickCommand = new RelayCommand<Tile>(MoveTile);

		InitBoard();

		Shuffle();
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

	private void Shuffle()
	{
		for (var i = 0; i < ShuffleAmount; i++)
		{
			var empty = GetEmptyTile();
			var movableTiles = Tiles.Where(t => !t.IsEmpty && CheckTilesAdjacent(empty, t)).ToList();

			var randomTile = movableTiles[_random.Next(movableTiles.Count)];

			SwapTiles(randomTile, empty);
		}
	}

	private void MoveTile(Tile? tile)
	{
		if (tile == null || tile.IsEmpty)
			return;

		var empty = GetEmptyTile();

		if (!CheckTilesAdjacent(empty, tile)) return;

		SwapTiles(empty, tile);

		CheckWin();
	}

	private Tile GetEmptyTile()
	{
		return Tiles.First(t => t.IsEmpty);
	}

	private bool CheckTilesAdjacent(Tile a, Tile b)
	{
		return (Math.Abs(a.Row - b.Row) == 1 && a.Col == b.Col) ||
		       (Math.Abs(a.Col - b.Col) == 1 && a.Row == b.Row);
	}

	private void SwapTiles(Tile a, Tile b)
	{
		(a.Value, b.Value) = (b.Value, a.Value);
	}

	private void CheckWin()
	{
		for (var i = 0; i < Tiles.Count - 1; i++)
			if (Tiles[i].Value != i + 1)
				return;

		if (!Tiles.Last().IsEmpty) return;

		MessageBox.Show("You win!");
	}
}