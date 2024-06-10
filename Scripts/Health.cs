using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;//script on player and enemy but only wanna shake for player hit so use this bool
    //turn this bool off in inspector in unity for enemy
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();//Camera.main is set thing so already has findobjectoftype in it
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();//if collides it grabs DamageDealer script off what it collided with (if it exists). if no damageDealer then no damage taken, just shows if you hit enemy or not

        if(damageDealer != null)//if its not nothing so if DamageDealer exists on what you collide with (enemy)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();//call hit on thing with damageDealer which is emey so enemy destroyed when touched
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(!isPlayer)//if enemy dies not player then increase score
        {
            scoreKeeper.ModifyScore(score);
            
        }
        else//if player dies then load game over screen
        {
            levelManager.LoadGameOver();
            
        }
        Destroy(gameObject);        
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)//if hit effect we wanna play is attached
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
                    //particles are just an instance attached to game object so thats first bracket part, this is duration or particles being created+lifetime of them we set up in unity. .constantmax as we just have min and max value and didnt use curve 
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null)
        {
            cameraShake.Play();//starts coroutine in CameraShake Script. cameraShake is reference created at top that lets us use CameraShake script
        }

    }
    
}

