using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardStatBooster : MonoBehaviour, IDropHandler
{
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI valueText;
    private Action<int> onStatBoosted;

    private int setValue;
    private int limit;

    public void Initialize(string label, int limit, Action<int> onStatBoosted)
    {
        labelText.text = label + ": ";
        valueText.text = "0 / " + limit.ToString();
        this.limit = limit;
        this.onStatBoosted = onStatBoosted;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            setValue += eventData.pointerDrag.GetComponent<Dice>().FaceValue;
            onStatBoosted?.Invoke(setValue);

            if (setValue > limit)
                setValue = limit;

            valueText.text = setValue.ToString() + " / " + limit.ToString();
            AudioManager.PlayAudio("StatBooster");
            Destroy(eventData.pointerDrag);
        }
    }
}