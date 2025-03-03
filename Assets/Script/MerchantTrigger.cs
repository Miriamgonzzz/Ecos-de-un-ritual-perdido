using UnityEngine;

public class MerchantTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mercadoUI; // Referencia al men� del mercado
    private bool jugadorDentro = false; // Para saber si el jugador est� en el trigger

    void Start()
    {
        // Asegurar que el mercado est� oculto al inicio
        mercadoUI.SetActive(false);
    }

    void Update()
    {
        // Si el jugador est� dentro y presiona "E", abrir/cerrar el mercado
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMercado();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra al collider, permitir abrir el mercado
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del collider, cerrar el mercado y evitar que se abra
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            mercadoUI.SetActive(false);
        }
    }

    public void ToggleMercado()
    {
        // Alternar visibilidad del mercado
        mercadoUI.SetActive(!mercadoUI.activeSelf);
    }

    public void CerrarMercado()
    {
        // M�todo para cerrar el mercado (se puede asignar al bot�n de la "X")
        mercadoUI.SetActive(false);
    }
}
