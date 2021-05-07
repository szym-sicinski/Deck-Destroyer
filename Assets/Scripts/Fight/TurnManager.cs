using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{

    private FightUIManager fightUIManager;
    private Player[] players;
    private Enemy[] enemies;
    private readonly Queue<Enemy> enemiesQueue = new Queue<Enemy>();
    private SaveManager saveManager;

    public bool isStopped = true;
    public bool endTurnBlockade;
    private void Start()
    {
        players = FindObjectsOfType<Player>();
        enemies = FindObjectsOfType<Enemy>();
        saveManager = FindObjectOfType<SaveManager>();
        fightUIManager = FindObjectOfType<FightUIManager>();
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
    public void EndFightCheck(Fighter fighter) //Checks conditions of ending fight
    {
        Debug.Log("End fight check");
        int aliveCount = 0;
        if (fighter is Player) //If its player check only if all players are dead
        {
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
        else //else check if all enemies are dead
        {
            Debug.Log("Enemy");
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsAlive)
                    aliveCount++;
            }
            Debug.Log("Alive: " + aliveCount);
            if (aliveCount == 0)
            {
                WinFight();
                return;
            }
        }
    }

    public void LoseFight()
    {
        fightUIManager.ShowEndScreen(false);
        foreach (Player player in saveManager.players)
            Destroy(player.gameObject);
        Destroy(saveManager.gameObject);
    }

    private void WinFight()
    {
        Debug.Log("fight won");
        fightUIManager.ShowEndScreen(true);
        saveManager.bGiveExp = true;
        saveManager.PlayersEnabled(false); //Disable heroes
    }
}
