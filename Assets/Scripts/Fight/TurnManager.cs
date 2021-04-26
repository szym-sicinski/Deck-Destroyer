using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    //private LinkedList<Fighter> fighters = new LinkedList<Fighter>();

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
        //LoadEnemies();
    }

    private void Update()
    {
        if(isStopped)
        {
            isStopped = false;
            NextTurn();
        }
    }

    private void LoadEnemies()
    {
        //fighters.AddFirst(FindObjectOfType<Player>());
        //foreach (Fighter enemy in FindObjectsOfType<Enemy>())
        //{
        //    fighters.AddLast(enemy);
        //}

        foreach (Enemy enemy in enemies)
            enemiesQueue.Enqueue(enemy);
    }
    public void NextTurn()
    {
        if(enemiesQueue.Count == 0)
        {
            MakePlayersTurn();
            LoadEnemies();
        }
        else
        {
            enemiesQueue.Dequeue().MakeTurn();
        }


        //fighters.First.Value.MakeTurn();
        //fighters.RemoveFirst();
        //if (fighters.Count == 0)
        //    LoadEnemies();
    }

    private void MakePlayersTurn()
    {
        foreach (Player player in players)
            player.MakeTurn();
    }

    public void EndTurn()
    {
        isStopped = true;
    }
    public void EndFightCheck()
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
        saveManager.PlayersEnabled(false);
        SceneManager.LoadScene(1);
    }
    //public void KickFighter(Fighter fighter)
    //{
    //    LinkedListNode<Fighter> head = fighters.First;
    //    if(fighters.Count == 1)
    //    {
    //        fighters.RemoveFirst();
    //        LoadEnemies();
    //        return;
    //    }
    //    while (head != null)
    //    {
    //        var nextNode = head.Next;
    //        if (head.Value == fighter)
    //        {
    //            fighters.Remove(head);
    //            break;
    //        }
    //        head = nextNode;
    //    }
    //}
}
