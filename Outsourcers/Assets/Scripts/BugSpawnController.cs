using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawnController : MonoBehaviour
{
    public int initialBugPerWave = 5;
    public int currentBugPerWave;

    public float spawnDelay = 0.5f; //Delay between spawning each bug in a wave

    public int currentWave = 0;
    public float waveCooldown = 10.0f; //Time in seconds between waves

    public bool inCooldown;
    public float cooldownCounter = 0; //Used for testing and the UI
    public int waveMult = 2; //How many bugs increase in consecutive waves
    public int spawnDistance;

    public List<Bug> currentBugsAlive;

    public GameObject bugPrefab;
    public GameObject rareBugPrefab;
    public Transform player;
    public int rareSpawnChance = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBugPerWave = initialBugPerWave;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentBugsAlive.Clear();

        currentWave++;

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
       for (int i = 0; i < currentBugPerWave; i++)
        {
            //Generate a random offset within a specified range
            Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-1f,1f), 0f, UnityEngine.Random.Range(-1f,1f));
            Vector3 spawnPosition = transform.position + spawnOffset;
            int randomNum = UnityEngine.Random.Range(0, 101);
            var bug = Instantiate(bugPrefab, spawnPosition, Quaternion.identity); ;
            print(randomNum);
            if (randomNum <= rareSpawnChance)
            {
                //Rare bug Spawn
                //Change the var to rare bug
                bug = Instantiate(rareBugPrefab, spawnPosition, Quaternion.identity);
            }
            
            //Get bug script
            Bug bugScript = bug.GetComponent<Bug>();

            //Track this bug
            currentBugsAlive.Add(bugScript);

            yield return new WaitForSeconds(spawnDelay);   
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if we are close enough to spawn bugs
        float distanceFromObject = Vector3.Distance(player.position, transform.position);
        if (distanceFromObject < spawnDistance)
        {
            GetComponent<BugSpawnController>().enabled = true;
        }
        else
        {
            GetComponent<BugSpawnController>().enabled = false;
        }

            //Get all dead bugs
            List<Bug> bugToRemove = new List<Bug>();
        foreach (Bug bug in currentBugsAlive)
        {
            if(!bug.isAlive)
            {
                bugToRemove.Add(bug);
            }
        }

        //Actually remove all dead bugs
        foreach (Bug bug in bugToRemove)
        {
            currentBugsAlive.Remove(bug);
        }

        bugToRemove.Clear();

        //Start cooldown if all bugs are dead
        if (currentBugsAlive.Count == 0 && inCooldown == false)
        {
            //Start cooldown for next wave
            StartCoroutine(WaveCooldown());
        }

        //Run the cooldown counter
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            //Reset counter
            cooldownCounter = waveCooldown;
        }
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;

        currentBugPerWave *= waveMult;
        StartNextWave();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnDistance); // Attacking //Stop Attacking
    }
}
