using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightUIManager : MonoBehaviour // Handles UI of fight scene
{
    private TurnManager turnManager;
    private TargetingSystem targetingSystem;
    private SaveManager saveManager;

    [SerializeField] private Canvas lostScreen;

    [SerializeField] private Canvas winScreen;
    [SerializeField] private TMP_Text goldReward;

    [SerializeField] private Hand[] hands;
    [SerializeField] private Button endTurnButton;
    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        targetingSystem = FindObjectOfType<TargetingSystem>();
        saveManager = FindObjectOfType<SaveManager>();
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
            ChangeInteractableOfCards(hand.transform, !isPanelHidden); //In case some cards stay from other round
        }
    }
    public void EndTurnButtonInteraction(bool isInteractable) //Changes interactable of end turn button
    {
        endTurnButton.interactable = isInteractable;
    }

    public void LockingGUI(bool bLocking) // Locks/unlocks interactable of end turn button and cards in hand
    {
        EndTurnButtonInteraction(bLocking);
        foreach (Hand hand in hands)
        {
            ChangeInteractableOfCards(hand.transform, bLocking);
        }
    }
    public void ShowEndScreen(bool isVictoryscreen)
    {
        if (isVictoryscreen)
        {
            winScreen.gameObject.SetActive(true);
            goldReward.SetText(saveManager.RewardGold().ToString());

        }
        else
            lostScreen.gameObject.SetActive(true);
    }
}
