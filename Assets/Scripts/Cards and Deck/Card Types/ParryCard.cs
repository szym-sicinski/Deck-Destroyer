using UnityEngine;
public class ParryCard : Card
{
    [SerializeField] private int dmgVal;
    public override void Click()
    {
        if (value > owner.currentPower)
            return;
        owner.currentPower -= value;
        owner.targetingSystem.MarkTargets(this, Target.ENEMIES);
    }

    public override void Effect()
    {
        owner.targetingSystem.chosenFighter.TakeDmg(dmgVal + owner.Str);
        owner.AddBlock(dmgVal + owner.Dex);
    }
}
