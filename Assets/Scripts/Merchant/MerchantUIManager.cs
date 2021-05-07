using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUIManager : MonoBehaviour
{
    [SerializeField] private GameObject cardsToBuy;
    [SerializeField] private GameObject heroChoice;
    [SerializeField] private GameObject maleChoice;
    [SerializeField] private GameObject femaleChoice;
    [SerializeField] private TMP_Text moneyDisplay;

    private SaveManager saveManager;
    private Deck deck;

    private Card chosenCard;

    private const int NUMBER_OF_CARDS_TO_BUY = 8;
    private const int COST_OF_CARD = 700;
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        deck = FindObjectOfType<Deck>();

        foreach (Player player in saveManager.players)
        {
            if (player.CompareTag("Player Female"))
            {
                femaleChoice.SetActive(true);
            }
            else if (player.CompareTag("Player Male"))
            {
                maleChoice.SetActive(true);
            }
        }
        Debug.Log("Loading cards");
        for (int i = 0; i < NUMBER_OF_CARDS_TO_BUY; i++)
        {
            GameObject card = Instantiate(deck.cards[UnityEngine.Random.Range(0, deck.cards.Length)], cardsToBuy.transform);
            card.GetComponent<Button>().onClick.RemoveAllListeners();
            card.GetComponent<Button>().onClick.AddListener(
                () => { 
                        if (saveManager.money >= COST_OF_CARD)
                        {
                            chosenCard = card.GetComponent<Card>();
                            heroChoice.SetActive(true);
                        }
                       });
        }
        Debug.Log("Loading money");

        RefreshMoneyDisplay();
    }

    public void RefreshMoneyDisplay()
    {
        moneyDisplay.SetText(saveManager.money.ToString());
    }

    public void ChooseHero(string tag)
    {
        foreach (Player player in saveManager.players)
        {
            if(player.CompareTag(tag))
            {
                player.AddCard(chosenCard.Id);
                Destroy(chosenCard.gameObject);
                saveManager.money -= COST_OF_CARD;
                RefreshMoneyDisplay();
                heroChoice.SetActive(false);
                break;
            }
        }
    }
}
