using System;
using System.Collections;
using UnityEngine;
public class Enemy : Fighter
{
    private EnemyBehaviour behaviour;
    private const int CHANCE_TO_ATTACK = 75;

    public void SetDifficulty(int difficulty)
    {
        maxHP =  10 + (int) (difficulty * 0.6f);
        healthBar.SetMaxVal(maxHP);
        str = currentStr = 1 + (int)(difficulty * 0.5f);
        currentDex = dex = 1 + (int)(difficulty * 0.5f);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        behaviour = GetComponent<EnemyBehaviour>();
        initialPos = transform.position;
        currentHP = maxHP;
    }
    public override void MakeTurn()
    {
        if(currentHP <= 0)
        {
            EndTurnTrigger();
            return;
        }
        if(UnityEngine.Random.Range(1,100)<CHANCE_TO_ATTACK)
        {
            behaviour.CastDmg();
        }
        else
        {
            StartCoroutine(nameof(WaitAndCast));
        }
    }
    IEnumerator WaitAndCast()
    {
        yield return new WaitForSeconds(1.75f);
        behaviour.CastBuff();
    }

    protected override void EndRunEvent()
    {
        EndTurnTrigger();
    }
}
