using DrawBody.controller;
using System.Windows;

namespace DrawBody;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Canvas Controller to get access to the canvas but still be able to follow the MVVM pattern loosly.
    /// </summary>
    private CanvasController canvasController;
    public MainWindow()
    {
        InitializeComponent();
        canvasController = new CanvasController(canvas);
        DataContext = canvasController;
    }
}
