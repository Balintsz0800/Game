using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable {
    [SerializeField] Transform pivot;
    [SerializeField] float openAngle = 70f;
    [SerializeField] float openDuration = 1f;
    
    public string Description { get; set; }
    float defaultYRotation;
    float targetYRotation;
    float currentLerpTime;
    bool isOpen;

    void Start() {
        defaultYRotation = pivot.localEulerAngles.y;
    }

    void Update() {
        if (currentLerpTime < openDuration) {
            currentLerpTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentLerpTime / openDuration);
            float yRotation = Mathf.LerpAngle(pivot.localEulerAngles.y, targetYRotation, t);
            pivot.localEulerAngles = new Vector3(0f, yRotation, 0f);
        }
    }
    
    public void Interact() {
        ToggleDoor();
    }
    void ToggleDoor() {
        isOpen = !isOpen;
        targetYRotation = isOpen ? defaultYRotation + openAngle : defaultYRotation;
        currentLerpTime = 0f;
        Debug.Log("Open");
    }
}