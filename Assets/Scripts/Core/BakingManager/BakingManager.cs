using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ClumsyWizard.Utilities;
using TMPro;

public class BakingManager : Singleton<BakingManager>
{
    public static Transform Transform { get; private set; }
    public static Canvas Canvas {get; private set; }
    private CakeGenerator cakeGenerator;

    [Header("Baking Area")]
    [SerializeField] private Transform bakingArea;
    [SerializeField] private List<GameObject> usedIngredients = new List<GameObject>();
    [SerializeField] private Transform bakingSlotHolder;
    private List<BakingSlot> bakingSlots = new List<BakingSlot>();

    [Header("Cake Sell")]
    [SerializeField] private TextMeshProUGUI instructionText;
    private List<IngredientCardData> cakeLayers;
    private int cakeScore;
    [SerializeField] private int pricePerCakeScore;
    [SerializeField] private GameObject purchaseEffect;

    private void Start()
    {
        Transform = bakingArea;
        Canvas = GetComponentInChildren<Canvas>();

        cakeGenerator = new CakeGenerator();

        for (int i = 0; i < bakingSlotHolder.childCount; i++)
        {
            BakingSlot slot = bakingSlotHolder.GetChild(i).GetComponent<BakingSlot>();
            bakingSlots.Add(slot);
            slot.OnIngredientDrop += OnDrop;
        }

        GenerateRandomCake();
    }

    private void GenerateRandomCake()
    {
        (List<IngredientCardData> cakeLayers, string instruction) = cakeGenerator.GenerateCake();
        instructionText.text = instruction;
        this.cakeLayers = cakeLayers;
    }

    public static void AddPenalty(int amount)
    {
        Instance.cakeScore -= amount;
    }

    //UI
    public void SellCake()
    {
        bool empty = true;
        foreach (BakingSlot slot in bakingSlots)
        {
            if (slot.ingredientTypes.Count != 0)
            {
                empty = false;
                break;
            }
        }

        if (empty)
            return;

        AudioManager.PlayAudio("CakeSold");
        foreach (BakingSlot slot in bakingSlots)
        {
            for (int i = 0; i < Mathf.Min(slot.ingredientTypes.Count, cakeLayers.Count); i++)
            {
                if (slot.ingredientTypes[i].Type == IngredientType.Base)
                    Instantiate(Instance.purchaseEffect, slot.transform.position, Quaternion.identity);

                if (slot.ingredientTypes[i].Type == cakeLayers[i].IngredientType)
                {
                    if (slot.ingredientTypes[i].Name == cakeLayers[i].Name)
                    {
                        cakeScore += cakeLayers[i].Points;
                    }
                    else
                    {
                        cakeScore += Mathf.RoundToInt(cakeLayers[i].Points / 2.0f);
                    }
                }
                else
                {
                    cakeScore -= Mathf.RoundToInt(cakeLayers[i].Points / 2.0f);
                }
            }

            slot.Clear();
        }

        PlayerManager.AddMoney(cakeScore * pricePerCakeScore);
        GameManager.CakesBaked++;
        cakeScore = 0;
        GenerateRandomCake();
    }

    private void OnDrop(IngredientCard card)
    {
        GameObject prefab = card.GetSpawnedPrefab();
        usedIngredients.Add(prefab);

        if (card.EffectArea == 1)
        {
            Destroy(card.gameObject);
        }
        else
        {
            card.OnEndDrag();
            card.DecreaseEffectArea();
        }
    }
}
