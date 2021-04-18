using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCard : Card
{
    [SerializeField] private int defValue;
    public override void Click()
    {
        if (value > owner.currentPower)
            return;
        owner.currentPower -= value;

        Effect();

    }

    public override void Effect()
    {
        owner.AddBlock(defValue);
        Destroy(gameObject);
        owner.RefreshPowerDisplay();
    }
}
