using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour, IPointerDownHandler, IPointerUpHandler , IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private int maxFaceValue;
    public int FaceValue { get; private set; }

    private new RectTransform transform;
    private Transform parent;

    [Header("UI")]
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public void Initialize(Canvas canvas)
    {
        transform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;

        parent = transform.parent;
        this.canvas = canvas;
        FaceValue = UnityEngine.Random.Range(1, maxFaceValue + 1);
        transform.GetChild(0).GetChild(FaceValue - 1).gameObject.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.SetParent(parent.transform);
    } 
}
