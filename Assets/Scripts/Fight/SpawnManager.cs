using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour //CHANGED ORDER OF SCRIPT EXECUTION. It must spawn enemies before any script will try find enemies
{
    [SerializeField] private GameObject playersSpawnSets;
    [SerializeField] private GameObject enemiesSpawnSets;

    [SerializeField] private GameObject[] playersPrefabs;
    [SerializeField] private GameObject[] enemiesPrefabs;

    [SerializeField] private GameObject playerTeam;
    [SerializeField] private GameObject enemyTeam;

    //[SerializeField] public GameObject[] cards ; //List of cards prefabs. ID OF CARD MUST MATCH INDEX

    //private SaveManager saveManager;

    private Deck deck;

    void Start() //Spawns players to correct position. Spawns random number enemies and sets their difficulty. 
    {
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        deck = FindObjectOfType<Deck>();

        Player[] players = FindObjectsOfType<Player>();
        //int maxNumberOfEnemies = enemiesSpawnSets.transform.childCount;
        int maxNumberOfEnemies = 1;

        Transform chosenSpawnSet = playersSpawnSets.transform.Find("For " + players.Length);
        for (int i = 0; i < players.Length; i++)
        {
            Vector3 spawnPos = chosenSpawnSet.Find("Spawner " + (i + 1)).position;
            players[i].transform.position = spawnPos;
            players[i].InitialPos = spawnPos;
        }

        int numberOfEnemies = UnityEngine.Random.Range(1, maxNumberOfEnemies + 1);
        chosenSpawnSet = enemiesSpawnSets.transform.Find("For " + numberOfEnemies);

        //Some random difficulty
        int difficulty = saveManager.level / ((int) Math.Round(numberOfEnemies - 0.5f) + 1);
        if (saveManager.isHardFight)
        {
            difficulty += (int)Math.Round(difficulty / 4f);
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPos = chosenSpawnSet.Find("Spawner " + (i + 1)).position;
            Enemy enemy = Instantiate(enemiesPrefabs[UnityEngine.Random.Range(0, enemiesPrefabs.Length)], spawnPos , Quaternion.identity, enemyTeam.transform).GetComponent<Enemy>(); //UnityEngine.Random.Range(0, enemiesPrefabs.Length)
            enemy.SetDifficulty(difficulty);
        }
    }
    public void SpawnCard(int id, Transform panel, Player player) //spawns card of given id at specific panel and sets ownership of card
    {
        GameObject card = Instantiate(deck.cards[id], panel.position, Quaternion.identity,panel) as GameObject;
        card.GetComponent<Card>().owner = player;
    }
}