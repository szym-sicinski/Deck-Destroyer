using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
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

    private List<int> deck = new List<int>(); //Array of cards id
    private List<int> trash = new List<int>();
    [SerializeField]private Hand hand;

    protected override void Start()
    {
        base.Start();
        fightUIManager = FindObjectOfType<FightUIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();

        for (int i = 0; i < 5; i++)
        {
            deck.Add(0);
            deck.Add(1);
        }
        deck.Add(2);
        deck.Add(3);

        deck.Shuffle();
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

    // Eq:

    //private int wornWeapon = 0;
    //private int wornShield = 0;
    //private int wornHelmet = 0;
    //private int wornArmor = 0;

    //private bool[] unlockedShields = new bool [2];
    //private bool[] unlockedArmors = new bool [3];
    //private bool[] unlockedHelmets = new bool [2];
    //private bool[] unlockedWeapons = new bool [3];

    //private Helmet helmetSlot;
    //private Weapon weaponSlot;
    //private Shield shieldSlot;
    //private Armor armorSlot;
    public Player()
    {
        MaxHP = CurrentHP = 100;
        Str = CurrentStr = 0;
        Dex = CurrentDex = 0;
        power = 3;
        CurrentDef = 0;
        gold = 150;
        //for(int i=0;i<2; i++)
        //{
        //    unlockedShields[i] = false;
        //    unlockedArmors[i] = false;
        //    unlockedHelmets[i] = false;
        //    unlockedWeapons[i] = false;
        //}
        //unlockedArmors[2] = false;
        //unlockedWeapons[2] = false;

        //// Starting eq:
        //unlockedArmors[0] = true;
        //unlockedWeapons[0] = true;

        //ChangeArmor(new LightArmor());
        //ChangeWeapon(new LightWeapon());

        //wornArmor = 1;
        //wornWeapon = 1;
    }
    //public void ChangeWeapon(Weapon weapon)
    //{
    //    if (weaponSlot != null)
    //    {
    //        Dex -= weaponSlot.DefValue;
    //        Str -= weaponSlot.StrValue;
    //    }
    //    weaponSlot = weapon;
    //    Dex += weapon.DefValue;
    //    Str += weapon.StrValue;
    //}
    //public void ChangeHelmet(Helmet helmet)
    //{
    //    if (helmetSlot != null)
    //    {
    //        Dex -= helmetSlot.DefValue;
    //        Str -= helmetSlot.StrValue;
    //    }
    //    helmetSlot = helmet;
    //    Dex += helmet.DefValue;
    //    Str += helmet.StrValue;

    //}
    //public void ChangeShield(Shield shield)
    //{
    //    if (shieldSlot != null)
    //    {
    //        Dex -= shieldSlot.DefValue;
    //        Str -= shieldSlot.StrValue;
    //    }

    //    shieldSlot = shield;
    //    Dex += shield.DefValue;
    //    Str += shield.StrValue;
    //}
    //public void ChangeArmor(Armor armor)
    //{
    //    if (armorSlot != null)
    //    {
    //        Dex -= armorSlot.DefValue;
    //        Str -= armorSlot.StrValue;
    //    }
    //    armorSlot = armor;
    //    Dex += armor.DefValue;
    //    Str += armor.StrValue;

    //}


    public int Power { get => power; set => power = value; }
    public int Gold { get => gold; set => gold = value; }
    public void RefreshPowerDisplay()
    {
        hand.SetPowerDisplayValue(currentPower);
    }
    public override void MakeTurn()
    {
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

    //public int WornWeapon { get => wornWeapon; set => wornWeapon = value; }
    //public int WornShield { get => wornShield; set => wornShield = value; }
    //public int WornHelmet { get => wornHelmet; set => wornHelmet = value; }
    //public int WornArmor { get => wornArmor; set => wornArmor = value; }
    //public bool[] UnlockedShields { get => unlockedShields; set => unlockedShields = value; }
    //public bool[] UnlockedArmors { get => unlockedArmors; set => unlockedArmors = value; }
    //public bool[] UnlockedHelmets { get => unlockedHelmets; set => unlockedHelmets = value; }
    //public bool[] UnlockedWeapons { get => unlockedWeapons; set => unlockedWeapons = value; }
    //internal Helmet HelmetSlot { get => helmetSlot; set => helmetSlot = value; }
    //internal Shield ShieldSlot { get => shieldSlot; set => shieldSlot = value; }
    //internal Weapon WeaponSlot { get => weaponSlot; set => weaponSlot = value; }
    //internal Armor ArmorSlot { get => armorSlot; set => armorSlot = value; }
}