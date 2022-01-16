using DrawBody.execution;
using DrawBody.Helpers;
using DrawBody.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace DrawBody.controller;

/// <summary>
/// Canvas Controller to provide functionality to the canvas while decoupeling the view from the logic.
/// </summary>
public class CanvasController : BaseController
{
    /// <summary>
    /// The origin of the System (0|0|0).
    /// </summary>
    private static Vector3D _origin = new Vector3D(WindowUtils.GetCenter().Item1, WindowUtils.GetCenter().Item2, 0);

    private int _TorusDetail = 50;

    public int TorusDetail
    {
        get { return _TorusDetail; }
        set
        {
            _TorusDetail = value;
            OnPropertyChanged();
            UpdateCanvas();
        }
    }

    private int _RingDetail = 25;

    public int RingDetail
    {
        get { return _RingDetail; }
        set
        {
            _RingDetail = value;
            OnPropertyChanged();
            UpdateCanvas();
        }
    }


    /// <summary>
    /// The canvas that will be used to display the bodys.
    /// </summary>
    private Canvas _canvas = new Canvas();
    public Canvas Canvas
    {
        get { return _canvas; }
        set
        {
            _canvas = value;
            OnPropertyChanged();
        }
    }

    private void UpdateCanvas()
    {
        if (Canvas is null)
            return;

        Canvas.Children.Clear();
        DrawGrid();
        DrawTorus();
    }

    /// <summary>
    /// Constructor for this class.
    /// </summary>
    /// <param name="canvas">the canvas to be used</param>
    public CanvasController(Canvas canvas)
    {
        _canvas = canvas;
        DrawGrid();
        UpdateCanvas();
    }

    /// <summary>
    /// Draws the Torus to the screen.
    /// </summary>
    private void DrawTorus()
    {
        Torus torus = new Torus();
        DrawBody(torus.CalculateTorus(200, 50, TorusDetail, RingDetail), RingDetail);
    }

    /// <summary>
    /// if the face is covered it wont be drawn.
    /// </summary>
    /// <param name="normal">the crossporduct of the faces directional vertices</param>
    /// <returns>true if can draw</returns>
    private bool CanDrawFace(Vector3D normal)
    {
        return true;
    }

    private void DrawBody(List<Vector3D> bodyVals, int ringDetail)
    {
        var mod = bodyVals.Count;

        for (int i = 0; i < bodyVals.Count; i++)
        {
            //var first = bodyVals[i];
            //var second = bodyVals[(i + ringDetail) % mod];
            //var third = bodyVals[(i + 1 + ringDetail) % mod];
            //var fourth = bodyVals[(i + 1) % mod];

            var first = bodyVals[i];
            var second = bodyVals[(i + ringDetail) % mod];
            var third = bodyVals[(i + 1 + ringDetail) % mod];
            var fourth = bodyVals[(i + 1) % mod];


            //  1 2 3 3 1 4

            var vectors = new List<Vector3D>() { first, second, third, third, first, fourth };

            DrawPolygon(CorrectValuesFor2D(vectors));
        }
    }


    /// <summary>
    /// Draws a single polygon
    /// </summary>
    /// <param name="points">the points of the polygon</param>
    public void DrawPolygon(Point[] points, Boolean fill = true)
    {
        Polyline polygon = new();
        polygon.Stroke = Brushes.Black;
        polygon.StrokeThickness = .25;

        polygon.Fill = fill ? Brushes.Yellow : Brushes.Transparent;

        polygon.Points = new PointCollection(points);
        polygon.Points.Add(points[0]);
        Canvas.Children.Add(polygon);
    }

