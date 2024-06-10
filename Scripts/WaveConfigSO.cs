using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]//when making new wave, this names them
public class WaveConfigSO : ScriptableObject//scriptable objects
{
    [SerializeField] Transform pathPrefab;//prefab of path we made with yellow points
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] List<GameObject> enemyPrefabs;//loop through this in enemy spawner then instantiating (creating at game play) enemies

    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;//add or subtract from timeBteweenEnemeyspawns so its semi random
    [SerializeField] float minSpawnTime = 0.2f;//used to prevent neg time if random variance > spawntime
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);//gets first way point of path. first object in path (first child)
    }

    public List<Transform> GetWaypoints()//return list of waypoints in path prefab
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);//add all waypoints (children of pathPrefab) to new list called waypoints then return that list
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];//return enemy at index asked for
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);//(what you wanna clamp, min,max)
                                                    //float.Max is max number a float can hold
    }
}
