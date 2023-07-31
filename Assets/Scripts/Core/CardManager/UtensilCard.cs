using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UtensilCard : BakingCard
{
    [Header("Extra")]
    [SerializeField] private GameObject statBoosterPrefab;
    [SerializeField] private Transform statBoosterHolder;
    private int capacity;

    public override void Initialize(BakingCardData data, Canvas canvas)
    {
        UtensilCardData utensilData = (UtensilCardData)data;

        if (utensilData.Stats != null)
        {
            CardStatBooster statBooster = Instantiate(statBoosterPrefab, statBoosterHolder).GetComponent<CardStatBooster>();
            capacity = utensilData.Stats.Value;
            statBooster.Initialize(utensilData.Stats.Name, utensilData.Stats.Limit, (int boostedValue) =>
            {
                if (capacity + boostedValue <= utensilData.Stats.Value)
                {
                    capacity += boostedValue;
                }

                UpdateUI();
            });
        }

        base.Initialize(data, canvas);
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();

        UpdateDescription("<%Capacity%>", capacity.ToString());
    }
}
