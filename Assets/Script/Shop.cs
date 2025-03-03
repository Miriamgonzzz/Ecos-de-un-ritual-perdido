using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Shop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mercadoUI; // UI del mercado
    public List<Product> productosEnVenta; // Lista de productos en este mercado
    public Transform listaProductosUI; // Panel donde se mostrarán los productos
    public GameObject itemPrefab; // Prefab del botón del producto
    public Inventory inventario; // Referencia al inventario del jugador

    private bool jugadorDentro = false;

    void Start()
    {
        mercadoUI.SetActive(false);
    }

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMercado();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            mercadoUI.SetActive(false);
        }
    }

    public void ToggleMercado()
    {
        mercadoUI.SetActive(!mercadoUI.activeSelf);
        if (mercadoUI.activeSelf)
        {
            MostrarProductos();
        }
    }

    void MostrarProductos()
    {
        foreach (Transform child in listaProductosUI)
        {
            Destroy(child.gameObject); // Limpiar la lista antes de actualizarla
        }

        foreach (Product producto in productosEnVenta)
        {
            GameObject item = Instantiate(itemPrefab, listaProductosUI);
            item.transform.Find("Nombre").GetComponent<Text>().text = producto.nombre;
            item.transform.Find("Precio").GetComponent<Text>().text = producto.precio.ToString() + " monedas";
            item.transform.Find("Icono").GetComponent<Image>().sprite = producto.icono;

            Button boton = item.GetComponent<Button>();
            boton.onClick.AddListener(() => ComprarProducto(producto));
        }
    }

    public void ComprarProducto(Product producto)
    {
        if (inventario.monedas >= producto.precio)
        {
            inventario.monedas -= producto.precio; // Resta las monedas
            inventario.AddItem(producto.nombre); // Agrega el objeto al inventario
            MostrarProductos(); // Refresca la UI del mercado
            Debug.Log($"Compraste {producto.nombre}. Monedas restantes: {inventario.monedas}");
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }
}
