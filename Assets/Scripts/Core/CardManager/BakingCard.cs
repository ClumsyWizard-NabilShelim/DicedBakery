using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BakingCard : MonoBehaviour, IDropHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
{
    protected BakingCardData data;
    public bool Activated { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject costTextHolder;
    [SerializeField] private Transform costDiceHolder;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image portraitImage;

    public GameObject Prefab => data.Prefab;
    public CardType CardType => data.CardType;
    public string Name => data.Name;

    //Drag & Drop
    private new RectTransform transform;
    private Transform parent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    protected RectTransform spawnedPrefabTransform;

    public virtual void Initialize(BakingCardData data, Canvas canvas)
    {
        this.data = data;
        UpdateUI();

        transform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;

        parent = transform.parent;
        this.canvas = canvas;
        costDiceHolder.GetChild(data.Cost - 1).gameObject.SetActive(true);
    }

    protected virtual void UpdateUI()
    {
        nameText.text = data.Name;        
        portraitImage.sprite = data.Portrait;
    }

    protected void UpdateDescription(string find, string replace)
    {
        string description = data.Description.Replace(find, replace);
        descriptionText.text = description;
    }

    //UI Events
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.PlayAudio("CardSelect");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.PlayAudio("CardSelect");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Activated)
            return;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.0f;

        spawnedPrefabTransform = Instantiate(Prefab, BakingManager.Transform).GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public virtual void OnEndDrag()
    {
        if (!Activated)
            return;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Activated || eventData.pointerDrag == null)
            return;

        Dice dice = eventData.pointerDrag.GetComponent<Dice>();

        if (dice == null || dice.FaceValue < data.Cost)
            return;

        AudioManager.PlayAudio("Dice");
        Destroy(dice.gameObject);
        costDiceHolder.GetChild(data.Cost - 1).gameObject.SetActive(false);
        costDiceHolder.GetChild(costDiceHolder.childCount - 1).gameObject.SetActive(true);
        Activated = true;
    }

    public GameObject GetSpawnedPrefab()
    {
        return spawnedPrefabTransform.gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }
}
