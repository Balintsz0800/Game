using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public CarController playermove;
    public Camera PlayerCamera;
    public Camera CarCam;
    public GameObject player;
    
    public float openAngle;
    public float openSpeed;
    public bool isOpen = false;
    
    private Quaternion closeRotation;
    private Quaternion openRotation;
    private Coroutine currentCoroutine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playermove.enabled = false;
        closeRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(ToggleDoor());
        }
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? closeRotation : openRotation;
        isOpen = !isOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        
        transform.rotation = targetRotation;
    }
}

internal class MonoBehaviourScript
{
}
