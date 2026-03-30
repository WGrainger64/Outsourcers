using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }
    public Transform player;

    public Bug hoveredBug = null;
    public Disposal_script hoveredBin = null;
    public Recorder hoveredTape = null;

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

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Bug>() && objectHitByRaycast.GetComponent<Bug>().currHold == false)
            {
                //Test distance here
                float distanceFromObject = Vector3.Distance(player.position, objectHitByRaycast.transform.position);
                if (distanceFromObject < 8f)
                {
                    //Disable outline of previously selected item
                    if (hoveredBug)
                    {
                        hoveredBug.GetComponent<Outline>().enabled = false;
                        hoveredBug.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    hoveredBug = objectHitByRaycast.gameObject.GetComponent<Bug>();
                    if (hoveredBug.isAlive == false)
                    {
                        hoveredBug.GetComponent<Outline>().enabled = true;
                        hoveredBug.transform.GetChild(0).gameObject.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            BugManager.Instance.PickupBug(objectHitByRaycast.gameObject);
                        }
                    }
                }
            }
            else
            {
                if (hoveredBug)
                {
                    hoveredBug.GetComponent<Outline>().enabled = false;
                    hoveredBug.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            //Put the interaction for the disposal outline here
            if (objectHitByRaycast.GetComponent<Disposal_script>())
            {
                //Test distance here
                float distanceFromObject = Vector3.Distance(player.position, objectHitByRaycast.transform.position);
                if (distanceFromObject < 8f)
                {
                    //Disable outline of previously selected item
                    if (hoveredBin)
                    {
                        hoveredBin.GetComponent<Outline>().enabled = false;
                        objectHitByRaycast.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    hoveredBin = objectHitByRaycast.gameObject.GetComponent<Disposal_script>();
                    objectHitByRaycast.transform.GetChild(0).gameObject.SetActive(true);

                    hoveredBin.GetComponent<Outline>().enabled = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        BugManager.Instance.SellBug();
                    }
                }
            }
            else
            {
                if (hoveredBin)
                {
                    hoveredBin.GetComponent<Outline>().enabled = false;
                    hoveredBin.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            if (objectHitByRaycast.GetComponent<Recorder>())
            {
                //Test distance here
                float distanceFromObject = Vector3.Distance(player.position, objectHitByRaycast.transform.position);
                if (distanceFromObject < 8f)
                {
                    //Disable outline of previously selected item
                    if (hoveredTape)
                    {
                        hoveredTape.GetComponent<Outline>().enabled = false;
                        hoveredTape.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    hoveredTape = objectHitByRaycast.gameObject.GetComponent<Recorder>();
                    hoveredTape.transform.GetChild(0).gameObject.SetActive(true);

                    hoveredTape.GetComponent<Outline>().enabled = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hoveredTape.PlayRecording(objectHitByRaycast.gameObject);
                    }
                }
            }
            else
            {
                if (hoveredTape)
                {
                    hoveredTape.GetComponent<Outline>().enabled = false;
                    hoveredTape.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}
