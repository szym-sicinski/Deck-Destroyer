using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playersSpawnSets;
    [SerializeField] private GameObject enemiesSpawnSets;

    [SerializeField] private GameObject[] playersPrefabs; //MAIN PLAYER FIRST
    [SerializeField] private GameObject[] enemiesPrefabs;

    [SerializeField] private GameObject playerTeam;
    [SerializeField] private GameObject enemyTeam;

    [SerializeField] private List<GameObject> cards = new List<GameObject>();

    //private SaveManager saveManager;

    private const int maxNumberOfEnemies = 1;

    void Start() //CHANGED ORDER OF SCRIPT EXECUTION
    {
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        Player[] players = FindObjectsOfType<Player>();
        //if (players.Length == 0) //Instantiation for tests
        //{
        //    Instantiate(playersPrefabs[0], playersSpawnSets.transform.Find("For 2").Find("Spawner 1").position, Quaternion.identity, playerTeam.transform);
        //    Instantiate(playersPrefabs[1], playersSpawnSets.transform.Find("For 2").Find("Spawner 2").position, Quaternion.identity, playerTeam.transform);
        //}

        //Debug.Break();


        Transform chosenSpawnSet = playersSpawnSets.transform.Find("For " + players.Length);
        for (int i = 0; i < players.Length; i++)
        {
            Vector3 spawnPos = chosenSpawnSet.Find("Spawner " + (i + 1)).position;
            players[i].transform.position = spawnPos;
            players[i].InitialPos = spawnPos;
            //players[i].transform.parent = playerTeam.transform;
            //Debug.Log(players[i].InitialPos + ", " + spawnPos);
        }


        int numberOfEnemies = UnityEngine.Random.Range(1, maxNumberOfEnemies + 1);
        

        chosenSpawnSet = enemiesSpawnSets.transform.Find("For " + numberOfEnemies);
        int difficulty = saveManager.level / ((int) Math.Round(numberOfEnemies - 0.5f) + 1);
        if (saveManager.isHardFight)
        {
            saveManager.isHardFight = false;
            difficulty += (int)Math.Round(difficulty / 4f);
        }
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPos = chosenSpawnSet.Find("Spawner " + (i + 1)).position;
            Enemy enemy = Instantiate(enemiesPrefabs[UnityEngine.Random.Range(0, 0)], spawnPos , Quaternion.identity, enemyTeam.transform).GetComponent<Enemy>(); //UnityEngine.Random.Range(0, enemiesPrefabs.Length)
            enemy.SetDifficulty(difficulty);
        }
    }
    public void SpawnCard(int id, Transform panel, Player player)
    {
        GameObject card = Instantiate(cards[id], panel.position, Quaternion.identity,panel) as GameObject;
        card.GetComponent<Card>().owner = player;
    }
}