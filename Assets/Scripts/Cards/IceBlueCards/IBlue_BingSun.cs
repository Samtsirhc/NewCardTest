using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_BingSun : IceBlueCard
{

    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                armor = 5;
                ice = 4;
                break;
            case 2:
                armor = 7;
                ice = 5;
                break;
            case 3:
                armor = 9;
                ice = 7;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        GetArmor(armor);
    }
    public override void OnGet()
    {
        base.OnGet();
        BattleManager.Instance.player.ice += ice;
    }
    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "»¤¼×" + armor + "\n";
        description += "»ñµÃ£ºº®±ù" + ice + "";
    }
}
