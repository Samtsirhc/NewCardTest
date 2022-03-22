using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_FengNuYiQuan : FireRedCard
{
    private int damage;
    protected override void Start()
    {
        base.Start();
        burnFactor = 3;
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 7;
                break;
            case 3:
                damage = 10;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 7;
                break;
            case 3:
                damage = 10;
                break;
            default:
                break;
        }
        CastDamage(damage);
    }
}
