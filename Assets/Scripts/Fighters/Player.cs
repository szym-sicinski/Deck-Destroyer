using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        int count = list.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int r = UnityEngine.Random.Range(i, count);
            T tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }
}


public class Player : Fighter
{
    private enum Side
    {
        LEFT,
        RIGHT
    }

    [SerializeField] private Side side;
    public int power;
    public int currentPower;
    private int gold;
    private FightUIManager fightUIManager;
    private SpawnManager spawnManager;

    private readonly List<int> deck = new List<int>(); //list of cards id
    private readonly List<int> trash = new List<int>(); 

    [SerializeField] private Hand hand;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }
    private void Start()
    {
        MakeStarterDeck();
        currentHP = maxHP;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        fightUIManager = FindObjectOfType<FightUIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();

        FindHand();

    }

    private void FindHand()
    {
        if (FindObjectsOfType<Player>().Length > 0)
        {
            Hand[] potentialHands = FindObjectsOfType<Hand>();
            string searchTag;
            if (side == Side.LEFT)
                searchTag = "Left Panel";
            else
                searchTag = "Right Panel";
            foreach (Hand potentialHand in potentialHands)
            {
                if (potentialHand.CompareTag(searchTag))
                {
                    hand = potentialHand;
                    break;
                }
            }
        }
        else
            hand = FindObjectOfType<Hand>();
    }

    public void CastCardEffect()
    {
        targetingSystem.CastCardEffect();
    }
    public void MakeStarterDeck()
    {
        for (int i = 0; i < 5; i++)
        {
            deck.Add(0);
            deck.Add(1);
        }
        deck.Add(2);
        deck.Add(3);

        deck.Shuffle();
    }

    internal void Rest()
    {
        currentHP = Math.Max(MaxHP, currentHP + (int)(currentHP * 0.35)); //Heal by 35%
    }



    public int Power { get => power; set => power = value; }
    public int Gold { get => gold; set => gold = value; }
    public void RefreshPowerDisplay()
    {
        hand.SetPowerDisplayValue(currentPower);
    }
    public override void MakeTurn()
    {
        if(currentHP <= 0)
        {
            return;
        }
        currentPower = power;
        if (trash.Count + deck.Count != 12)
            Debug.LogError("CARDS LEAK");
        RefreshPowerDisplay();
        fightUIManager.PlayerTurnStart();
        FillHand();
    }
    public override void SetRunTarget(Vector3 target)
    {
        base.SetRunTarget(target);
        fightUIManager.LockingGUI(false);
    }
    private void FillHand()
    {
        for (int i = 0; i < 5; i++)
        {
            if (deck.Count == 0)
            {
                TrashToDeck();
            }
            spawnManager.SpawnCard(deck[0], hand.transform, this);
            deck.RemoveAt(0);
        }
    }
    private void TrashToDeck()
    {
        foreach (int id in trash)
        {
            deck.Add(id);
        }

        deck.Shuffle();
        trash.Clear();
    }
    public void AddToTrash(int id)
    {
        trash.Add(id);
    }

    protected override void EndRunEvent()
    {
        fightUIManager.LockingGUI(true);
    }
}