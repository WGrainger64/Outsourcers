using UnityEngine;

public class rifle : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float minimumY = -70f;
    public float maximumY = 80f;
    public float currRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //get mouseY
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        //Add the mouse val to the curr rotation
        currRotation += mouseY;

        //Prevents flipping
        currRotation = Mathf.Clamp(currRotation, minimumY, maximumY);

        //Rotate the gun
        transform.localEulerAngles = new Vector3(-currRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
