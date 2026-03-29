using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BugManager : MonoBehaviour
{
    public static BugManager Instance { get; set; }
    public GameObject player;
    public GameObject activeBugSlot;
    public List<GameObject> bugSlots;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        activeBugSlot = bugSlots[0];
    }
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject bugSlot in bugSlots)
        {
            if (bugSlot == activeBugSlot)
            {
                bugSlot.SetActive(true);
            }
            else
            {
                bugSlot.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }

    }

    public void PickupBug(GameObject pickedUpBug)
    {
        
        DropCurrentBug(pickedUpBug);

        //Set bug as a child to the parent
        
        print("pickup");
        pickedUpBug.transform.SetParent(activeBugSlot.transform, false);
        
        Bug bug = pickedUpBug.GetComponent<Bug>();
  
        pickedUpBug.transform.localPosition = new Vector3(bug.spawnPos.x, bug.spawnPos.y, bug.spawnPos.z);
        pickedUpBug.transform.localRotation = Quaternion.Euler(bug.spawnRot.x, bug.spawnRot.y, bug.spawnRot.z);

        //Set current bug to be picked up
        bug.currHold = true;
        //stop animations
        bug.animator.enabled = false;
    }

    private void DropCurrentBug(GameObject pickedUpBug)
    {
        if (activeBugSlot.transform.childCount > 0)
        {

            var bugToDrop = activeBugSlot.transform.GetChild(0).gameObject;

            bugToDrop.GetComponent<Bug>().currHold = false;
            //bugToDrop.GetComponent<Bug>().animator.enabled = true;

            bugToDrop.transform.SetParent(pickedUpBug.transform.parent);
            bugToDrop.transform.localPosition = pickedUpBug.transform.localPosition;
            bugToDrop.transform.localRotation = pickedUpBug.transform.localRotation; //21:45
        }
    }

    public void SwitchActiveSlot(int slotNumber)
    {
        if(activeBugSlot.transform.childCount > 0)
        {
            Bug currBug = activeBugSlot.transform.GetChild(0).GetComponent<Bug>();
            currBug.currHold = false;
        }

        activeBugSlot = bugSlots[slotNumber];

        if (activeBugSlot.transform.childCount > 0)
        {
            Bug newBug = activeBugSlot.transform.GetChild(0).GetComponent<Bug>();
            newBug.currHold = true;
        }
    }

    public void SellBug(GameObject bin)
    {
        //Get the current bug in the active slot
        Bug currentBug = activeBugSlot.transform.GetChild(0).GetComponent<Bug>();
        //Get a random price fluctation
        float priceFluct = (UnityEngine.Random.Range(-10.0f, 10.0f));
        priceFluct = Mathf.Round(priceFluct * 100.0f) * 0.01f;

        //Add current player money the price of the bug and the price fluctation
        float money = player.GetComponent<Player>().playerMoney += currentBug.price + priceFluct;

        //Play chaching
        SoundManager.Instance.chaChing.Play();

        player.GetComponent<Player>().playerMoneyUI.text = $"${money}";

        Destroy(activeBugSlot.transform.GetChild(0).gameObject);
    }
}