    /// <summary>
    /// Draws the origin on the canvas.
    /// </summary>
    /// <param name="thickness">the thickness of the axis</param>
    /// <param name="color">the color at which the origin is to be drawn</param>
    private void DrawOrigin(double thickness, SolidColorBrush color)
    {
        Line yAxis = new();
        Line xAxis = new();
        Line zAxis = new();

        yAxis.Stroke = color;
        yAxis.StrokeThickness = thickness;
        yAxis.X1 = WindowUtils.GetCenter().Item1;
        yAxis.X2 = WindowUtils.GetCenter().Item1;
        yAxis.Y1 = 200;
        yAxis.Y2 = WindowUtils.GetCenter().Item2 * 2 - 200;

        xAxis.Stroke = color;
        xAxis.StrokeThickness = thickness;
        xAxis.X1 = 200;
        xAxis.X2 = WindowUtils.GetCenter().Item1 * 2 - 200;
        xAxis.Y1 = WindowUtils.GetCenter().Item2;
        xAxis.Y2 = WindowUtils.GetCenter().Item2;

        zAxis.Stroke = color;
        zAxis.StrokeThickness = thickness;
        zAxis.X1 = WindowUtils.GetCenter().Item1 * 2 - 500;
        zAxis.X2 = 500;
        zAxis.Y1 = 200;
        zAxis.Y2 = WindowUtils.GetCenter().Item2 * 2 - 200;

        Canvas.Children.Add(xAxis);
        Canvas.Children.Add(yAxis);
        Canvas.Children.Add(zAxis);
    }
    /// <summary>
    /// Draw a base grid.
    /// </summary>
    private void DrawGrid()
    {
        DrawOrigin(1.15, Brushes.DarkGray);
        double offset = 50.0;
        var color = Brushes.LightGray;
        var thickness = 1;

        for (int i = 0; i < 15; i++)
        {
            Line xOne = new Line();
            Line xTwo = new Line();
            Line yOne = new Line();
            Line yTwo = new Line();
            xOne.StrokeThickness = thickness;
            xTwo.StrokeThickness = thickness;
            yOne.StrokeThickness = thickness;
            yTwo.StrokeThickness = thickness;
            xOne.Stroke = color;
            xTwo.Stroke = color;
            yOne.Stroke = color;
            yTwo.Stroke = color;

            var totalOffset = i * offset;

            xOne.X1 = WindowUtils.SCREEN_WIDTH - 500;
            xOne.X2 = 500;
            xOne.Y1 = 200;
            xOne.Y2 = WindowUtils.SCREEN_HEIGHT - 200;

            xOne.X1 += totalOffset;
            xOne.X2 += totalOffset;

            xTwo.X1 = WindowUtils.SCREEN_WIDTH - 500;
            xTwo.X2 = 500;
            xTwo.Y1 = 200;
            xTwo.Y2 = WindowUtils.SCREEN_HEIGHT - 200;
            xTwo.X1 -= totalOffset;
            xTwo.X2 -= totalOffset;

            yOne.X1 = 100;
            yOne.X2 = WindowUtils.SCREEN_WIDTH - 100;
            yOne.Y1 = WindowUtils.GetCenter().Item2;
            yOne.Y2 = WindowUtils.GetCenter().Item2;
            yTwo.X1 = 100;
            yTwo.X2 = WindowUtils.SCREEN_WIDTH - 100;
            yTwo.Y1 = WindowUtils.GetCenter().Item2;
            yTwo.Y2 = WindowUtils.GetCenter().Item2;

            yOne.Y1 += totalOffset / 2;
            yOne.Y2 += totalOffset / 2;
            yOne.X1 -= totalOffset / 2;
            yOne.X2 -= totalOffset / 2;
            yTwo.Y1 -= totalOffset / 2;
            yTwo.Y2 -= totalOffset / 2;
            yTwo.X1 += totalOffset / 2;
            yTwo.X2 += totalOffset / 2;

            Canvas.Children.Add(xOne);
            Canvas.Children.Add(yOne);
            Canvas.Children.Add(xTwo);
            Canvas.Children.Add(yTwo);
        }

    }

    /// <summary>
    /// Rotate the the view on the canvas.
    /// </summary>
    /// <param name="alpha"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void Rotate(double alpha) => throw new NotImplementedException();

    /// <summary>
    /// Move the Screen arround.
    /// </summary>
    /// <param name="vector">the vectro that needs to be added to the current position of the screen</param>
    /// <exception cref="NotImplementedException"></exception>
    private void Move(Vector3D vector) => throw new NotImplementedException();

    /// <summary>
    /// Zoom in or out
    /// </summary>
    /// <param name="zoom">the zoom coefficient</param>
    private void Zoom(double zoom)
    {
        if (zoom > 0) ZoomIn();
        if (zoom < 0) ZoomOut();
    }

    /// <summary>
    /// Zoom in the view.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void ZoomIn() => throw new NotImplementedException();

    /// <summary>
    /// Zoom out of the view.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void ZoomOut() => throw new NotImplementedException();


    /// <summary>
    /// corrects the vector from the R3 to R2 so we can draw the vector correctly to the screen. 
    /// </summary>
    /// <param name="values">the vertecies to be corrected</param>
    /// <param name="alpha">the angle at which the x axis is positioned, it defaults to 45 degree</param>
    /// <returns>the corrected values as an array of 2D points</returns>
    private Point[] CorrectValuesFor2D(List<Vector3D> values, double alpha = 45)
    {
        alpha = AngleUtils.DegreeToRadian(alpha);
        Point[] result = new Point[values.Count];
        int index = 0;
        foreach (var value in values)
        {
            // due to perspective we draw the y and z axis twice the stepsize of the x axis
            // therefore we need to use this perspective value in the calculation as well 

            // x = 45° => (z)/ sin 45 + x  
            double x = _origin.X + (2 * value.Y + Math.Cos(alpha) * value.X) / 2;

            // y = y +cos 45 * x

            double y = _origin.Y - (2 * value.Z + Math.Sin(alpha) * value.X) / 2;
            // invert the y axis y -> y starts at the bottom,
            // as opposed to the system in which we calculate our values
            // therefore we need to invert the y values and subtract them
            // from the y axis in order to draw it correctly


            result[index++] = new Point(x, y);
        }

        return result;
    }

    /// <summary>
    /// Draw Torus Command to relay the draw torus function.
    /// </summary>
    ICommand _DrawTorusCommand;
    public ICommand DrawTorusCommand => _DrawTorusCommand ??= new RelayCommand(() => DrawTorus());

    ICommand _DrawTriangleCommand;
    public ICommand DrawTriangleCommand => _DrawTriangleCommand ??= new RelayCommand(() => DrawPolygon(new Point[] { new Point(200, 200), new Point(400, 200), new Point(200, 400) }));

}
