using System;

namespace DrawBody.Helpers;

public class AngleUtils
{
    public static double DegreeToRadian(double value)
    {
        return value * Math.PI / 180;
    }

}
