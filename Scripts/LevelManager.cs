using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//need if were using diff levels

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;//delay so when die theres a pause before game over
    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()//not in tutorial
    {        
        Debug.Log("Reload and reset score");
        //scoreKeeper.ResetScore();     ////problem          
                        //can load scene by name or index in build settings
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu");//just loads whatever scene you put in brackets
    }

    public void LoadGameOver()
    {   
        Debug.Log("LoadGameOver");
        StartCoroutine(WaitandLoad("GameOver", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();//doesnt work for web and might not work for mobile games(fine for small unity games)
    }

        //wait for delay before loading scene
    IEnumerator WaitandLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);//wait for number of seconds set by delay variable
        SceneManager.LoadScene(sceneName);
    }
}
