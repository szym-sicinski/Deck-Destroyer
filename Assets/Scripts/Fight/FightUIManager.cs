using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUIManager : MonoBehaviour
{
    private TurnManager turnManager;
    private TargetingSystem targetingSystem;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private Button endTurnButton;
    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        targetingSystem = FindObjectOfType<TargetingSystem>();
    }
    private void ChangeInteractableOfCards(Transform transform, bool interactable = false) //Turns off all card from panel
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = interactable;
        }
    }
    public void EndTurnButtonClick()
    {
        EndTurnButtonInteraction(false);
        foreach (GameObject panel in panels)
        {
            panel.GetComponent<Animator>().SetBool("isHidden", true);
            ChangeInteractableOfCards(panel.transform);
        }
        targetingSystem.UnmarkTargets();
        turnManager.EndTurn();
    }
    public void PlayerTurnStart()
    {
        foreach (GameObject panel in panels)
        {
            panel.GetComponent<Animator>().SetBool("isHidden", false);
            ChangeInteractableOfCards(panel.transform,true);
        }
    }

    internal void EndTurnButtonInteraction(bool isInteractable)
    {
        endTurnButton.interactable = isInteractable;
    }

    internal void LockingGUI(bool bLocking)
    {
        EndTurnButtonInteraction(bLocking);
        foreach (GameObject panel in panels)
        {
            ChangeInteractableOfCards(panel.transform,bLocking);
        }
    }
}
