using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_LianXuanQuan : FireRedCard
{
    private int damage;
    private int burnPoint;
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                burnPoint = 3;
                break;
            case 2:
                damage = 7;
                burnPoint = 4;
                break;
            case 3:
                damage = 10;
                burnPoint = 5;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(damage);
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                burnPoint = 3;
                break;
            case 2:
                damage = 7;
                burnPoint = 4;
                break;
            case 3:
                damage = 10;
                burnPoint = 5;
                break;
            default:
                break;
        }
        if (GetComboCount() >= 1)
        {
            BattleManager.Instance.player.fire += burnPoint;
        }
    }
}
