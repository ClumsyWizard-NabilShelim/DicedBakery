using System.Collections;
using UnityEngine;

public enum IngredientType
{
    Base,
    Topping,
    Fruit
}

[CreateAssetMenu(fileName = "New Utensil", menuName = "Baking/Ingredient")]
public class IngredientCardData : BakingCardData
{
    [field: SerializeField] public int Range { get; private set; }
    [field: SerializeField, Range(1, 6)] public int RangeIncreaseLimit { get; private set; }
    [field: SerializeField] public int Points { get; private set; }
    [field: SerializeField] public IngredientType IngredientType { get; private set; }
}