using UnityEngine;

class SteroidsCard : Card
{
    [SerializeField] private int strValue;
    public override void Click()
    {
        if (value > owner.currentPower)
            return;
        owner.currentPower -= value;

        owner.targetingSystem.MarkTargets(this, Target.ALLIES);
    }

    public override void Effect()
    {
        owner.targetingSystem.chosenFighter.BuffStr(strValue);
    }
}

