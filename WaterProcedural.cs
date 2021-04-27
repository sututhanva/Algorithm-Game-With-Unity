using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProcedural : MonoBehaviour
{
    Mesh mesh;
    [SerializeField] [Range(0,256)] int xSize, zSize;
    [SerializeField] [Range(0, 16)] int ySize;
    Vector3[] vertices;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        ProduceMesh();
    }

    // Update is called once per frame
    void Update()
    {
        PerlinNoiseWave();
        UpdateMesh();
    }

    Vector3[] CreateVertices(int xSize,int ySize, int zSize)
    {
        vertices = new Vector3[xSize * zSize];
        triangles = new int[(xSize - 1) * (zSize - 1) * 6];

        int i = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }
        return vertices;
    }

    int[] CreateTriangle(int xSize, int ySize, int zSize)
    {
        triangles = new int[(xSize - 1) * (zSize - 1) * 6];
        int v = 0;
        for (int row = 0; row < zSize - 1; row++)
        {
            for (int column = 0; column < xSize - 1; column++)
            {
                int xVertical = row * xSize + column;
                triangles[v] = xVertical;
                triangles[v + 1] = xVertical + xSize;
                triangles[v + 2] = xVertical + 1;

                triangles[v + 3] = xVertical + 1;
                triangles[v + 4] = xVertical + xSize;
                triangles[v + 5] = xVertical + xSize + 1;
                v += 6;
            }
        }
        return triangles;
    }

    void ProduceMesh()
    {
        vertices = CreateVertices(xSize, 0, zSize);
        triangles = CreateTriangle(xSize, 0, zSize);
    }

    void SimpleWave()
    {
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                vertices[x + xSize * z] = new Vector3(vertices[x + xSize * z].x, Mathf.Sin(Time.time+x), vertices[x + xSize * z].z);
            }
        }
    }
    void PerlinNoiseWave()
    {
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                Debug.Log(Mathf.PerlinNoise(Time.time * x, Time.time * z));
                vertices[x + xSize * z] = new Vector3(vertices[x + xSize * z].x, Mathf.PerlinNoise(Time.time*x*0.2f, Time.time*z*0.2f), vertices[x + xSize * z].z);
            }
        }
    }


    private void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
