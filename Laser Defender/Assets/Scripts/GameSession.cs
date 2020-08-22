using UnityEngine;

public class GameSession : MonoBehaviour
{
    int Score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }

    void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public int GetScore()
    {
        return Score;
    }

    public void AddScore(int scoreValue)
    {
        Score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}