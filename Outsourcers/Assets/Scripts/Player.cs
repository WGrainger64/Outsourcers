using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int HP = 4;
    public GameObject bloodyScreen;

    public TextMeshProUGUI playerHealthUI;
    public TextMeshProUGUI ammoText;
    public GameObject gameOverText;

    public bool isDead;
    public GameObject weapon;
    public float playerMoney = 500000f;
    public TextMeshProUGUI playerMoneyUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealthUI.text = $"Health: {HP}";
        playerMoneyUI.text = $"You Owe: ${playerMoney}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {

        HP -= damageAmount;

        if (HP <= 0)
        {
            print("Player Dead");
            //Set player to dead
            isDead = true;
            //Turn off the gun
            weapon.SetActive(false);
            playerHealthUI.text = $"Health: {HP}";
            playerHealthUI.color = Color.red;
            playerMoneyUI.color = Color.red;
            ammoText.color = Color.red;
            PlayerDead();
        }
        else
        {
            print("Player Hit");
            StartCoroutine(BloodyScreenEffect());
            playerHealthUI.text = $"Health: {HP}";
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayerDead()
    {
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDeath);
        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverMusic;
        SoundManager.Instance.playerChannel.PlayDelayed(2f);
        GetComponent<FirstPersonController>().enabled = false;

        //Dying Animation
        GetComponentInChildren<Animator>().enabled = true;

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverText.gameObject.SetActive(true);

        if (playerMoney < SaveLoadManager.Instance.LoadHighScore())
        {
            SaveLoadManager.Instance.SaveHighScore(playerMoney);
        }

        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(6f);

        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator BloodyScreenEffect()
    {
        if (bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

         var image = bloodyScreen.GetComponentInChildren<Image>();
 
        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;
 
        float duration = 3f;
        float elapsedTime = 0f;
 
        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
 
            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
 
            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;
 
            yield return null; ; // Wait for the next frame.
        }
        

        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("BugAttack"))
        {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<BugAttack>().damage);
            }
           
        }
    }
}
