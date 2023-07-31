using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BakingSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public List<IngredientCard> ingredientTypes { get; private set; }
    public Action<IngredientCard> OnIngredientDrop { get; set; }
    [SerializeField] private GameObject placeEffect;
    private void Start()
    {
        ingredientTypes = new List<IngredientCard>();
    }

    public void Clear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        IngredientCard card;
        if (card = IsIngredient(eventData))
        {
            ingredientTypes.Add(card);
            OnIngredientDrop?.Invoke(card);
            Instantiate(placeEffect, transform.position, Quaternion.identity);
            AudioManager.PlayAudio("Plop");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IngredientCard card;
        if (card = IsIngredient(eventData))
        {
            card.GetSpawnedPrefab().transform.SetParent(transform);
            card.GetSpawnedPrefab().transform.position = transform.position;

            RectTransform rectTransform = card.GetSpawnedPrefab().GetComponent<RectTransform>();

            int size = Mathf.RoundToInt(rectTransform.sizeDelta.y / 2);
            rectTransform.localPosition += Vector3.up * (Mathf.Abs(transform.childCount - 1) * (size / 1.5f));
        }
    }

    private IngredientCard IsIngredient(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return null;

        IngredientCard card = eventData.pointerDrag.GetComponent<IngredientCard>();

        if (card == null || !card.Activated)
            return null;

        if (card.CardType != CardType.Ingredient)
            return null;

        return card;
    }
}