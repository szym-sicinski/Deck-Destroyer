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
    private MusicManager musicManager;

    [SerializeField] private Canvas lostScreen;
    [SerializeField] private TMP_Text topScoreDisplay;
    [SerializeField] private TMP_Text currentScoreDisplay;
    [SerializeField] private TMP_Text subTitle;

    private const string congratulation = "Congratulation! You beat your top score";
    private const string keepTrying = "Keep Trying!";
    private const string drawScore = "You reached top score!";

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
        //turnManager.EndTurn();
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
        if (isVictoryscreen) //Show victory screen
        {
            //musicManager.PlaySound(SoundType.COINS); //THIS LINE BREAKS GAME FOR SOME REASON
            winScreen.gameObject.SetActive(true);
            goldReward.SetText(saveManager.RewardGold().ToString());
        }
        else //else show lost screen
        {
            int topScore = PlayerPrefs.GetInt("maxScore", 0);
            Debug.Log(topScore);
            if ( topScore < saveManager.level )
            {
                subTitle.SetText(congratulation);
                PlayerPrefs.SetInt("maxScore", saveManager.level);
            }
            else
                if( topScore == saveManager.level)
                    subTitle.SetText(drawScore);
                else
                    subTitle.SetText(keepTrying);

            topScoreDisplay.SetText(topScore.ToString());
            currentScoreDisplay.SetText(saveManager.level.ToString());

            lostScreen.gameObject.SetActive(true);

        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
