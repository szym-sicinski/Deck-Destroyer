using System;
using System.Collections;
using UnityEngine;
public class Enemy : Fighter
{
    private EnemyBehaviour behaviour;
    private const int CHANCE_TO_ATTACK = 70; //% of chance to attack

    public void SetDifficulty(int difficulty) //Sets difficulty of enemy. Base: 10HP, 1 str & dex
    {
        maxHP = currentHP = 15 + (int) (difficulty * 0.75f);
        healthBar.SetMaxVal(maxHP);
        healthBar.SetVal(currentHP);
        str = currentStr = 1 + (int)(difficulty * 0.5f);
        dex = currentDex = 1 + (int)(difficulty * 0.5f);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        behaviour = GetComponent<EnemyBehaviour>();
        initialPos = transform.position;
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
    IEnumerator WaitAndCast() //This is subtitue of animation I dont have
    {
        yield return new WaitForSeconds(0.5f);
        behaviour.CastBuff();
    }

    protected override void EndRunEvent()
    {
        EndTurnTrigger();
    }
}
