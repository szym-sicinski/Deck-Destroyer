using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

abstract public class Card : MonoBehaviour
{
    [SerializeField] protected int value; //Cost of casting card
    [SerializeField] protected string cardName;
    [SerializeField] protected string describtion;
    [SerializeField] protected int id;

    public bool isRunning; //if true owner will run and attack target. Works only on single targets
    [HideInInspector] public Player owner;

    [SerializeField] protected Target target; //Who can be affected by card
    [SerializeField] protected TMP_Text valueDisplay;
    [SerializeField] protected TMP_Text titleDisplay;

    public int Id { get => id; }
    protected Target Target { get => target; }

    abstract public void Effect(); //what card does. if character wont run to the target you need to destroy card gameobject at en of that function

    public abstract void Click();
    //Call Effect function when overridining this function if don't want selecting target. 
    //use owner.targetingSystem.MarkTargets(this, target); if want single choosable target
    private void OnDestroy() //Before Destroy add card id to trash of owner
    {
        if (owner != null)
            owner.AddToTrash(id);
    }
    protected void Awake()
    {
        valueDisplay.text = value.ToString();
        titleDisplay.text = cardName.ToString();
        GetComponent<Button>().onClick.AddListener(Click);
    }
    protected void SetOwner(Player player)
    {
        owner = player;
    }
}

