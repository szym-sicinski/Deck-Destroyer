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
    private enum Side //Default side of hand on screen
    {
        LEFT,
        RIGHT
    }

    [SerializeField] private Side side;
    public int power;
    public int currentPower;
    private int experience;
    private FightUIManager fightUIManager;
    private SpawnManager spawnManager;

    private readonly List<int> deck = new List<int>(); //list of cards id
    private readonly List<int> trash = new List<int>();

    private const int START_CARDS_COUNT = 3;
    private const int LEVEL_UP_EXP = 1;

    [SerializeField] private Hand hand;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MakeStarterDeck();
        currentHP = maxHP;
        healthBar.SetMaxVal(currentHP);
        healthBar.SetVal(currentHP);
        gameObject.SetActive(false);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        fightUIManager = FindObjectOfType<FightUIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();

        FindHand();
        deck.Shuffle();

    }
    protected override void StopAnimator()
    {
        animator.enabled = false;
        turnManager.LoseFight();
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

    public bool GiveExp(int exp) //gives exp AND RETURNS TRUE IF LEVELUP
    {
        experience += exp;

        if(experience >= LEVEL_UP_EXP)
        {
            while (experience >= LEVEL_UP_EXP)
            {
                LevelUp();
                experience -= LEVEL_UP_EXP;
            } 
            return true;
        }
        return false;
    }

    private void LevelUp()
    {
        switch (UnityEngine.Random.Range(0,3)) //3 possible upgrades
        {
            case 0: //Max health by 20%
                ChangeMaxHP(0.2f);
                break;
            case 1: //+2 dex
                ChangeDex(2);
                break;
            case 2: //+3 str
                ChangeStr(3);
                break;
            default:
                break;
        }
        Debug.Log(tag + "Leveled Up");
    }

    private void ChangeStr(int str)
    {
        Str = currentStr = str;
    }

    private void ChangeDex(int dex)
    {
        Dex = currentDex = dex;
    }

    public void AddCard(int idOfChosenCard)
    {
        deck.Add(idOfChosenCard);
    }

    public void ResetStats()
    {
        currentDef = 0;
        currentDex = dex;
        currentStr = str;
        spriteRenderer.flipX = false;
        isMoving = false;
    }

    public void CastCardEffect()
    {
        targetingSystem.CastCardEffect();
    }
    public void MakeStarterDeck()
    {
        for (int i = 0; i < 2; i++)
        {
            deck.Add(0);
            deck.Add(1);
        }
        //deck.Add(2);
        //deck.Add(3);
        //deck.Add(4);
        //deck.Add(5);
        //deck.Add(6);
        //deck.Add(7);

        //deck.Shuffle();
    }

    public int Power { get => power; set => power = value; }
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
        for (int i = 0; i < START_CARDS_COUNT; i++)
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
    public void ChangeMaxHP(int hpDelta)
    {
        maxHP += hpDelta;
        healthBar.SetMaxVal(maxHP);
    }
    public void ChangeMaxHP(float hpDelta) //Hp delta % of boosted maxhp for example 0.2 = 20 %
    {
        maxHP += (int) (hpDelta * maxHP);
        healthBar.SetMaxVal(maxHP);
    }
    private void OnDestroy()
    {
        FindObjectOfType<SaveManager>().OnPlayerDie();
    }
}