using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    private Player[] players;
    private Enemy[] enemies;
    private readonly Queue<Enemy> enemiesQueue = new Queue<Enemy>();
    private SaveManager saveManager;

    private bool isStopped = true;
    private void Start()
    {
        players = FindObjectsOfType<Player>();
        enemies = FindObjectsOfType<Enemy>();
        saveManager = FindObjectOfType<SaveManager>();
    }

    private void Update()
    {
        if(isStopped) //Because isStopped is true on start this will start turns loop
        {
            isStopped = false;
            NextTurn();
        }
    }

    private void LoadEnemies() //Enqueue all enemies
    {
        foreach (Enemy enemy in enemies)
            enemiesQueue.Enqueue(enemy);
    }
    public void NextTurn()
    {
        if(enemiesQueue.Count == 0) //If queue of enemies is empty that means its player turn. During first loop queue is empty so first turn is always player
        {
            MakePlayersTurn();
            LoadEnemies();
        }
        else
        {
            enemiesQueue.Dequeue().MakeTurn();
        }
    }

    private void MakePlayersTurn() //Triggers filling hand of players 
    {
        foreach (Player player in players)
            player.MakeTurn();
    }

    public void EndTurn()
    {
        isStopped = true;
    }
    public void EndFightCheck() //Checks conditions of ending fight
    {
        int aliveCount = 0;

        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsAlive)
                aliveCount++;
        }
        if (aliveCount == 0)
        {
            WinFight();
            return;
        }
        aliveCount = 0;
        foreach (Player player in players)
        {
            if (player.IsAlive)
                aliveCount++;
        }
        if (aliveCount == 0)
        {
            LoseFight();
            return;
        }
    }

    private void LoseFight()
    {
        throw new NotImplementedException();
    }

    private void WinFight()
    {
        saveManager.PlayersEnabled(false); //Disable heroes
        SceneManager.LoadScene(1);
    }
}
