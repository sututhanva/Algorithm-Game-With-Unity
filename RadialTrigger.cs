using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float radiusDetect = 10f;

    private Vector3 enemyVector;
    private Vector3 towerVector;
    // Update is called once per frame
    void Update()
    {
        enemyVector = enemy.transform.position;
        towerVector = transform.position;

        // Vector between toward from tower to enemy
        Vector3 vectTowerToEnemy = enemyVector - towerVector;
        Vector3 dirAToE = vectTowerToEnemy.normalized;

        EnemyDetecter(vectTowerToEnemy);
    }

    // Detect when enemy move inside tower radius
    private void EnemyDetecter(Vector3 vectTowerToEnemy)
    {
        // Length from tower to enemy doesn't sqrt yet
        float lengthAbsTowerToEnemy = (vectTowerToEnemy.x * vectTowerToEnemy.x) + (vectTowerToEnemy.y * vectTowerToEnemy.y);
        float radiusDetectAbs = radiusDetect * radiusDetect;

        // Check is Enemy inside radius of tower or not?
        bool isEnemyInTowerRange = radiusDetectAbs >= lengthAbsTowerToEnemy;
        if (isEnemyInTowerRange)
        {
            // Do sth when enemy inside tower radius
            enemy.GetComponentInChildren<Renderer>().material.color = Color.green;
        }
    }

    // Dev test with Gizmos
    private void OnDrawGizmos()
    {
        enemyVector = enemy.transform.position;
        towerVector = transform.position;
        Vector3 vectAToE = enemyVector - towerVector;
        Vector3 dirAToE = vectAToE.normalized;
        Debug.Log(dirAToE);

        // Draw tower radius
        Gizmos.DrawWireSphere(towerVector, radiusDetect);
    }
}
