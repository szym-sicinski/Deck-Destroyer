using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target //Types of targets
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
    public void MarkTargets(Card card, Target target = Target.ENEMIES) //Marks fighters as targets and saves them in array. Saves card so later can cast effect 
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
    public void UnmarkTargets() //Unmarks marked targets and forgets saved card
    {
        if(targets != null)
            foreach (Fighter target in targets)
                target.MarkAsTarget(false);
        targets = null;
        chosenCard = null;
    }
    public void CastCardEffect() //Casts card effect, refreshes power display, destroys card and unmarks targets
    {
        chosenCard.Effect();
        chosenCard.owner.RefreshPowerDisplay();
        Destroy(chosenCard.gameObject);
        UnmarkTargets();
    }

    public Fighter FindRandomFighter(Target target) //Returns random fighter regarding to chosen type of target
    {
        Fighter[] potentialTargets;
        switch (target)
        {
          
            case Target.ALLIES:
                {
                    potentialTargets = FindObjectsOfType<Player>();
                    break;
                    //return potentialTargets[Random.Range(0, potentialTargets.Length)];
                }
            case Target.ENEMIES:
                {
                    potentialTargets = FindObjectsOfType<Enemy>();
                    break;
                    //return potentialTargets[Random.Range(0, potentialTargets.Length)];
                }
            case Target.ALL:
                {
                    potentialTargets = FindObjectsOfType<Fighter>();
                    break;
                    // potentialTargets[Random.Range(0, potentialTargets.Length)];
                }
            default: return null;
        }
        List<Fighter> result = new List<Fighter>();
        foreach(Fighter potentialTarget in potentialTargets)
        {
            if (potentialTarget.IsAlive)
                result.Add(potentialTarget);
        }
        return result[UnityEngine.Random.Range(0,result.Count)];
    }

    public void ChooseFighter(Fighter fighter) // Called after clicked on Fighter. Saves fighter given as parameter. Sets owner of card to run if card says so, if not casts card effect
    {
        chosenFighter = fighter;
        if (!chosenCard.isRunning)
            CastCardEffect();
        else
            chosenCard.owner.SetRunTarget(chosenFighter.transform.position);
    }

    public Fighter[] FindAllFighters(Target target) //Returns array containing fighters matching given target type
    {
        Fighter[] potentialTargets;
        switch (target)
        {
            case Target.ALLIES:
                {
                    potentialTargets = FindObjectsOfType<Player>();
                    break;
                }
            case Target.ENEMIES:
                {
                    potentialTargets = FindObjectsOfType<Enemy>();
                    break;
                }
            case Target.ALL:
                {
                    potentialTargets = FindObjectsOfType<Fighter>();
                    break;
                }
            default: return null;
        }
        List<Fighter> result = new List<Fighter>();
        foreach (Fighter potentialTarget in potentialTargets)
        {
            if (potentialTarget.IsAlive)
                result.Add(potentialTarget);
        }
        return result.ToArray();
    }
}
