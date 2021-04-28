using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GeometricShapes : MonoBehaviour
{
    [SerializeField][Min(3)] int numberVertices = 3;
    [SerializeField] [Min(1)] int density = 1;
    Vector3[] vertices;
    Vector3 trfPos;
    private void OnDrawGizmos()
    {
        vertices = new Vector3[numberVertices];
        trfPos = transform.position;
        DrawGeometric(density);
    }

    private void DrawGeometric()
    {
        float tau = 2 * Mathf.PI;
        float piece = tau / numberVertices;
        if (numberVertices >= 3)
        {
            for (int i = 0; i < numberVertices; i++)
            {
                vertices[i] = new Vector3(trfPos.x + Mathf.Cos(piece * (i + 1)), trfPos.y + Mathf.Sin(piece * (i + 1)), trfPos.z + 0);
            }
            for (int i = 0; i < numberVertices - 1; i++)
            {
                int nextVertical = i + 1;
                if (i + 1 > (numberVertices - 1))
                {
                    nextVertical = nextVertical - numberVertices;
                }
                Handles.DrawAAPolyLine(vertices[i], vertices[nextVertical]);
            }
        }
    }

    private void DrawGeometric(int density)
    {
        float tau = 2 * Mathf.PI;
        float piece = tau / numberVertices;
        if (numberVertices >= 3)
        {
            for (int i = 0; i < numberVertices; i++)
            {
                vertices[i] = new Vector3(trfPos.x + Mathf.Cos(piece * (i + 1)), trfPos.y + Mathf.Sin(piece * (i + 1)), trfPos.z + 0);
            }
            for (int i = 0; i < numberVertices; i++)
            {
                int nextVertical = i + density;
                if (i + density > (numberVertices -1))
                {
                    nextVertical = nextVertical - numberVertices;
                }
                Handles.DrawLine(vertices[i], vertices[nextVertical]);
            }
        }
    }
}
