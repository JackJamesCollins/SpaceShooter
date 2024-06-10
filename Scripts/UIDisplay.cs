using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();//gets health at start to show full health when game starts. this auto updates if we change health value in other script
    }


    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();//slider goes down if health does
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");//text is string so convert to string
    }                                                   //adding 0s makes them appear at bottom of screen and right 0s replaced by score as it increases (00005500)
}
