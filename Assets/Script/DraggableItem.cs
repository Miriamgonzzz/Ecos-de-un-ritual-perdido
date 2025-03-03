using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform parentAfterDrag;
    private Image itemImage;
    private Inventory inventory;
    public int slotIndex; // Índice del slot en el inventario rápido

    void Start()
    {
        itemImage = GetComponent<Image>();
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // Mover al nivel más alto de la jerarquía para que no esté limitado por el layout
        transform.SetAsLastSibling();
        itemImage.raycastTarget = false; // Evita que interfiera con otros objetos mientras se arrastra
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Sigue el cursor
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        itemImage.raycastTarget = true; // Reactivar raycast
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            // Intercambia los slots en el inventario rápido
            inventory.SwapItems(slotIndex, droppedItem.slotIndex);
        }
    }
}
