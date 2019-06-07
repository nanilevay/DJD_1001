using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected float      minDistanceToSpawn;
    [SerializeField] protected float      maxDistanceToSpawn;
    [SerializeField] private GameObject   enemyPrefab;
    [SerializeField] protected int        amountToSpawn;
    public virtual void Spawn()
    {
        Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetSpawnPosition()
    {
        RaycastHit2D spawnPosition;
        Vector3 rayOrigin;
        rayOrigin = transform.position + new Vector3
            (Random.Range(-maxDistanceToSpawn, maxDistanceToSpawn), 0.0f, 0.0f);
        rayOrigin.y = 1000.0f;

        spawnPosition = Physics2D.Raycast(rayOrigin, transform.up * -1.0f, Mathf.Infinity,
            LayerMask.GetMask("Ground"));

        if (Mathf.Abs(transform.position.x - spawnPosition.point.x)
            > minDistanceToSpawn)
        {
            return spawnPosition.point;
        }

        return GetSpawnPosition();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToSpawn);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, maxDistanceToSpawn);
    }
}
