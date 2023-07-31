using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;
using UnityEngine.UI;

public class DiceManager : Singleton<DiceManager>
{
    [Header("Dice")]
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private RectTransform diceHolder;
    [SerializeField] private int diceCount;

    [Header("Re Roll")]
    [SerializeField] private int maxReRolls;
    private int currentReRolls;
    [SerializeField] private int reRollCost;

    [Header("UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button rollButton;
    [SerializeField] private Button reRollButton;

    protected override void Awake()
    {
        base.Awake();
        
        rollButton.gameObject.SetActive(true);
        reRollButton.gameObject.SetActive(false);

        currentReRolls = 0;
    }

    public static void RollDice()
    {
        Instance.ReRoll(false);
    }

    public void Roll()
    {
        AudioManager.PlayAudio("Dice");
        for (int i = 0; i < diceCount; i++)
        {
            Dice dice = Instantiate(dicePrefab, diceHolder).GetComponent<Dice>();
            dice.Initialize(canvas);
        }
        rollButton.gameObject.SetActive(false);

        if(currentReRolls < maxReRolls)
            reRollButton.gameObject.SetActive(true);
    }

    public static void ResetReRolls()
    {
        Instance.currentReRolls = 0;
    }

    public void ReRoll(bool cutRollCost)
    {
        if (currentReRolls == maxReRolls)
            return;

        if(cutRollCost)
            PlayerManager.RemoveMoney(reRollCost);

        currentReRolls++;
        reRollButton.gameObject.SetActive(false);

        for (int i = 0; i < diceHolder.childCount; i++)
        {
            Destroy(diceHolder.GetChild(i).gameObject);
        }

        Roll();
    }
}
