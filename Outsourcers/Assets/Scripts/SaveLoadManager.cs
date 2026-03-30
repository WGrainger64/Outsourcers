using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; set; }

    string highScoreKey = "LeastOwedSaveValue";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveHighScore(float score)
    {
        //Saves score into a key that we named at the start
        PlayerPrefs.SetFloat(highScoreKey, score);
    }

    public float LoadHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            return PlayerPrefs.GetFloat(highScoreKey);
        }
        else
        {
            return 400000;
        }
    }
}
