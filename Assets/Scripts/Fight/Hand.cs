using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private TMP_Text powerDisplay;
    [SerializeField] private FightUIManager fightUIManager;

    private TurnManager turnManager;

    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }
    public void DestroyChildren() // Destroy cards in hand. Called in animation
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    public void SetPowerDisplayValue(int power) //Sets text of display as given parameter
    {
        powerDisplay.text = power.ToString();
    }
    private void BlockEndTurnButton() //Blocks end turn button. Called in animation
    {
        fightUIManager.EndTurnButtonInteraction(true);
    }
}
