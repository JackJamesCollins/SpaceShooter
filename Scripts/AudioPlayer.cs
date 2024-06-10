using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour//call this code in shooter script
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;//range like that makes nice slider

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;//range like that makes nice slider

    static AudioPlayer instance;//static persists through all instances of a class, if multiple audio players instantiated then theyd all share static instance. didnt use this but can do singleton that way. Can do this to make it Glbal so you dont have to "FindObjectOfType.... in other scripts"
    void Awake()
    {
        ManageSingleton();//used to make one instance of something that isnt destroyed between scenes
    }

    void ManageSingleton()
    {               //tells us how many audio players exist
        //int instanceCount = FindObjectsOfType(GetType()).Length;//look for any instance to see if audio player already exists
        //if(instanceCount > 1)
        if(instance != null)
        {
            gameObject.SetActive(false);//disable in awake so other things can use it before we destroy it
            Destroy(gameObject);//if audio player already exists then destroy it so we dont have multiple
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//if just 1, the one we instantiate when play pressed, then itll be carried over to other scenes  
        }

    }
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);//play shooting audio clip at its volume we set in unity inspector
    }
    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }
    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);//(audio clip, position it plays at, volume)
        }
    }

}
