using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gift_Manager : MonoBehaviour
{
    [SerializeField] private int gifts;
    [SerializeField] private TextMeshProUGUI giftsText;
    private int maxGifts;

    private void Start()
    {
        GetAllGifts();
        UpdateText();
    }

    private void GetAllGifts()
    {
        maxGifts = FindObjectsOfType<Gift>().Length;
    }

    private void HasTakenAll()
    {
        if(gifts >= maxGifts)
        {
            FindObjectOfType<Transition>().PlayOutTransition("EndScene");
        }
    }

    public void AddGift(int value)
    {
        gifts++;
        UpdateText();
        HasTakenAll();
    }

    private void UpdateText()
    {
        giftsText.text = gifts.ToString() + "/" + maxGifts.ToString();
    }
}
