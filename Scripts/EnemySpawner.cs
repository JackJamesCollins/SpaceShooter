using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    WaveConfigSO currentWave;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;//share serialised filed with PathFinder so theyre the same, can have SField in both but run the risk of not putting same thing in both
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {                                   //(what you wanna spawn, where you wanna spawn, rotation-for no rotation put Quaternion.identity, transform of where you want it stored so just put transform puts it under enemy spawner game object)
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform);//puts enemy in game as you press play
                                        //Waitforseconds is built in method                                 //.Euler can change trsnform of object as its made if ship isnt round you might want it facing a certain direction. this also makes them shoot down instead of up
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());//continue loop after random seconds to spawn again
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while(isLooping);//if this is true then after gone through list of waves, it repeats going through them again so spawns same wavaes again in order
    }

}
