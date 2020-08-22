using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] private float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }
    
    public void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void LoadEndGame()
    {
        StartCoroutine(WaitAndLoad());
        
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("End Game");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
