using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BouncingLaser : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 laserPos = transform.position;
        Vector3 laserDir = transform.forward;
        Reflect(laserPos, laserDir);
    }

    void Reflect(Vector3 laserPos, Vector3 laserDir)
    {
        if (Physics.Raycast(laserPos, laserDir, out RaycastHit hit))
        {
            Vector3 hitPoint = hit.point;
            Vector3 sideVector = Vector3.Cross(laserDir, hit.normal);
            Vector3 reflect = Vector3.Cross(sideVector, laserDir);
            Handles.color = Color.red;
            Handles.DrawAAPolyLine(laserPos, hitPoint);
            Handles.DrawAAPolyLine(hitPoint, reflect + hitPoint);
            Reflect(hitPoint, reflect);
        }
        else return;
    }
}
