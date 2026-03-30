using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool doorBool = true;
    public float doorSpeed = 1.0f;
    public float doorHeight = 9f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float doorCheck = Input.GetAxis("Fire1");
        float translation = 1.0f * Time.deltaTime;

        if (doorCheck != 0)
        {
            doorBool = true;
        }

        if (doorBool)
        {
            if (transform.position.y < doorHeight)
            {
                transform.Translate(0, translation, 0);
            }
        }

    }
}
