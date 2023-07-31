using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RentManager : MonoBehaviour
{
    [SerializeField] private GameObject rentMenu;
    private Action<bool> onDecisionMade;
    private int rentMoney;

    [Header("UI")]
    [SerializeField] private GameObject payButton;
    [SerializeField] private TextMeshProUGUI skipButtonText;
    [SerializeField] private TextMeshProUGUI rentAmountText;
    [SerializeField] private TextMeshProUGUI moneyAmountText;
    [SerializeField] private TextMeshProUGUI afterPaymentMoneyAmountText;
    [SerializeField] private TextMeshProUGUI comment;

    private void Awake()
    {
        rentMenu.SetActive(false);
    }

    public void ShowMenu(int rentMoney, int money, int monthsWihtoutRent, Action<bool> onDecisionMade)
    {
        this.onDecisionMade = onDecisionMade;
        rentMenu.SetActive(true);
        this.rentMoney = rentMoney;

        if(rentMoney > money)
        {
            payButton.SetActive(false);
            skipButtonText.text = "Can't Pay!";
        }
        else
        {
            payButton.SetActive(true);
            skipButtonText.text = "Later";
        }

        rentAmountText.text = rentMoney.ToString();
        moneyAmountText.text = money.ToString();
        afterPaymentMoneyAmountText.text = (money - rentMoney).ToString();

        if(monthsWihtoutRent == 0)
        {
            comment.text = "Hey, how is it going? Its that day of the month, please pay your rent. Hopefully you and your business is doing as well as my bank account!";
        }
        else if(monthsWihtoutRent == 1)
        {
            comment.text = "Looks like your small business is only getting smalled. Do pay the rent!";
        }
        else if(monthsWihtoutRent == 2)
        {
            comment.text = "Interesting how your small business will become non-existant if you miss this payment!";
        }
    }

    public void PayRent()
    {
        GameManager.RentPayed += rentMoney;
        onDecisionMade?.Invoke(true);
        rentMenu.SetActive(false);
    }
    public void SkipRent()
    {
        onDecisionMade?.Invoke(false);
        rentMenu.SetActive(false);
    }
}