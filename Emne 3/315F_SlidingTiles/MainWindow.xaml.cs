using _315F_SlidingTiles.ViewModels;

namespace _315F_SlidingTiles;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
	public MainWindow()
	{
		InitializeComponent();
		DataContext = new PuzzleViewModel();
	}
}