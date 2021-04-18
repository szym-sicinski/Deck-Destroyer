using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard : Card
{
    [SerializeField] private int healValue;
    public override void Click()
    {
        if (value > owner.currentPower)
            return;
        owner.currentPower -= value;
        owner.targetingSystem.MarkTargets(this,Target.ALLIES);

    }

    public override void Effect()
    {
        owner.targetingSystem.chosenFighter.Heal(healValue);
    }
}
