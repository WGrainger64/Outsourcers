using UnityEngine;

public class Recorder : MonoBehaviour
{

    public AudioClip recording;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If we call this function on the same object twice it will stop it
    public void PlayRecording(GameObject tape)
    {
        if (SoundManager.Instance.recordingChannel.isPlaying)
        {
            SoundManager.Instance.recordingChannel.Stop();
        }
        else
        {
            SoundManager.Instance.recordingChannel.PlayOneShot(recording);
        }
        
    }
}
