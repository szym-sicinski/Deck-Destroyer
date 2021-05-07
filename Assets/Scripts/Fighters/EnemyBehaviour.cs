using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] //Behaviour specific for enemies
public class EnemyBehaviour : MonoBehaviour
{

    private Enemy owner;
    private const int BUFFS_COUNT = 4;
    private const int ATTACKS_COUNT = 2; //How many attacks and buffs are implemented

    private Fighter target;
    private int dmgToDeal; //I need to store that and deal that dmg with animation

    private void Start()
    {
        owner = GetComponent<Enemy>();
    }

    public void CastBuff() //Cast buff to random enemy
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
        switch (UnityEngine.Random.Range(0, ATTACKS_COUNT))
        {
            case 0:
                AttackSinglePlayer();
                break;
            case 1: //FIXME
                //StartCoroutine(nameof(AttackAllPlayers));
                AttackSinglePlayer();
                break;
            default:
                Debug.LogError("Cast attack out of Range");
                break;
        }
    }

    public void DealDmgTrigger() //Called from animation
    {
        target.TakeDmg(dmgToDeal);
        target.musicManager.PlaySound(SoundType.SMACK);
    }
    #region Attacks
    public IEnumerable AttackAllPlayers() //FIXME: WAIT
    {

        Fighter[] fighters = owner.targetingSystem.FindAllFighters(Target.ALLIES);
        int dmg = (int)Math.Round(owner.CurrentStr / 1.5f) + 2;
        foreach (Fighter fighter in fighters)
        {
            fighter.TakeDmg(dmg);
            //owner.particleSpawner.SpawnParticles(fighter.transform.position,ParticlesType.BLOOD);
        }
        Debug.Log("Wait");
        yield return new WaitForSeconds(1.8f);
        Debug.Log("End Turn");
        owner.EndTurnTrigger();
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
        if (target.CurrentHP / target.MaxHP > 0.65f) //If target is healthy just buff his str
            target.BuffStr(3);
        else
        {
            target.Heal();
            Debug.Log(target.ToString() + " healed");
        }
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
