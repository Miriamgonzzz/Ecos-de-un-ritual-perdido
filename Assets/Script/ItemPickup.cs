using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string itemName; // Nombre del objeto

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem(itemName);
                Destroy(gameObject); // Elimina el objeto del mundo
            }
        }
    }
}
