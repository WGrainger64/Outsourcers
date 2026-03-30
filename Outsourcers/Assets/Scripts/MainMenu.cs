using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;

    public string newGameScene = "Outsourcers";

    public AudioClip bg_music;
    public AudioSource main_channel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        main_channel.PlayOneShot(bg_music);

        //Set the high score text
        float highScore = SaveLoadManager.Instance.LoadHighScore();
        highScoreUI.text = $"Least Owed: ${highScore}";

        //Unlock the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartNewGame()
    {
        main_channel.Stop();
        SceneManager.LoadScene(newGameScene);
    }

    public void ExitApplication()
    {
        //Stil exits in the editor for testing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
