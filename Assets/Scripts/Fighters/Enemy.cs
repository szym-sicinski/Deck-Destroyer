using System.Collections;
using UnityEngine;
public class Enemy : Fighter
{
    private EnemyBehaviour behaviour;
    private const int CHANCE_TO_ATTACK = 75;

    public void SetDifficulty(int difficulty)
    {

    }
    protected override void Start()
    {
        base.Start();
        behaviour = GetComponent<EnemyBehaviour>();
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
            StartCoroutine("WaitAndCast");
        }
    }
    IEnumerator WaitAndCast()
    {
        yield return new WaitForSeconds(1.5f);
        behaviour.CastBuff();
    }

    protected override void EndRunEvent()
    {
        EndTurnTrigger();
    }
}
