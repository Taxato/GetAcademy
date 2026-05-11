using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace _315F_SlidingTiles.Controls;

public partial class TileControl
{
	private readonly TranslateTransform _transform = new();

	public TileControl()
	{
		InitializeComponent();

		RenderTransform = _transform;

		DataContextChanged += OnDataContextChanged;
	}

	private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is INotifyPropertyChanged oldNpc) oldNpc.PropertyChanged -= Tile_PropertyChanged;

		if (e.NewValue is INotifyPropertyChanged newNpc) newNpc.PropertyChanged += Tile_PropertyChanged;

		UpdatePosition(false);
	}

	private void Tile_PropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName is "X" or "Y") Dispatcher.Invoke(() => UpdatePosition(true));
	}

	private void UpdatePosition(bool animate)
	{
		if (DataContext == null)
			return;

		dynamic tile = DataContext;

		double x = tile.X;
		double y = tile.Y;

		if (!animate)
		{
			_transform.X = x;
			_transform.Y = y;
			return;
		}

		Animate(_transform, TranslateTransform.XProperty, x);
		Animate(_transform, TranslateTransform.YProperty, y);
	}

	private void Animate(DependencyObject target, DependencyProperty property, double to)
	{
		var animation = new DoubleAnimation
		{
			To = to,
			Duration = TimeSpan.FromMilliseconds(120),
			EasingFunction = new CubicEase
			{
				EasingMode = EasingMode.EaseOut
			}
		};

		(target as TranslateTransform)?.BeginAnimation(property, animation);
	}

	#region ICommand Dependency Property

	public static readonly DependencyProperty CommandProperty =
		DependencyProperty.Register(
			nameof(Command),
			typeof(ICommand),
			typeof(TileControl),
			new PropertyMetadata(null));

	public ICommand? Command
	{
		get => (ICommand?)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public static readonly DependencyProperty CommandParameterProperty =
		DependencyProperty.Register(
			nameof(CommandParameter),
			typeof(object),
			typeof(TileControl),
			new PropertyMetadata(null));

	public object? CommandParameter
	{
		get => GetValue(CommandParameterProperty);
		set => SetValue(CommandParameterProperty, value);
	}

	#endregion
}