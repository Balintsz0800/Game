using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem InventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventoryItem.parentAfterDrag = transform;
        }
    }
}
