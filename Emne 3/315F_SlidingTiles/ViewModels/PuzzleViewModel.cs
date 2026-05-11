using System.Collections.ObjectModel;
using System.Windows;
using _315F_SlidingTiles.Models;
using CommunityToolkit.Mvvm.Input;

namespace _315F_SlidingTiles.ViewModels;

public class PuzzleViewModel
{
	private const int GridSize = 4;
	private const double Gap = 5;
	private const int ShuffleAmount = 500;


	private readonly Random _random = new();

	public PuzzleViewModel()
	{
		TileClickCommand = new RelayCommand<Tile>(MoveTile);

		InitBoard();

		Shuffle();
	}

	private static double TileSize => 90;


	public double BoardSize =>
		GridSize * TileSize + (GridSize - 1) * Gap;

	public ObservableCollection<Tile> Tiles { get; } = [];

	public IRelayCommand<Tile> TileClickCommand { get; }

	private void InitBoard()
	{
		Tiles.Clear();

		var value = 1;

		for (var r = 0; r < GridSize; r++)
		for (var c = 0; c < GridSize; c++)
			if (r == GridSize - 1 && c == GridSize - 1)
				Tiles.Add(new Tile
				{
					Value = 0,
					Row = r,
					Col = c,
					X = c * (TileSize + Gap),
					Y = r * (TileSize + Gap)
				});
			else
				Tiles.Add(new Tile
				{
					Value = value++,
					Row = r,
					Col = c,
					X = c * (TileSize + Gap),
					Y = r * (TileSize + Gap)
				});
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

		SwapTiles(tile, empty);

		CheckWin();
	}

	private Tile GetEmptyTile()
	{
		return Tiles.First(t => t.IsEmpty);
	}

	private static bool CheckTilesAdjacent(Tile a, Tile b)
	{
		return (Math.Abs(a.Row - b.Row) == 1 && a.Col == b.Col) ||
		       (Math.Abs(a.Col - b.Col) == 1 && a.Row == b.Row);
	}

	private static void SwapTiles(Tile tile, Tile empty)
	{
		var oldRow = tile.Row;
		var oldCol = tile.Col;

		tile.Row = empty.Row;
		tile.Col = empty.Col;

		tile.X = tile.Col * (TileSize + Gap);
		tile.Y = tile.Row * (TileSize + Gap);

		empty.Row = oldRow;
		empty.Col = oldCol;

		empty.X = empty.Col * (TileSize + Gap);
		empty.Y = empty.Row * (TileSize + Gap);
	}

	private void CheckWin()
	{
		foreach (var tile in Tiles)
		{
			if (tile.IsEmpty)
				continue;

			var expectedValue =
				tile.Row * GridSize + tile.Col + 1;

			if (tile.Value != expectedValue)
				return;
		}

		var empty = GetEmptyTile();
		if (empty is { Row: GridSize - 1, Col: GridSize - 1 }) MessageBox.Show("You win!");
	}
}