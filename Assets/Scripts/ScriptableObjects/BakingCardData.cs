using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Ingredient,
    Utensil
}

public class BakingCardData : ScriptableObject
{
    [field: SerializeField] public Sprite Portrait { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, TextArea(4, 4)] public string Description { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public CardType CardType { get; private set; }
}
