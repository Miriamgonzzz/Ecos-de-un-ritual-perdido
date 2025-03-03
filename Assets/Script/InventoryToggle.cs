using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryToggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform inventoryImage; // Panel del inventario grande
    public RectTransform inventoryButton; // RectTransform del botón
    public CanvasGroup canvasGroup; // Para ocultarlo y mostrarlo suavemente
    public float moveSpeed = 50f; // Velocidad de animación
    public Vector2 hiddenPosition; // Posición oculta fuera de la pantalla
    public Vector2 visiblePosition; // Posición visible en la pantalla
    public Vector2 hiddenButtonPosition; // Posición oculta del botón (fuera de la pantalla)
    public Vector2 visibleButtonPosition; // Posición visible del botón (en la pantalla, distinta al inventario)
    private bool isVisible = false;

    void Start()
    {
        hiddenPosition = new Vector2(1920, inventoryImage.anchoredPosition.y); // Fuera de la pantalla (a la derecha)
        visiblePosition = new Vector2(673, inventoryImage.anchoredPosition.y); // Visible (ajusta como desees)

        hiddenButtonPosition = new Vector2(880, 428); // Botón oculto, a la derecha
        visibleButtonPosition = new Vector2(331, 428); // Botón visible, ajusta como desees

        inventoryImage.anchoredPosition = hiddenPosition; // Inicialmente oculto
        inventoryButton.anchoredPosition = hiddenButtonPosition;
        canvasGroup.alpha = 0; // Ocultar UI al inicio
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        StopAllCoroutines();
        StartCoroutine(isVisible ? HideInventory() : ShowInventory());
    }

    IEnumerator ShowInventory()
    {
        isVisible = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed; // Incrementamos t con el paso del tiempo
            t = Mathf.Clamp01(t); // Aseguramos que t esté entre 0 y 1

            // Hacemos la transición de la posición y la visibilidad con Lerp
            inventoryImage.anchoredPosition = Vector2.Lerp(hiddenPosition, visiblePosition, t);
            inventoryButton.anchoredPosition = Vector2.Lerp(hiddenButtonPosition, visibleButtonPosition, t); // Mover el botón también
            canvasGroup.alpha = Mathf.Lerp(0, 1, t); // Transición de opacidad
            yield return null; // Espera hasta el siguiente frame
        }
    }

    IEnumerator HideInventory()
    {
        isVisible = false;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            inventoryImage.anchoredPosition = Vector2.Lerp(visiblePosition, hiddenPosition, t);
            inventoryButton.anchoredPosition = Vector2.Lerp(visibleButtonPosition, hiddenButtonPosition, t); // Mover el botón también
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
