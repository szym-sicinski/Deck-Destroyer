using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LocationType
{
    NORMAL_FIGHT,
    HARD_FIGHT,
    MERCHANT,
    CAMPFIRE,
    RANDOM
}
public class MapGUIManager : MonoBehaviour
{

    [SerializeField] private GameObject[] buttonsSpawners;
    [SerializeField] private GameObject[] buttonsPrefabs; //FIRST NORMAL FIGHT

    [SerializeField] private Canvas restInfo;



    private SaveManager saveManager;

    private const int CHANCE_TO_NORMAL_FIGHT = 60;

    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();

        SpawnLocations();
    }

    private void SpawnLocations()
    {
        int numberOfLocations = UnityEngine.Random.Range(1, 4); //max three locations


        switch (numberOfLocations)
        {
            case 1: //Simple spawn location at random spawner
                Instantiate(buttonsPrefabs[RandomLocation()], buttonsSpawners[UnityEngine.Random.Range(0, 3)].transform);
                break;
            case 2: //Select spawner excluded from spawning, spawn random location at others spawners
                int excludedSpawnPointIndex = UnityEngine.Random.Range(0, 3);
                for (int i = 0; i < 3; i++)
                {
                    if (i == excludedSpawnPointIndex)
                        continue;
                    Instantiate(buttonsPrefabs[RandomLocation()], buttonsSpawners[i].transform);
                }
                break;
            case 3: //Simple fill spawners with random locations
                foreach (GameObject spawner in buttonsSpawners)
                {
                    Instantiate(buttonsPrefabs[RandomLocation()], spawner.transform);
                }
                break;
            default:
                Debug.LogError("Wrong number of locations to spawn");
                break;
        }
    }

    private int RandomLocation()
    {
        if (UnityEngine.Random.Range(1, 101) < CHANCE_TO_NORMAL_FIGHT)
            return 0;
        else
            return UnityEngine.Random.Range(1, buttonsPrefabs.Length);
    }

    public void LocationClick(LocationType type)
    {
        saveManager.level++;
        switch (type)
        {
            case LocationType.NORMAL_FIGHT:
                SceneManager.LoadScene(2);
                break;
            case LocationType.HARD_FIGHT:
                saveManager.isHardFight = true;
                SceneManager.LoadScene(2);
                break;
            case LocationType.MERCHANT:
                SceneManager.LoadScene(3);
                break;
            case LocationType.CAMPFIRE: //Heal, show heal info, respawn locations
                //Debug.Log("Rest");
                Debug.Log(saveManager.players.Length);
                foreach (Player player in saveManager.players)
                {
                    Debug.Log("Rest : " + player.ToString());
                    player.Heal(false);
                }
                restInfo.gameObject.SetActive(true);
                foreach (GameObject spawner in buttonsSpawners) // Destroy all children of spawners
                {
                    for (int i = 0; i < spawner.transform.childCount; i++)
                    {
                        Destroy(spawner.transform.GetChild(i).gameObject);
                    }
                }
                SpawnLocations();
                break;
            case LocationType.RANDOM:
                LocationClick((LocationType)UnityEngine.Random.Range(0, 5));
                break;
            default:
                Debug.LogError("Invalid location type clicked");
                break;
        }
    }
}
