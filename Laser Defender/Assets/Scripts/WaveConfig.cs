using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemy = 5;
    [SerializeField] private float moveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    
    public List<Transform> GetWaypoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }
    
    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    
    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }
    
    public int GetNumberOfEnemy()
    {
        return numberOfEnemy;
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    
    
}
