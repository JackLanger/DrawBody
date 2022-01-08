namespace DrawBody.Models;
using Helpers;
using System;
using System.Collections.Generic;

public class Circle
{
    /// <summary>
    /// Calculates the points of a circle according to the detail and radius provieded.
    /// </summary>
    /// <param name="detail">teh number of points of a circle, the more points the smoother the circle</param>
    /// <param name="radius">the radius of the circle</param>
    /// <returns>an Array list of coordinates</returns>
    public IEnumerable<Tuple<double, double>> CalculatePoints(int detail, double radius)
    {
        double step = 400 / detail;         // breaks if is less than 400 ??

        Tuple<double, double>[] coordinates = new Tuple<double, double>[detail];

        for (int i = 0; i < detail; i++)
        {
            double angle = AngleUtils.DegreeToRadian(i * step);
            double x = Math.Cos(angle) * radius + WindowUtils.GetCenter().Item1;
            double y = Math.Sin(angle) * radius + WindowUtils.GetCenter().Item2;
            coordinates[i] = new Tuple<double, double>(x, y);
        }
        return coordinates;
    }

    /// <summary>
    /// draws a circle in 2D and tilts it by the provided angle
    /// </summary>
    /// <param name="original">the original 2D data</param>
    /// <param name="tilt">the angle the circle has to be rotated or tilted</param>
    /// <returns>the corrected 2D values to draw to the canvas</returns>
    public static IEnumerable<Tuple<double, double>> Rotate(IEnumerable<Tuple<double, double>> original, double tilt = 60)
    {

        List<Tuple<double, double>> coordinates = new();
        tilt = AngleUtils.DegreeToRadian(tilt);
        double centerY = WindowUtils.GetCenter().Item2;
        foreach (var val in original)
        {
            double x = val.Item1;

            double y = (val.Item2 - centerY) * Math.Cos(tilt) + centerY;

            coordinates.Add(new Tuple<double, double>(x, y));
        }

        return coordinates;
    }
}
