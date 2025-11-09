using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class Interaction : MonoBehaviour {
 
    public Camera mainCamera;
    public float interactionDistance = 10f;
 
    public GameObject interactionUI;
    public TMP_Text interactionText;
    
    private void Update() {
        InteractionRay();
    }
    
    private void InteractionRay() {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;
 
        bool hitSomething = false;
 
        if (Physics.Raycast(ray, out hit, interactionDistance)) {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
 
            if (interactable != null) {
                hitSomething = true;
                if (!interactionUI.activeSelf)
                {
                    interactionUI.SetActive(true);
                }
                interactionText.text = interactable.Description;
 
                if (Input.GetKeyDown(KeyCode.E)) {
                    interactable.Interact();
                }
            }
        } 
        interactionUI.SetActive(hitSomething);
    }
}