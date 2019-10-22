using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public LineRenderer lineR;
    public Transform point0,point1,point2,point3;

    private int numPoints = 50;
    private Vector3[] positions = new Vector3[50];

    public bool isCubic = true;

    [Range(0,2)]
    [Tooltip("This varible increases the amount of curves in the line by adding more points and using a more complex formula")]
    public int numberOfCurves = 0;

	// Use this for initialization
	void Start ()
    {
        CalculateLine();

        cube.transform.position = positions[0];
        StartCoroutine(CubeAnimTest());
    }



    void CalculateLine()
    {
        lineR.positionCount = numPoints;
        Vector3[] points = { point0.position, point1.position, point2.position, point3.position };
        positions = BezierCurve.DrawLine(numberOfCurves, points, numPoints);
        lineR.SetPositions(positions);
    }

    private void FixedUpdate()
    {
        CalculateLine();
    }

    public int frameRate = 24;
    public GameObject cube;
    int cubePos = 0;

    public IEnumerator CubeAnimTest()
    {
        yield return new WaitForSeconds(1/frameRate);
        cube.transform.position = positions[cubePos];

        cubePos++;
        if (cubePos >= positions.Length)
        {
            cubePos = 0;
        }
        StartCoroutine(CubeAnimTest());
    }
}
