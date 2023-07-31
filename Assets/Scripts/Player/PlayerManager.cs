using System;
using UnityEngine;
using ClumsyWizard.Utilities;
using TMPro;

public enum TimeOfDay
{
    Morning,
    Evening,
    Night
}

public enum Month
{
    January,
    February,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}

[Serializable]
public struct DateTime
{
    [field: SerializeField] public int Day { get; private set; }
    [SerializeField] private Month month;
    public Month Month
    {
        get
        {
            return month;
        }

        set
        {
            month = value;

            if ((int)month > 12)
                month = 0;
        }
    }

    public DateTime(int day, Month month)
    {
        Day = day;
        this.month = month;
    }

    public void UpdateDateTime()
    {
        Day++;

        if(Day > 30)
        {
            Day = 1;
            Month = Month + 1;
        }
    }

    public void AddDays(int days)
    {
        for (int i = 0; i < days; i++)
        {
            UpdateDateTime();
        }
    }
}

public class PlayerManager : Singleton<PlayerManager>
{
    private RentManager rentManager;
    [Header("Money")]
    [SerializeField] private int startingMoney;
    private int money;
    [SerializeField] private int rentMoney;
    private static DateTime rentDueData;
    [SerializeField] private int rentInterval;
    [SerializeField] private int maxMonthsWithoutRentPayed;
    private static int monthsWithoutRentPayed;


    [Header("Date Time")]
    [SerializeField] private DateTime startingDate;
    private static TimeOfDay timeOfDay;
    private static DateTime currentDateTime;
    private static int timeInHours;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI timeOfDayText;
    [SerializeField] private TextMeshProUGUI currentDateTimeText;
    [SerializeField] private TextMeshProUGUI rentAmountText;
    [SerializeField] private TextMeshProUGUI rentDueDateText;

    protected override void Awake()
    {
        base.Awake();

        rentManager = GetComponent<RentManager>();

        money = startingMoney;

        timeOfDay = TimeOfDay.Morning;
        currentDateTime = startingDate;

        rentDueData = new DateTime(startingDate.Day, startingDate.Month);
        rentDueData.AddDays(rentInterval);
    }

    //Money
    public static void AddMoney(int amount)
    {
        GameManager.TotalEarned = Instance.money + amount;
        Instance.money += amount;
        Instance.UpdateUI();
    }

    public static void RemoveMoney(int amount)
    {
        Instance.money -= amount;

        if (Instance.money < 0)
            Instance.money = 0;

        Instance.UpdateUI();
    }

    //Rent
    private static void UpdateRent(int newRent)
    {
        Instance.rentMoney = newRent;
        rentDueData.AddDays(Instance.rentInterval);
        Instance.UpdateUI();
    }

    //Time & Data
    public static void UpdateTimeOfDay(int hours)
    {
        timeInHours += hours;

        if(timeInHours > 24)
            timeInHours = timeInHours - 24;

        if(timeInHours != 0 && timeInHours % 8 == 0)
        {
            if (timeOfDay == TimeOfDay.Night)
            {
                UpdateCurrentDateTime();
                timeOfDay = TimeOfDay.Morning;
            }
            else
            {
                timeOfDay++;
            }
        }

        Instance.UpdateUI();

        if (rentDueData.Day == currentDateTime.Day && rentDueData.Month == currentDateTime.Month)
        {
            Instance.rentManager.ShowMenu(Instance.rentMoney, Instance.money, Instance.maxMonthsWithoutRentPayed, (bool payed) =>
            {
                if(payed)
                {
                    RemoveMoney(Instance.rentMoney);
                    UpdateRent(Instance.rentMoney);
                    monthsWithoutRentPayed = 0;
                }
                else
                {
                    UpdateRent(Instance.rentMoney + Mathf.RoundToInt(Instance.rentMoney * 0.15f));
                    monthsWithoutRentPayed++;
                    if (monthsWithoutRentPayed >= Instance.maxMonthsWithoutRentPayed)
                        GameManager.GameOver();
                }
            });
        }
    }

    private static void UpdateCurrentDateTime()
    {
        GameManager.DaySurvived++;
        currentDateTime.UpdateDateTime();
    }

    //UI
    public void UpdateUI()
    {
        moneyText.text = money.ToString();
        timeOfDayText.text = timeOfDay.ToString();
        currentDateTimeText.text = currentDateTime.Day + " " + currentDateTime.Month.ToString();
        rentAmountText.text = rentMoney.ToString();
        rentDueDateText.text = "Due on: " + rentDueData.Day + " " + rentDueData.Month.ToString();
    }
}