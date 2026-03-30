using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip companyVoice;

    public string newGameScene = "OutsourcersGame";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.clip = companyVoice;
        audioSource.Play();
        //Start the coroutine and wait for it to finish
        StartCoroutine(WaitForSoundToEnd(companyVoice.length));
    }

    private IEnumerator WaitForSoundToEnd(float duration)
    {
        yield return new WaitForSeconds(duration);

        //When finished playing then go to the game scene
        SceneManager.LoadScene(newGameScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }
    }

    private void SkipCutscene()
    {
        SceneManager.LoadScene(newGameScene);
    }
}
