using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ClumsyWizard.Utilities;

[Serializable]
public struct CakeDataHolder
{
    public IngredientType IngredientType;
    public List<IngredientCardData> Data;
}

public class CakeGenerator
{
    private Dictionary<IngredientType, List<IngredientCardData>> ingredients;

    public CakeGenerator()
    {
        ingredients = new Dictionary<IngredientType, List<IngredientCardData>>();

        foreach (IngredientCardData data in CardManager.GetIngredients())
        {
            if(ingredients.ContainsKey(data.IngredientType))
                ingredients[data.IngredientType].Add(data);
            else
                ingredients.Add(data.IngredientType, new List<IngredientCardData>() { data });
        }
    }

    public (List<IngredientCardData> cakeLayers, string instruction) GenerateCake()
    {
        int numberOfLayers = UnityEngine.Random.Range(1, 4);

        List<IngredientCardData> cakeData = new List<IngredientCardData>();
        IngredientCardData baseLayer = GetRandomIngredientOfType(IngredientType.Base);

        cakeData.Add(baseLayer);

        for (int i = 1; i < numberOfLayers; i++)
        {
            cakeData.Add(GetRandomIngredeint());
        }

        string instruction = "";

        for (int i = 0; i < cakeData.Count; i++)
        {
            if (i == 0)
            {
                instruction += "\nBase Layer: " + cakeData[0].Name + "\n";
                continue;
            }

            instruction += "Layer " + i.ToString() + ": " + cakeData[i].Name + "\n";
        }

        return (cakeData, instruction);
    }

    private IngredientCardData GetRandomIngredientOfType(IngredientType type)
    {
        int randomIndex = UnityEngine.Random.Range(0, ingredients[type].Count);
        return ingredients[type][randomIndex];
    }

    private IngredientCardData GetRandomIngredeint()
    {
        IngredientType type = (IngredientType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(IngredientType)).Length);
        int randomIndex = UnityEngine.Random.Range(0, ingredients[type].Count);
        return ingredients[type][randomIndex];
    }
}
