using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_QuanTou : FireRedCard
{
    private int damage;
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 7;
                break;
            case 3:
                damage = 12;
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
                damage = 12;
                break;
            default:
                break;
        }
        CastDamage(damage);
    }
}
