using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable {
    [SerializeField] Transform pivot;
    [SerializeField] float openAngle = 70f;
    [SerializeField] float openDuration;
    
    public string Description { get; set; }
    private float defaultYRotation;
    float targetYRotation;
    float currentLerpTime;
    public bool isOpen;

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
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void Interact() {
        Debug.Log("Interact method called");
        ToggleDoor();
    }
    void ToggleDoor() {
        isOpen = !isOpen;
        targetYRotation = isOpen ? defaultYRotation + openAngle : defaultYRotation;
        currentLerpTime = 0f;
        Debug.Log("Open");
    }
}