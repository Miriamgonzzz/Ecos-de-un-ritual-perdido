using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int bigInventorySize = 12; // Tama�o m�ximo del inventario grande
    public int quickInventorySize = 5; // Tama�o del inventario r�pido

    public List<string> bigInventory = new List<string>(); // Inventario grande
    public List<string> quickInventory = new List<string>(); // Inventario r�pido

    public List<Image> slots; // Im�genes de los slots en la UI
    public List<Image> quickInventorySlots; // Slots del inventario r�pido (solo 5 slots)
    public List<string> itemNames;
    public List<Sprite> itemSprites;
    public Dictionary<string, Sprite> itemIcons; // Diccionario de �conos de objetos

    private int selectedSlot = -1; // Slot seleccionado para el intercambio en el inventario grande
    private int selectedQuickSlot = -1; // Slot seleccionado en el inventario r�pido (para usar el item)


    void Start()
    {
        itemIcons = new Dictionary<string, Sprite>();

     for (int i = 0; i < itemNames.Count && i < itemSprites.Count; i++)
        {
            itemIcons[itemNames[i]] = itemSprites[i];
        }

        UpdateQuickInventory();
        UpdateInventoryUI();

        // Agregar los EventTriggers para cada slot de imagen
        SetupEventTriggers();
    }

    // ?? M�todo para agregar un objeto al inventario
    public void AddItem(string item)
    {
        if (bigInventory.Count < bigInventorySize)
        {
            bigInventory.Add(item);
            Debug.Log("Agregado: " + item);
            UpdateQuickInventory();
            UpdateInventoryUI() ;
        }
        else
        {
            Debug.Log("Inventario lleno");
        }
    }

    // ?? Actualiza el inventario r�pido y la UI
    void UpdateQuickInventory()
    {
        quickInventory.Clear();

        for (int i = 0; i < quickInventorySize && i < bigInventory.Count; i++)
        {
            quickInventory.Add(bigInventory[i]);
        }

        UpdateInventoryUI(); // Llamamos a la funci�n que actualiza la UI
    }



    // ?? Actualiza la UI del inventario r�pido
    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < bigInventory.Count && itemIcons.ContainsKey(bigInventory[i]))
            {
                slots[i].sprite = itemIcons[bigInventory[i]];
                slots[i].enabled = true; // Muestra la imagen
            }
            else
            {
                slots[i].sprite = null;
                slots[i].enabled = false; // Oculta la imagen si no hay objeto
            }
        }
        // Actualiza la UI del inventario r�pido
        for (int i = 0; i < quickInventorySlots.Count; i++)
        {
            if (i < quickInventory.Count && itemIcons.ContainsKey(quickInventory[i]))
            {
                quickInventorySlots[i].sprite = itemIcons[quickInventory[i]];
                quickInventorySlots[i].enabled = true; // Muestra la imagen
            }
            else
            {
                quickInventorySlots[i].sprite = null;
                quickInventorySlots[i].enabled = false; // Oculta la imagen si no hay objeto
            }
        }
    }
    // M�todo para intercambiar items entre dos slots del inventario grande
    public void SwapItems(int slotA, int slotB)
    {
        if (slotA < bigInventory.Count && slotB < bigInventory.Count)
        {
            string temp = bigInventory[slotA];
            bigInventory[slotA] = bigInventory[slotB];
            bigInventory[slotB] = temp;

            UpdateQuickInventory();
            UpdateInventoryUI(); // Refresca la UI del inventario grande
        }
    }

    // M�todo para seleccionar un item cuando se hace clic en una imagen en el inventario grande
    public void SelectSlotInBigInventory(int slotIndex)
    {
        Debug.Log("�ndice recibido para inventario grande: " + slotIndex);
        Debug.Log("Tama�o del inventario grande: " + bigInventory.Count);

        if (slotIndex >= 0 && slotIndex < bigInventory.Count)
        {

            if (selectedSlot == -1)
            {
                selectedSlot = slotIndex;
            }
            else
            {
                SwapItems(selectedSlot, slotIndex);
                selectedSlot = -1;
            }
        }
        else
        {
            Debug.LogWarning("�ndice fuera de rango en el inventario grande: " + slotIndex);
        }
    }

    // M�todo para seleccionar un item cuando se hace clic en una imagen en el inventario r�pido
    public void SelectSlotInQuickInventory(int slotIndex)
    {
        Debug.Log("�ndice recibido para inventario r�pido: " + slotIndex);
        Debug.Log("Tama�o del inventario r�pido: " + quickInventory.Count);

        if (slotIndex >= 0 && slotIndex < quickInventory.Count)
        {
            selectedQuickSlot = slotIndex;
            string selectedItem = quickInventory[selectedQuickSlot];
        }
        else
        {
            Debug.LogWarning("�ndice fuera de rango en el inventario r�pido: " + slotIndex);
        }
        // Aqu� puedes agregar la l�gica para usar el item seleccionado, como equiparlo o usarlo.
    }

    // M�todo para eliminar un item del inventario
    public void RemoveItem(string item)
    {
        if (bigInventory.Contains(item))
        {
            bigInventory.Remove(item);
            Debug.Log("Eliminado: " + item);
            UpdateQuickInventory(); // Actualiza el inventario r�pido despu�s de eliminar un objeto
            UpdateInventoryUI(); // Refresca la UI de ambos inventarios
        }
        else
        {
            Debug.Log("Item no encontrado en el inventario");
        }
    }

    // Configura el EventTrigger para cada slot en los inventarios
    void SetupEventTriggers()
    {

        for (int i = 0; i < quickInventorySlots.Count; i++)
        {
            EventTrigger trigger = quickInventorySlots[i].GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = quickInventorySlots[i].gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };

            int index = i; // Importante para evitar problemas con closures
            entry.callback.AddListener((eventData) => {
                SelectSlotInQuickInventory(index); });
            trigger.triggers.Add(entry);
        }

        for (int i = 0; i < slots.Count; i++)
        {

            if (slots[i] == null)
            {
                continue;
            }

            EventTrigger trigger = slots[i].GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = slots[i].gameObject.AddComponent<EventTrigger>();

            }

            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
               
            };

            int index = i; // Importante para evitar problemas con closures
            entry.callback.AddListener((eventData) => {
                SelectSlotInBigInventory(index); 
            });
            trigger.triggers.Add(entry);
        }
    }

}
