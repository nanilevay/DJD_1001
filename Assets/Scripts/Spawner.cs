using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float      minDistanceToSpawn;
    [SerializeField] private float      maxDistanceToSpawn;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform  cameraPosition;

    public void Spawn()
    {
        Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetSpawnPosition()
    {
        RaycastHit2D spawnPosition;
        Vector3 rayOrigin;
        rayOrigin = transform.position + new Vector3
            (Random.Range(0, maxDistanceToSpawn), 0.0f, 0.0f);

        spawnPosition = Physics2D.Raycast(rayOrigin, transform.up * -1.0f, Mathf.Infinity,
            LayerMask.GetMask("Ground"));

        if (Mathf.Abs(cameraPosition.position.x - spawnPosition.point.x)
            > minDistanceToSpawn)
        {
            return spawnPosition.point;
        }

        return GetSpawnPosition();
    }
}
