using UnityEngine;

public class GlobalReference : MonoBehaviour
{
    public static GlobalReference Instance { get; set; }

    public GameObject bulletImpactEffectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null && Instance != this)
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
        
    }
}
