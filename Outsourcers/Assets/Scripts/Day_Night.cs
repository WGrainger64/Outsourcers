using UnityEngine;

public class Day_Night : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0.2f, 0, 0); //Rotates in the x direction
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
