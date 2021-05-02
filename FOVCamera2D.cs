using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FOVCamera2D : MonoBehaviour
{
    [SerializeField] List<Transform> trfObjects = new List<Transform>();
    private void OnDrawGizmos()
    {
        Camera camera = GetComponent<Camera>();
        Vector2 cameraPos = transform.position;
        float minDot = float.MaxValue;
        foreach (var trfObj in trfObjects)
        {
            Vector2 objPos = trfObj.position;
            Vector2 cameraToObj = objPos - cameraPos;
            float dot = Vector2.Dot(cameraToObj.normalized, transform.forward);
            if(minDot > dot)
            {
                minDot = dot;
            }
        }
        float angleRad = Mathf.Acos(minDot);
        float fovCamDegree = angleRad * Mathf.Rad2Deg * 2;
        camera.fieldOfView = fovCamDegree;
        
        Handles.DrawLine(cameraPos, trfObjects[0].position);
    }
}
