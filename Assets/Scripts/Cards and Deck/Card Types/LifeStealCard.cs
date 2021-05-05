using UnityEngine;
public class LifeStealCard : Card
{
    [SerializeField] private int dmgValue;

    public override void Effect()
    {
        owner.currentPower -= value;
        owner.targetingSystem.chosenFighter.TakeDmg(dmgValue + owner.Str);
        owner.Heal(0.1f);

    }

    public override void Click()
    {
        if (value <= owner.currentPower)
            owner.targetingSystem.MarkTargets(this);
    }
}
