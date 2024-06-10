using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour//this script is on the enemy prefab so concerns the enemy
{
    EnemySpawner enemySpawner;//just reference to EnemySpawner script
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()//gets wave config from enemy spawner script
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();//sets wave config to same as enemy spawner
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;//move enemy to first waypoint in list
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(waypointIndex < waypoints.Count)//if enemy isnt at end of path
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;//distance moving each frame
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);//this moves enemy. MoveTowards(current postion, target distcance, max distance)
            if(transform.position == targetPosition)//if got to target waypoint
            {
                waypointIndex++;//go to next waypoint
            }
        }
        else
        {
            Destroy(gameObject);//if enemy at end of path then destroy it
        }
    }
}
