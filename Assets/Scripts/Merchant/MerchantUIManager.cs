using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Deck deck;

    void Start()
    {
        deck = FindObjectOfType<Deck>();

        for (int i = 0; i < 8; i++)
        {
            GameObject card = Instantiate(deck.cards[UnityEngine.Random.Range(0, deck.cards.Length)], panel.transform);
            card.GetComponent<Button>().onClick.RemoveAllListeners();
            card.GetComponent<Button>().onClick.AddListener(() => { Debug.Log("Kek"); });
        }
    }
}
