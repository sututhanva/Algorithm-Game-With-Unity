using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FOVCamera3D : MonoBehaviour
{
    [SerializeField] List<GameObject> objs;
    private void OnDrawGizmos()
    {
        Camera cam = GetComponent<Camera>();

        Vector2 camDir = Vector2.right;

        float maxAngleRad = float.MinValue; // lowest dot product
        Vector3 maxPoint = default;

        foreach (GameObject obj in objs)
        {
            // Transform to local space
            Vector3 pointLocal = cam.transform.InverseTransformPoint(obj.transform.position);

            // flat 3D-XYZ to 2D-ZY space
            Vector2 pointFlat = new Vector2(pointLocal.z, pointLocal.y);
            Vector2 camToPoint = pointFlat.normalized; // point - (vector zero) => still point

            float angleRad = Mathf.Acos(Vector2.Dot(camDir, camToPoint));
            if(angleRad > maxAngleRad)
            {
                maxAngleRad = angleRad;
                maxPoint = obj.transform.position;
            }
        }

        Handles.DrawLine(cam.transform.position, maxPoint);
        cam.fieldOfView = maxAngleRad * Mathf.Rad2Deg * 2;
    }
}
