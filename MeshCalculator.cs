using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCalculator : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    private void Start()
    {
        float totalSurfaceArea = 0;
        for (int i = 0; i < mesh.triangles.Length; i+=3)
        {
            if(i + 2 > mesh.triangles.Length) { break; };
            Vector3 firstEdge = mesh.vertices[mesh.triangles[i + 1]] - mesh.vertices[mesh.triangles[i]];
            Vector3 secondEdge = mesh.vertices[mesh.triangles[i + 2]] - mesh.vertices[mesh.triangles[i + 1]];
            float triangleSurfaceArea = Vector3.Cross(firstEdge, secondEdge).magnitude / 2;
            totalSurfaceArea += triangleSurfaceArea;
        }
    }
}
