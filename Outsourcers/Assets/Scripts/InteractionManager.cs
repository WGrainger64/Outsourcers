using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Bug hoveredBug = null;

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

            if (objectHitByRaycast.GetComponent<Bug>())
            {
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
        }
    }
}
