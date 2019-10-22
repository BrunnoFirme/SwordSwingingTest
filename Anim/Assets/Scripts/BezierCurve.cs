using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurve 
{
    /// <summary>
    /// Calculates the positions of points on a curve
    /// </summary>
    /// <param name="numberOfCurves">Amount of extra data points used to modify the curve Max: 2</param>
    /// <param name="curveModPoints">Points that modify the curve (creates a pull towards their location)</param>
    /// <param name="numPoints">The total number of points in the animation. less = fast, more = slow</param>
    /// <returns></returns>
    public static Vector3[] DrawLine(int numberOfCurves, Vector3[] curveModPoints, int numPoints)
    {
        switch (numberOfCurves)
        {
            default:
                return DrawLinearCurve(numPoints, curveModPoints);
            case 1:
                return DrawQuadraticCurve(numPoints, curveModPoints);
            case 2:
                return DrawCubicCurve(numPoints, curveModPoints);
        }
    }

    private static Vector3[] DrawLinearCurve(int numPoints, Vector3[] posArray)
    {
        Vector3[] positions = new Vector3[numPoints];

        positions[0] = CalculateLinearBezierPoint(0, posArray[0], posArray[1]);
        for (int i = 1; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = CalculateLinearBezierPoint(t, posArray[0], posArray[1]);
        }

        return positions;
    }

    private static Vector3[] DrawQuadraticCurve(int numPoints, Vector3[] posArray)
    {
        Vector3[] positions = new Vector3[numPoints];

        positions[0] = CalculateQuadraticBezierPoint(0, posArray[0], posArray[1], posArray[2]);
        for (int i = 1; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = CalculateQuadraticBezierPoint(t, posArray[0], posArray[1], posArray[2]);
        }

        return positions;
    }

    private static Vector3[] DrawCubicCurve(int numPoints, Vector3[] posArray)
    {
        Vector3[] positions = new Vector3[numPoints];

        positions[0] = CalculateCubicBezierPoint(0, posArray[0], posArray[1], posArray[2], posArray[3]);
        for (int i = 1; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = CalculateCubicBezierPoint(t, posArray[0], posArray[1], posArray[2], posArray[3]);
        }

        return positions;
    }

    private static Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    private static Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return (Mathf.Pow((1 - t), 2) * p0 + (2 * (1 - t) * t * p1) + Mathf.Pow(t, 2) * p2);
    }

    private static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (Mathf.Pow((1 - t), 3) * p0 + (3 * Mathf.Pow((1 - t), 2) * t * p1) + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3);
    }
}
