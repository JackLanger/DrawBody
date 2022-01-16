using DrawBody.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace DrawBody.Models;
/// <summary>
/// A Torus is a donut like shaped 3D object. 
/// </summary>
public class Torus
{

    /// <summary>
    /// The Origin at which the torus is located.
    /// </summary>
    private Vector3D origin;
    public Vector3D Origin
    {
        get { return origin; }
        set { origin = value; }
    }


    /// <summary>
    /// Constructor for this class, creates a torus at the origin where origin is (0|0|0)
    /// </summary>
    public Torus() : this(new Vector3D(0, 0, 0)) { }
    /// <summary>
    /// Constructor for this class takes an origin as parameter 
    /// </summary>
    /// <param name="origin">the origin of the torus</param>
    public Torus(Vector3D origin)
    {
        this.origin = origin;
    }

    /// <summary>
    /// Calculate the vertices of the Torus.
    /// </summary>
    /// /// <returns>a collection of vertices</returns>
    public List<Vector3D> CalculateTorus()
      => CalculateTorus(200, 50, 100, 36);


    /// <summary>
    /// Calculate the vertices of the Torus.
    /// </summary>
    /// <param name="radius">the radius of the torus</param>
    /// <param name="ringRadius">the ring radius of the torus</param>
    /// <returns>a collection of vertices</returns>
    public List<Vector3D> CalculateTorus(double radius, double ringRadius)
      => CalculateTorus(radius, ringRadius, 100, 36);


    public List<Vector3D> CalculateTorus(double radius, double ringRadius, int detail, int ringDetail)
    {
        List<Vector3D> vertices = new();

        var step = Helpers.AngleUtils.DegreeToRadian(360.0 / detail);     // convert from radials to degree
        double ringAngleStep = 360.0 / ringDetail;       // step size for the rings


        var ringPointStep = 360.0 / ringDetail;

        for (int i = 0; i < detail; i++)
        {
            var point = new Vector3D(radius * (Math.Sin(step * i)), radius * Math.Cos(step * i), 0);

            for (int r = 0; r < ringDetail; r++)
            {

                var ringAngle = AngleUtils.DegreeToRadian(ringPointStep * r);
                point.Normalize();
                var subVector = new Vector3D(Math.Cos(ringAngle) * point.X,
                                             Math.Cos(ringAngle) * point.Y,
                                             Math.Sin(ringAngle));
                subVector *= ringRadius;
                subVector += origin + point * radius;

                vertices.Add(subVector + point);
            }
        }

        return vertices;
    }

    /// <summary>
    /// Calculates the vector for the from the center of the ring to the point along the ring. 
    /// The vector is the normalized vector towards the origin and multiplied by the restrictive values for each dimension.
    /// </summary>
    /// <param name="center">the center of the ring</param>
    /// <param name="angle">the angle at which the vector has to be rotated along the circle</param>
    /// <param name="radius">the radius of the circle</param>
    /// <returns>a Vector3D pointing to the point on the ring</returns>
    private Vector3D CalculateVector(Vector3D center, double angle, double radius)
    {
        Vector3D vector = center;
        vector.Normalize();

        double cos = Math.Round(Math.Cos(angle), 3);
        double sin = Math.Round(Math.Sin(angle), 3);

        vector.X *= cos * radius;
        vector.Y *= cos * radius;
        vector.Z = sin * radius;

        return center - vector;
    }

}
