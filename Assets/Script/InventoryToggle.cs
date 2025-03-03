using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryToggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform inventoryImage; // Panel del inventario grande
    public RectTransform inventoryButton; // RectTransform del bot�n
    public CanvasGroup canvasGroup; // Para ocultarlo y mostrarlo suavemente
    public float moveSpeed = 50f; // Velocidad de animaci�n
    public Vector2 hiddenPosition; // Posici�n oculta fuera de la pantalla
    public Vector2 visiblePosition; // Posici�n visible en la pantalla
    public Vector2 hiddenButtonPosition; // Posici�n oculta del bot�n (fuera de la pantalla)
    public Vector2 visibleButtonPosition; // Posici�n visible del bot�n (en la pantalla, distinta al inventario)
    private bool isVisible = false;

    void Start()
    {
        hiddenPosition = new Vector2(1920, inventoryImage.anchoredPosition.y); // Fuera de la pantalla (a la derecha)
        visiblePosition = new Vector2(673, inventoryImage.anchoredPosition.y); // Visible (ajusta como desees)

        hiddenButtonPosition = new Vector2(880, 428); // Bot�n oculto, a la derecha
        visibleButtonPosition = new Vector2(331, 428); // Bot�n visible, ajusta como desees

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
            t = Mathf.Clamp01(t); // Aseguramos que t est� entre 0 y 1

            // Hacemos la transici�n de la posici�n y la visibilidad con Lerp
            inventoryImage.anchoredPosition = Vector2.Lerp(hiddenPosition, visiblePosition, t);
            inventoryButton.anchoredPosition = Vector2.Lerp(hiddenButtonPosition, visibleButtonPosition, t); // Mover el bot�n tambi�n
            canvasGroup.alpha = Mathf.Lerp(0, 1, t); // Transici�n de opacidad
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
            inventoryButton.anchoredPosition = Vector2.Lerp(visibleButtonPosition, hiddenButtonPosition, t); // Mover el bot�n tambi�n
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
