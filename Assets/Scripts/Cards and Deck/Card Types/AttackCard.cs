using System;
using UnityEngine;
public class AttackCard : Card
{
    [SerializeField] private int dmgValue = 5;

    public override void Effect()
    {
        owner.currentPower -= value;
        owner.targetingSystem.chosenFighter.TakeDmg(dmgValue + owner.Str);

    }

    public override void Click()
    {
        if (value <= owner.currentPower)
            owner.targetingSystem.MarkTargets(this);
    }
}
