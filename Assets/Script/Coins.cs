using UnityEngine;
using UnityEngine.UI;

public class Coins: MonoBehaviour
{
    public int valorMoneda = 10; // Valor de la moneda

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegurar que solo el jugador puede recogerla
        {
            Inventory inventario = other.GetComponent<Inventory>(); // Buscar el inventario en el jugador

            if (inventario != null)
            {
                inventario.AgregarMonedas(valorMoneda); // Sumar monedas
            }

            Destroy(gameObject); // Destruir la moneda después de recogerla
        }
    }

}
