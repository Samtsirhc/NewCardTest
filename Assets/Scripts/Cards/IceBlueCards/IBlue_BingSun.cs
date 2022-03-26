using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_BingSun : IceBlueCard
{
    private int growIce;

    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                armor = 5;
                growIce = 2;
                break;
            case 2:
                armor = 7;
                growIce = 3;
                break;
            case 3:
                armor = 9;
                growIce = 4;
                break;
            default:
                break;
        }
    }
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        BattleManager.Instance.player.ice += growIce;
    }
    public override void OnUse()
    {
        base.OnUse();
        GetArmor(armor);
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "»¤¼×" + armor + ";";
        description += "±£Áô£ºº®±ù" + growIce + "; ";
    }
}
