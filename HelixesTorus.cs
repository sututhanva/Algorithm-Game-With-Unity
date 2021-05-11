using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HelixesTorus : MonoBehaviour
{
    public int turncount = 4;
    public float height = 6;
    public float radius = 1f;

    public bool torus = false;

    public Color startColor = Color.magenta;
    public Color endColor = Color.cyan;


    const int POINT_PER_TURN = 64;
    const float TAU = 2 * Mathf.PI;
    private void OnDrawGizmos()
    {
        Helix();
    }

    // Draw Helix by procedural point using Trigonometry
    void Helix()
    {
        int pointCount = turncount * POINT_PER_TURN;
        Vector3[] points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (pointCount - 1f);
            points[i] = torus ? GetTorusCoilPoint(t, height, radius, turncount) : GetLinearCoilPoint(t, height, radius, turncount);

        }
        for (int i = 0; i < pointCount - 1; i++)
        {
            float tHeight = i / (pointCount - 1f);
            Handles.color = Color.Lerp(startColor, endColor, tHeight);
            Handles.DrawAAPolyLine(points[i], points[i+1]);
        }
    }

    Vector3 GetLinearCoilPoint(float t, float height, float radius, float turnCount)
    {
        float tWinding = t * turnCount;
        float angRad = tWinding * TAU;
        Vector3 point = new Vector3(Mathf.Cos(angRad), Mathf.Sin(angRad)) * radius;
        point.z = t * height;
        return point;
    }

    Vector3 GetTorusCoilPoint(float t, float circumference, float radius, float turnCount)
    {
        // C = 2*pi*r | C = Tau * r
        float majorRadius = circumference / TAU;

        float majorAngRad = t * TAU;

        Vector3 minorCenter = new Vector2(Mathf.Cos(majorAngRad), Mathf.Sin(majorAngRad)) * majorRadius;

        float tWinding = t * turnCount;
        float angRad = tWinding * TAU;
        Vector3 point = new Vector3(Mathf.Cos(angRad), Mathf.Sin(angRad)) * radius;

        Vector3 minorCenterX = minorCenter.normalized;
        Vector3 minorCenterY = Vector3.forward; // Z axis

        return minorCenter + point.x * minorCenterX + point.y * minorCenterY;
    }
}
