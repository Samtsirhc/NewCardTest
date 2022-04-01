using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_BingQiang : IceBlueCard
{
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                armor = 5;
                ice = 2;
                break;
            case 2:
                armor = 7;
                ice = 2;
                break;
            case 3:
                armor = 13;
                ice = 2;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        GetArmor(armor);
        BattleManager.Instance.player.ice += ice;
    }
    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "»¤¼×" + armor + ";";
        description += "º®±ù" + ice + "; ";
    }
}
