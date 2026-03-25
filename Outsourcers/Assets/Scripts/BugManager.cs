using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    }

    private void DropCurrentBug(GameObject pickedUpBug)
    {
        if (activeBugSlot.transform.childCount > 0)
        {

            var bugToDrop = activeBugSlot.transform.GetChild(0).gameObject;

            bugToDrop.GetComponent<Bug>().currHold = false;

            bugToDrop.transform.SetParent(pickedUpBug.transform.parent);
            bugToDrop.transform.localPosition = pickedUpBug.transform.localPosition;
            bugToDrop.transform.localRotation = pickedUpBug.transform.localRotation; //21:45
        }
    }
}
