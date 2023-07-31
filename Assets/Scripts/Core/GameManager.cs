using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject gameOverMenu;

    //Game stats
    public static int DaySurvived;
    public static int TotalEarned;
    public static int RentPayed;
    public static int CakesBaked;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI daysSurvivedText;
    [SerializeField] private TextMeshProUGUI totalEarnedText;
    [SerializeField] private TextMeshProUGUI rentPayedText;
    [SerializeField] private TextMeshProUGUI cakesBakedText;

    protected override void Awake()
    {
        base.Awake();
        gameOverMenu.SetActive(false);
    }

    public static void GameOver()
    {
        Instance.UpdateUI();
        Instance.gameOverMenu.SetActive(true);
    }

    private void UpdateUI()
    {
        daysSurvivedText.text = DaySurvived.ToString();
        totalEarnedText.text = TotalEarned.ToString();
        rentPayedText.text = RentPayed.ToString();
        cakesBakedText.text = CakesBaked.ToString();
    }

    public void MainMenu()
    {
        SceneManagement.Load("MainMenu");
    }
    public void Retry()
    {
        SceneManagement.Reload();
    }
}