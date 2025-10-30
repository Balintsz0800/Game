using UnityEngine;
using UnityEngine.Animations;


interface IInteractable {
    public void Interact();
}
public class Interaction : MonoBehaviour
{
    public Transform InteractionSource;
    public float InteractionRange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
         Ray r = new Ray(InteractionSource.position, InteractionSource.forward);
         if (Physics.Raycast(r, out RaycastHit hit, InteractionRange))
         {
             if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
             {
                 interactObj.Interact();
             }
         }
        }
    }

    public void Interact()
    {
        Debug.Log(Random.Range(0, 100));
    }
}
