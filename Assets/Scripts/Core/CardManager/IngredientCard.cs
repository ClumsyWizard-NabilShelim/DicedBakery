using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientCard : BakingCard
{
    [SerializeField] private CardStatBooster statBooster;
    public int EffectArea { get; private set; }
    public IngredientType Type => ((IngredientCardData)data).IngredientType;

    public override void Initialize(BakingCardData data, Canvas canvas)
    {
        IngredientCardData ingredientData = (IngredientCardData)data;
        EffectArea = ingredientData.Range;
        statBooster.Initialize("Effect Area", ingredientData.RangeIncreaseLimit, (int boostedValue) =>
        {
            if(EffectArea + boostedValue <= ingredientData.RangeIncreaseLimit)
            {
                EffectArea += boostedValue;
            }

            UpdateUI();
        });

        base.Initialize(data, canvas);
    }

    public void DecreaseEffectArea()
    {
        EffectArea--;
        UpdateUI();
    }

    public override void OnEndDrag()
    {
        if(EffectArea == 1)
            Destroy(spawnedPrefabTransform.gameObject);

        base.OnEndDrag();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();

        UpdateDescription("<%EffectArea%>", EffectArea.ToString());
    }
}
