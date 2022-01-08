using System;

namespace DrawBody.Helpers;

public static class WindowUtils
{
    public static double SCREEN_HEIGHT = System.Windows.SystemParameters.PrimaryScreenHeight;
    public static double SCREEN_WIDTH = System.Windows.SystemParameters.PrimaryScreenWidth;

    /// <summary>
    /// Returns the center of the screen
    /// </summary>
    /// <returns>tuple with x and y value for center of screen</returns>
    public static Tuple<double, double> GetCenter()
        => new Tuple<double, double>(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
}
