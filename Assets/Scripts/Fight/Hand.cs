using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private TMP_Text powerDisplay;
    [SerializeField] private FightUIManager fightUIManager;
    public void DestroyChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    internal void SetPowerDisplayValue(int power)
    {
        powerDisplay.text = power.ToString();
    }
    private void BlockEndTurnButton()
    {
        fightUIManager.EndTurnButtonInteraction(true);
    }
}
