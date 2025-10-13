    using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private float Xrotation = 0f;
    private float Yrotation = 0f;
    public Transform Orientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        Xrotation -= mouseY;
        
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        
        Yrotation += mouseX;
        transform.localRotation = Quaternion.Euler(Xrotation, Yrotation, 0f);
        Orientation.Rotate(Vector3.up *  mouseX);
    }
}
