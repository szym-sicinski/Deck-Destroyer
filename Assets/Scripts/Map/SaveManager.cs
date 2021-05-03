using System;
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
    public Player[] players;
    public static SaveManager instance;
    public bool isHardFight;

    private void Awake()
    {
        //TODO: SINGLETON
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

        //TODO: SINGLETON
        //SaveManager[] managers = FindObjectsOfType<SaveManager>();
        //if (managers != null)
        //{
        //    foreach (SaveManager manager in managers)
        //    {
        //        Destroy(manager.gameObject);
        //    }
        //}
        //else
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
                ResetStats();
                break;
            case 2: // Change to Fight scene
                PlayersEnabled(true);
                break;
            case 3: // Change to Merchant scene
                break;
        }
    }

    private void ResetStats()
    {
        foreach (Player player in players)
        {
            player.ResetStats();
        }
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
}
