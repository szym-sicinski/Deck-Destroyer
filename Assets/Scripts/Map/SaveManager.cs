﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private SaveManager singleton;
    private MapGUIManager mapGUI;

    [SerializeField] private GameObject[] playersPrefabs;
    public int level;
    [HideInInspector] public Player[] players;
    public static SaveManager instance;
    public bool isHardFight;
    public int money;
    public bool bGiveExp;

    private const int GOLD_MIN = 25;
    private const int GOLD_MAX = 75;
    private const float GOLD_BOOST_HARD_FIGHT = 1.5f; //50% of gold boost after hard fight


    private void Awake()
    {
        // SINGLETON
        if (instance == null)
            instance = this;
        else
            if (instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        mapGUI = FindObjectOfType<MapGUIManager>();

        SceneManager.sceneUnloaded += OnSceneExit;
        SceneManager.activeSceneChanged += OnSceneChange;
        singleton = this;


        players[0] = Instantiate(playersPrefabs[0]).GetComponent<Player>();
        players[1] = Instantiate(playersPrefabs[1]).GetComponent<Player>();
        //Instantiate(playersPrefabs[1]);
    }

    private void OnSceneExit(Scene scene)
    {
        //Debug.Log("Scene exit");
    }
    private void OnSceneChange(Scene scene1, Scene scene2)
    {
        switch (scene2.buildIndex)
        {
            case 1: // Change to map scene
                if(bGiveExp)
                {
                    GiveEXP();
                }
                ResetStats();
                break;
            case 2: // Change to Fight scene
                PlayersEnabled(true);
                break;
            case 3: // Change to Merchant scene
                break;
        }
    }
    private void GiveEXP() //Gives exp and if no level up then loads map scene
    {
        int exp = 1 + (isHardFight ? 5 : 0);
        StatsDisplayManager statsDisplayManager = FindObjectOfType<StatsDisplayManager>();
        foreach(Player player in players)
        {
            if (player.GiveExp(exp))
            {
                statsDisplayManager.ShowStats(true);
            }
        }
    }
    private void ResetStats()
    {
        foreach (Player player in players)
        {
            player.ResetStats();
        }
        isHardFight = false;
        bGiveExp = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        //Debug.Log("Scene loaded");
    }
    public void PlayersEnabled(bool enabled)
    {
        foreach (Player player in players)
        {
            player.gameObject.SetActive(enabled);
        }
    }

    public int RewardGold() //Rewards player with gold AND RETURNS GOLD VALUE
    {
        int moneyDelta = UnityEngine.Random.Range(GOLD_MIN, GOLD_MAX);
        if (isHardFight)
            moneyDelta =(int) GOLD_BOOST_HARD_FIGHT * moneyDelta;
        money += moneyDelta;
        return moneyDelta;
    }
}
