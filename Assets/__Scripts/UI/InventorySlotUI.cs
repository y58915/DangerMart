using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image image;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    public static int draggedIndex;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void SetItem(Item _item)
    {
        image.sprite = _item.itemImage;
        image.color = Color.white;
    }

    public void ClearItem()
    {
        image.sprite = null;
        image.color = Color.white;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // Debug.Log("Onbegin");
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        Inventory.instance.movingIndex = System.Array.IndexOf(InventoryPanelUI.instance.inventorySlotList, this); //TODO: assign dragged items index
        // Debug.Log(Inventory.instance.movingIndex);
    }

    public void OnDrag(PointerEventData eventData) {
        // Debug.Log("OnDrag");
        rectTransform.anchoredPosition += (eventData.delta)/(canvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData) {
        // Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = originalPosition;
        // Inventory.instance.movingIndex = -1;
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Debug.Log("OnPointerDown");
    }

}

