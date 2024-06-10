using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    int score;
    static ScoreKeeper instance;//static persists through all instances of a class, if multiple audio players instantiated then theyd all share static instance. didnt use this but can do singleton that way. Can do this to make it Glbal so you dont have to "FindObjectOfType.... in other scripts"
    void Awake()
    {
        ManageSingleton();//used to make one instance of something that isnt destroyed between scenes
    }

    void ManageSingleton()
    {               //tells us how many audio players exist
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

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)//called in health
    {
        score += value;//increase score when something is done
        Mathf.Clamp(score, 0, int.MaxValue);//clamp score so cant go below zero, int.MaxValue is built in-means highest int possible
        Debug.Log(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
