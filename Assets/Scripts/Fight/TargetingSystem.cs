using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target
{
    ENEMIES,
    ALLIES,
    ALL,
    NONE
}
public class TargetingSystem : MonoBehaviour
{
    public Fighter[] targets;
    public Card chosenCard;
    public Fighter chosenFighter;
    public void MarkTargets(Card card, Target target = Target.ENEMIES)
    {
        UnmarkTargets();
        if (target == Target.NONE)
            return;
        chosenCard = card;
        switch (target)
        {
            case Target.ENEMIES:
                targets = FindObjectsOfType<Enemy>();
                break;
            case Target.ALLIES:
                targets = FindObjectsOfType<Player>();
                break;
            case Target.ALL:
                targets = FindObjectsOfType<Fighter>();
                break;
        }

        foreach (Fighter fighter in targets)
            fighter.MarkAsTarget(true);
    }
    public void UnmarkTargets()
    {
        if(targets != null)
            foreach (Fighter target in targets)
                target.MarkAsTarget(false);
        targets = null;
        chosenCard = null;
    }
    public void CastCardEffect()
    {
        chosenCard.Effect();
        chosenCard.owner.RefreshPowerDisplay();
        Destroy(chosenCard.gameObject);
        UnmarkTargets();
    }

    public Fighter FindRandomFighter(Target target)
    {
        switch (target)
        {
            case Target.ALLIES:
                {
                    Fighter[] potentialTargets = FindObjectsOfType<Player>();
                    return potentialTargets[Random.Range(0, potentialTargets.Length)];
                }
            case Target.ENEMIES:
                {
                    Fighter[] potentialTargets = FindObjectsOfType<Enemy>();
                    return potentialTargets[Random.Range(0, potentialTargets.Length)];
                }
            case Target.ALL:
                {
                    Fighter[] potentialTargets = FindObjectsOfType<Fighter>();
                    return potentialTargets[Random.Range(0, potentialTargets.Length)];
                }
            default: return null;
        }
    }

    public void ChooseFighter(Fighter fighter)
    {
        chosenFighter = fighter;
        if (!chosenCard.isRunning)
            CastCardEffect();
    }

    public Fighter[] FindAllFighters(Target target)
    {
        switch (target)
        {
            case Target.ALLIES:
                {
                    return FindObjectsOfType<Player>();
                }
            case Target.ENEMIES:
                {
                    return FindObjectsOfType<Enemy>();
                }
            case Target.ALL:
                {
                    return FindObjectsOfType<Fighter>();
                }
            default: return null;
        }
    }
}
