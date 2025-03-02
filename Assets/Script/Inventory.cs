using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int bigInventorySize = 12; // Tamaño máximo del inventario grande
    public int quickInventorySize = 5; // Tamaño del inventario rápido

    public List<string> bigInventory = new List<string>(); // Inventario grande
    public List<string> quickInventory = new List<string>(); // Inventario rápido

    public List<Image> slots; // Imágenes de los slots en la UI
    public Dictionary<string, Sprite> itemIcons; // Diccionario de íconos de objetos

    void Start()
    {
        itemIcons = new Dictionary<string, Sprite>
    {
        { "Llave", Resources.Load<Sprite>("Assets/Inventory/RGP icons/items/key_silver") },
        { "Poción", Resources.Load<Sprite>("Assets/Inventory/RGP icons/items/bottle_standard_blue") }
    };
        UpdateQuickInventory();
    }

    // ?? Método para agregar un objeto al inventario
    public void AddItem(string item)
    {
        if (bigInventory.Count < bigInventorySize)
        {
            bigInventory.Add(item);
            Debug.Log("Agregado: " + item);
            UpdateQuickInventory();
        }
        else
        {
            Debug.Log("Inventario lleno");
        }
    }

    // ?? Actualiza el inventario rápido y la UI
    void UpdateQuickInventory()
    {
        quickInventory.Clear();

        for (int i = 0; i < quickInventorySize && i < bigInventory.Count; i++)
        {
            quickInventory.Add(bigInventory[i]);
        }

        UpdateInventoryUI(); // Llamamos a la función que actualiza la UI
    }

    // ?? Actualiza la UI del inventario rápido
    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < quickInventory.Count && itemIcons.ContainsKey(quickInventory[i]))
            {
                slots[i].sprite = itemIcons[quickInventory[i]];
                slots[i].enabled = true; // Muestra la imagen
            }
            else
            {
                slots[i].sprite = null;
                slots[i].enabled = false; // Oculta la imagen si no hay objeto
            }
        }
    }
    public void SwapItems(int slotA, int slotB)
    {
        if (slotA < quickInventory.Count && slotB < quickInventory.Count)
        {
            string temp = quickInventory[slotA];
            quickInventory[slotA] = quickInventory[slotB];
            quickInventory[slotB] = temp;

            UpdateInventoryUI(); // Refresca la UI
        }
    }
    public void RemoveItem(string item)
    {
        if (bigInventory.Contains(item))
        {
            bigInventory.Remove(item);
            Debug.Log("Eliminado: " + item);
            UpdateQuickInventory(); // Actualiza el inventario rápido después de eliminar un objeto
        }
        else
        {
            Debug.Log("Item no encontrado en el inventario");
        }
    }
}
