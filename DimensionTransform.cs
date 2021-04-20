using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionTransform : MonoBehaviour
{
    [SerializeField] Vector2 localSpacePoint;
    [SerializeField] Transform worldSpaceTransform;
    [SerializeField] Vector2 worldSpacePoint;
    private void OnDrawGizmos()
    {
        Vector2 room = transform.position;
        Vector2 right = transform.right;
        Vector2 up = transform.up;

        Vector2 LocalToWorld(Vector2 localPt){

            Vector2 roomToPlayerVect = right * localPt.x + up * localPt.y;
            return room + roomToPlayerVect;
        }

        Vector2 WorldToLocal(Vector2 worldPt)
        {
            Vector2 localToPlayerVect = worldPt - room ;
            float x = Vector2.Dot(localToPlayerVect, right);
            float y = Vector2.Dot(localToPlayerVect, up);
            return new Vector2(x,y);
        }

        worldSpacePoint = WorldToLocal(worldSpaceTransform.position);

        DrawBasisVector(room, right, up);
        DrawBasisVector(Vector2.zero, Vector2.right, Vector2.up);


        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(worldSpacePoint, 1f);
    }


    void DrawBasisVector(Vector2 root, Vector2 right, Vector2 up)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(root, right);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(root, up);
    }
}
