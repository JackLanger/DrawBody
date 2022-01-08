﻿using DrawBody.Helpers;
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
    public List<List<Vector3D>> CalculateTorus()
      => CalculateTorus(200, 50, 100, 36);


    /// <summary>
    /// Calculate the vertices of the Torus.
    /// </summary>
    /// <param name="radius">the radius of the torus</param>
    /// <param name="ringRadius">the ring radius of the torus</param>
    /// <returns>a collection of vertices</returns>
    public List<List<Vector3D>> CalculateTorus(double radius, double ringRadius)
      => CalculateTorus(radius, ringRadius, 100, 36);


    /// <summary>
    /// Calculate the vertices of the Torus.
    /// </summary>
    /// <param name="radius">the radius of the torus</param>
    /// <param name="ringRadius">the radius of the rings of the torus</param>
    /// <param name="detail">the detail at which the torus is calculated / drawn</param>
    /// <returns>a collection of vertices</returns>
    public List<List<Vector3D>> CalculateTorus(double radius, double ringRadius, int detail, int ringDetail)
    {
        Vector3D[] circle = new Vector3D[detail + 1];
        var step = Helpers.AngleUtils.DegreeToRadian(360.0 / detail);     // convert from radials to degree
        List<List<Vector3D>> torus = new();

        for (int i = 0; i <= detail; i++)
        {
            var angle = step * i;
            Vector3D result = new Vector3D(radius * Math.Cos(angle), Math.Sin(angle) * radius, 0);
            result += origin;
            circle[i] = result;


            List<Vector3D> ring = new();
            for (int j = 0; j < ringDetail; j++)
            {
                double subangle = AngleUtils.DegreeToRadian(j * 10);
                ring.Add(CalculateVector(result, subangle, ringRadius));
            }
            torus.Add(ring);
        }
        return torus;
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
        vector.Z = vector.Z == 0 ? 1 : vector.Z;

        vector.X *= Math.Cos(angle) * radius;
        vector.Y *= Math.Cos(angle) * radius;
        vector.Z *= Math.Sin(angle) * radius;

        return vector + center;
    }

}