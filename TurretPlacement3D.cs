using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretPlacement3D : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 headPos = transform.position;
        Vector3 lookDir = transform.forward;

        Matrix4x4 matrix = transform.localToWorldMatrix;

        Vector3[] boundTurret = {
            new Vector3( 1, 0, 1 ),
            new Vector3( -1, 0, 1 ),
            new Vector3( -1, 0, -1 ),
            new Vector3( 1, 0, -1 ),
            new Vector3( 1, 2, 1 ),
            new Vector3( -1, 2, 1 ),
            new Vector3( -1, 2, -1 ),
            new Vector3( 1, 2, -1 )
        };

        if (Physics.Raycast(headPos, lookDir, out RaycastHit hit))
        {
            Vector3 hitPos = hit.point;
            Vector3 hitNormal = hit.normal;
            Vector3 sideVector = Vector3.Cross(lookDir, hitNormal);
            Vector3 reflect = Vector3.Cross(hitNormal, sideVector);

            Handles.color = Color.red;
            Handles.DrawAAPolyLine(headPos, hitPos);
            Handles.color = Color.cyan;
            Handles.DrawAAPolyLine(hitPos, hitPos + reflect.normalized);

            matrix.SetColumn(3, new Vector4(hitPos.x, hitPos.y, hitPos.z, 1));

            for (int i = 0; i < boundTurret.Length; i++)
            {
                boundTurret[i] = matrix.MultiplyPoint(boundTurret[i]);
            }

            Handles.color = Color.green;
            for (int i = 0; i < boundTurret.Length/2 -1; i++)
            {
                Handles.DrawAAPolyLine(boundTurret[i], boundTurret[i+1]);
                Handles.DrawAAPolyLine(boundTurret[i], boundTurret[i + 4]);
                Handles.DrawAAPolyLine(boundTurret[i+4], boundTurret[i + 4 + 1]);
            }
            Handles.DrawAAPolyLine(boundTurret[0], boundTurret[3], boundTurret[7], boundTurret[4]);
        }
    }
}
