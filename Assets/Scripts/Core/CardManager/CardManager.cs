using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;
using System;
using TMPro;

[Serializable]
public struct CardManagerCardData
{
    public CardType cardType;
    public List<BakingCardData> cardDatas;
}

public class CardManager : Singleton<CardManager>
{
    [Header("Card")]
    [SerializeField] private List<CardManagerCardData> cardDataInitializer = new List<CardManagerCardData>();
    private Dictionary<CardType, List<BakingCardData>> cardDatas;
    [SerializeField] private int cardCount;
    [Space]
    [SerializeField] private GameObject ingredientCardPrefab;
    [SerializeField] private GameObject utensilCardPrefab;
    [SerializeField] private RectTransform cardHolder;
    [SerializeField] private Canvas canvas;

    [Header("Draw")]
    [SerializeField] private int drawTimeIncrease;
    [SerializeField] private TextMeshProUGUI drawTimeText;


    protected override void Awake()
    {
        base.Awake();
        cardDatas = new Dictionary<CardType, List<BakingCardData>>();
        foreach (CardManagerCardData data in cardDataInitializer)
        {
            cardDatas.Add(data.cardType, data.cardDatas);
        }

        drawTimeText.text = "Draw \n (+" + drawTimeIncrease + ")";
    }

    private void Start()
    {
        Draw();
    }

    public void Draw()
    {
        DiceManager.ResetReRolls();
        DiceManager.RollDice();

        if(cardHolder.childCount != 0)
        {
            for (int i = 0; i < cardHolder.childCount; i++)
            {
                Destroy(cardHolder.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < cardCount; i++)
        {
            (GameObject cardPrefab, BakingCardData data) = GetRandomCardData();

            BakingCard card = Instantiate(cardPrefab, cardHolder).GetComponent<BakingCard>();
            card.Initialize(data, canvas);
        }

        PlayerManager.UpdateTimeOfDay(drawTimeIncrease);
    }

    private (GameObject cardPrefab, BakingCardData data) GetRandomCardData()
    {
        int cardTypeLength = Enum.GetValues(typeof(CardType)).Length - 1;
        CardType randomCardType = (CardType)UnityEngine.Random.Range(0, cardTypeLength);
        BakingCardData data = cardDatas[randomCardType][UnityEngine.Random.Range(0, cardDatas[randomCardType].Count)];

        return (randomCardType == CardType.Ingredient ? ingredientCardPrefab : utensilCardPrefab, data);
    }

    public static List<BakingCardData> GetIngredients()
    {
        return Instance.cardDatas[CardType.Ingredient];
    }
}
