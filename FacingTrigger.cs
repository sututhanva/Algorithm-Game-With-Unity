using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    void Update()
    {
        Vector3 enemyVect = enemy.transform.position;
        Vector3 towerVect = transform.position;

        // Suppose tower facing to the north
        Vector3 towerDirNorVect = new Vector3(0, 0, 1);

        Vector3 enemyToTowerVect = enemyVect - towerVect;
        Vector3 enemyToTowerNorVect = enemyToTowerVect.normalized;

        // Scalar product of two normilize vector
        float dotProduct = enemyToTowerNorVect.x * towerDirNorVect.x + enemyToTowerNorVect.y * towerDirNorVect.y + enemyToTowerNorVect.z * towerDirNorVect.z;

        // Check tower have enemy insight or not?
        bool isEnemyLookOnTower = dotProduct > 0 ? true : false;

        // Check tower look exactly toward to enemy or not?
        bool isEnemyLookTowardTower = dotProduct == 1 ? true : false;

        if (isEnemyLookOnTower)
        {
            // Tower do sth like slowly rorate cannon on enemy
            Debug.Log("Enemy insight rorating cannon now");
            if (isEnemyLookTowardTower)
            {
                // Do sth like shooting on enemy
                Debug.Log("Cannon ready, fire at will");
            }
        }
    }
}
