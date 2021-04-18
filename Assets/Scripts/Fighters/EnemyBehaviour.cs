using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyBehaviour : MonoBehaviour
{

    private Enemy owner;
    private const int BUFFS_COUNT = 4;
    private const int ATTACKS_COUNT = 2;

    private Fighter target;
    private int dmgToDeal;
    private void Start()
    {
        owner = GetComponent<Enemy>();
    }

    public void CastBuff()
    {
        switch (UnityEngine.Random.Range(0, BUFFS_COUNT))
        {
            case 0:
                BuffStr();
                break;
            case 1:
                BuffDex();
                break;
            case 2:
                Heal();
                break;
            case 3:
                Block();
                break;
            default:
                Debug.LogError("Cast buff by Enemy out of range");
                break;
        }
        owner.EndTurnTrigger();
    }
    public void CastDmg()
    {
        switch (UnityEngine.Random.Range(0, ATTACKS_COUNT - 1))
        {
            case 0:
                AttackSinglePlayer();
                break;
            case 1:
                AttackAllPlayers();
                break;
            default:
                Debug.LogError("Cast attack out of Range");
                break;
        }
    }

    public void DealDmgTrigger()
    {
        target.TakeDmg(dmgToDeal);
    }
    #region Attacks
    private void AttackAllPlayers()
    {
        Fighter[] fighters = owner.targetingSystem.FindAllFighters(Target.ALLIES);
        int dmg = (int) Math.Round(owner.CurrentStr / 1.5f) + 2;
        foreach (Fighter fighter in fighters)
        {
            fighter.TakeDmg(dmg);
            owner.particleSpawner.SpawnParticles(fighter.transform.position,ParticlesType.BLOOD);
        }
    }

    private void AttackSinglePlayer()
    {
        target = owner.targetingSystem.FindRandomFighter(Target.ALLIES);
        dmgToDeal = 5 + owner.CurrentStr;

        owner.SetRunTarget(target.transform.position);
    }
    #endregion

    #region Buffs
    private void Block()
    {
        int block = 5;
        owner.AddBlock(block);
    }
    private void Heal()
    {
        Fighter target = owner.targetingSystem.FindRandomFighter(Target.ENEMIES);
        int heal = (int)Math.Round(owner.MaxHP / 15f * 100f);
        target.Heal(heal);
        Debug.Log(target.ToString() + " healed");
    }

    private void BuffDex()
    {
        Fighter target = owner.targetingSystem.FindRandomFighter(Target.ENEMIES);
        target.BuffDex(3);
        Debug.Log(target.ToString() + " buffed dex");
    }

    private void BuffStr()
    {
        Fighter target = owner.targetingSystem.FindRandomFighter(Target.ENEMIES);
        target.BuffStr(3);
        Debug.Log(target.ToString() + " buffed str");
    }
    #endregion
}
