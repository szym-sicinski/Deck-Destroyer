using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUIManager : MonoBehaviour // Handles UI of fight scene
{
    private TurnManager turnManager;
    private TargetingSystem targetingSystem;

    [SerializeField] private Hand[] hands;
    [SerializeField] private Button endTurnButton;
    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        targetingSystem = FindObjectOfType<TargetingSystem>();
    }
    private void ChangeInteractableOfCards(Transform transform, bool interactable = false) // Turns on/off all cards from panel
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = interactable;
        }
    }
    public void EndTurnButtonClick()// Blocks end turn button, shows hands, clears marking and ends turn
    {
        EndTurnButtonInteraction(false);
        ShowPanel(true);
        targetingSystem.UnmarkTargets();
        turnManager.EndTurn();
    }
    public void PlayerTurnStart() // Called on beninging of Player turn. Shows hands
    {
        ShowPanel(false);
    }

    private void ShowPanel(bool isPanelHidden) // Shows/hides panels
    {
        foreach (Hand hand in hands)
        {
            hand.GetComponent<Animator>().SetBool("isHidden", isPanelHidden);
            ChangeInteractableOfCards(hand.transform, !isPanelHidden);
        }
    }

    internal void EndTurnButtonInteraction(bool isInteractable) //Changes interactable of end turn button
    {
        endTurnButton.interactable = isInteractable;
    }

    internal void LockingGUI(bool bLocking) // Locks/unlocks interactable of end turn button and cards in hand
    {
        EndTurnButtonInteraction(bLocking);
        foreach (Hand hand in hands)
        {
            ChangeInteractableOfCards(hand.transform, bLocking);
        }
    }
}
