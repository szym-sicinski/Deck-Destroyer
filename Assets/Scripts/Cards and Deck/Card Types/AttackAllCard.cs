using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAllCard : Card
{
    [SerializeField] private int dmgValue = 2;

    public override void Effect()
    {

        Fighter[] targets = owner.targetingSystem.FindAllFighters(Target.ENEMIES);
        foreach (Fighter target in targets)
        {
            target.TakeDmg(dmgValue + owner.Str);
        }
        owner.RefreshPowerDisplay();
        Destroy(gameObject);
    }

    public override void Click()
    {
        if (value > owner.currentPower)
            return;
        owner.currentPower -= value;

        Effect();
    }
}
