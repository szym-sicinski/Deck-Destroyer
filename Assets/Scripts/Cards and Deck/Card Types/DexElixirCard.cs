using UnityEngine;

class DexElixirCard : Card
{
    [SerializeField] private int dexValue;
    public override void Click()
    {
        if (value > owner.currentPower)
            return;
        owner.currentPower -= value;

        owner.targetingSystem.MarkTargets(this, Target.ALLIES);
    }

    public override void Effect()
    {
        owner.targetingSystem.chosenFighter.BuffDex(dexValue);
    }
}