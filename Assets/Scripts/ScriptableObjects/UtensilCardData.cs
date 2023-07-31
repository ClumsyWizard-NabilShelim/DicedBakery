using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UtensilStat
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
    [field: SerializeField] public int Limit { get; private set; }

    public UtensilStat(string name, int value, int limit)
    {
        Name = name;
        Value = value;
        Limit = limit;
    }
}

[CreateAssetMenu(fileName = "New Utensil", menuName = "Baking/Utensil")]
public class UtensilCardData : BakingCardData
{
    [field: SerializeField] public UtensilStat Stats { get; private set; }
}