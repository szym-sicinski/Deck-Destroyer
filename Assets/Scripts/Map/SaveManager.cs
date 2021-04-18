using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private SaveManager singleton;
    private MapGUIManager mapGUI;

    public int level;
    public Player[] players;
    private void Awake()
    {
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

    }
    private void OnSceneExit(Scene scene)
    {
        Debug.Log("Scene exit");
    }
    private void OnSceneChange(Scene scene1, Scene scene2)
    {
        Debug.Log("Scene change");
        foreach (Player player in players)
        {
            player.gameObject.SetActive(true);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Scene loaded");
    }
}
