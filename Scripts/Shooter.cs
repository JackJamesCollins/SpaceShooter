using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minFiringRate = 0.1f;

    [SerializeField] bool useAI;

    [HideInInspector]public bool isFiring;//we wont change it manually so can hide it
    Coroutine fireCoroutine;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && fireCoroutine == null)//if fire clicked and coroutine returns nothing, i.e- weve just started the game or exited coroutine after firingRate wait  time.
        {
            fireCoroutine = StartCoroutine(FireContinuously());//start coroutine to fire
        }
        else if(!isFiring && fireCoroutine != null)//if not firing and coroutine is still running
        {
            StopCoroutine(fireCoroutine);//stop loop if not firing
            fireCoroutine = null;//change to null so can fire again
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)//infinite loop
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);//create laser while game running

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;//move laser up screen
            }
            
            Destroy(instance, projectileLifetime);//destroy bullet after certain time
            
            //makes shooting at random intervals
            float timeToNextProjectile = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);//clamp values to min firing rate you want and max float value possible

            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);//set firing rate so cant spam, even if hold button they dont shoot like beam
        }
    }
}
