using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Helixes : MonoBehaviour
{
    // Amount of helix
    public int helixAmount;

    // total length of helixes
    public float height;

    // radius of every helix
    public float radius;

    const int POINTS_PER_HELIX = 64;
    const float TAU = 2 * Mathf.PI;

    private void OnDrawGizmos()
    {
        Helix();
    }
    void SimpleCircle()
    {
        Vector3 center = transform.position;
        Vector3[] points = new Vector3[POINTS_PER_HELIX];
        for (int i = 0; i < POINTS_PER_HELIX; i++)
        {
            float pointAngle = TAU * i / POINTS_PER_HELIX;
            Vector3 pointPos = new Vector3(center.x + radius * Mathf.Cos(pointAngle), center.y, center.z + radius * Mathf.Sin(pointAngle));
            points[i] = pointPos;
        }


        for (int i = 0; i < points.Length; i++)
        {
            Color lineColor = Color.Lerp(Color.magenta, Color.cyan, (float)i / (float)points.Length);
            Handles.color = lineColor;
            Handles.DrawAAPolyLine(points[i], points[i + 1 % points.Length]);
        }
    }
    // Draw Helix by using Bezier Curves
    void HelixByBezier()
    {
        int indexHelix = 0;
        Vector3 center = transform.position;
        Vector3 before = new Vector3(center.x, center.y, center.z - radius);
        Vector3 next;
        while (indexHelix < helixAmount)
        {
            Debug.Log(Mathf.Cos(Mathf.PI * indexHelix) * radius);
            // End point of each Bezier
            next = new Vector3(before.x, before.y + height / helixAmount * 2, center.z + Mathf.Cos(Mathf.PI * indexHelix) * radius);

            // Bezier 2 tangent
            Vector3 startTangent = new Vector3(before.x + Mathf.Cos(Mathf.PI * indexHelix) * radius, before.y, before.z);
            Vector3 endTangent = new Vector3(next.x + Mathf.Cos(Mathf.PI * indexHelix) * radius, next.y, next.z);
            Handles.DrawBezier(before, next, startTangent, endTangent, Color.Lerp(Color.magenta, Color.cyan, (float)indexHelix / (float)helixAmount), null, 2f);

            // Save end point of current bezier is start point of next bezier
            before = next;
            indexHelix = indexHelix + 1;
        }
    }

    // Draw Helix by procedural point using Trigonometry
    void Helix()
    {
        int totalPoint = helixAmount * POINTS_PER_HELIX;
        Vector3[] points = new Vector3[totalPoint];
        Vector3 center = transform.position;

        // Procedural every point of helix
        for (int i = 0; i < totalPoint; i++)
        {
            float pointHeight = height * i / totalPoint;
            float pointAngle =  TAU * i / POINTS_PER_HELIX ;
            Vector3 pointPos = new Vector3(center.x + radius * Mathf.Cos(pointAngle), pointHeight , center.z + radius *  Mathf.Sin(pointAngle));
            points[i] = pointPos;
        }

        for (int i = 0; i < totalPoint-1; i++)
        {
            Color lineColor = Color.Lerp(Color.magenta, Color.cyan, (float)i / (float)totalPoint);
            Handles.color = lineColor;
            Handles.DrawAAPolyLine(points[i], points[i + 1]);
        }
    }
}
