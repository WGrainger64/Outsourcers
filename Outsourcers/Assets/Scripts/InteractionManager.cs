using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Bug hoveredBug = null;
    public Disposal_script hoveredBin = null;

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
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit)) 
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Bug>() && objectHitByRaycast.GetComponent<Bug>().currHold == false)
            {
                //Disable outline of previously selected item
                if (hoveredBug)
                {
                    hoveredBug.GetComponent<Outline>().enabled = false;
                }

                hoveredBug = objectHitByRaycast.gameObject.GetComponent<Bug>();
                if (hoveredBug.isAlive == false)
                {
                    hoveredBug.GetComponent<Outline>().enabled = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    { 
                        BugManager.Instance.PickupBug(objectHitByRaycast.gameObject);
                    }
                }
            }
            else
            {
                if (hoveredBug)
                {
                    hoveredBug.GetComponent<Outline>().enabled = false;
                }
            }

            //Put the interaction for the disposal outline here
            if (objectHitByRaycast.GetComponent<Disposal_script>())
            {
                //Disable outline of previously selected item
                if (hoveredBin)
                {
                    hoveredBin.GetComponent<Outline>().enabled = false;
                }

                hoveredBin = objectHitByRaycast.gameObject.GetComponent<Disposal_script>();

                hoveredBin.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    BugManager.Instance.SellBug(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredBin)
                {
                    hoveredBin.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
