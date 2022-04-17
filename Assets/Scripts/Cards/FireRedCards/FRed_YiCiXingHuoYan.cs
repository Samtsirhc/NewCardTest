using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_YiCiXingHuoYan : FireRedCard
{
    
    public static bool showed = false;
    public override void SetLevelData()
    {
        base.SetLevelData();
        fire = 30;
        switch (cardLevel)
        {
            case 1:
                break;
            case 2:
                fire = 40;
                break;
            case 3:
                fire = 50;
                break;
            default:
                break;
        }
    }
    public override void OnGet()
    {
        if (showed)
        {
            BattleManager.Instance.playCost += 2;
            DeleteSelf();
        }
        showed = true;
        base.OnGet();
    }
    public override void OnUse()
    {
        base.OnUse();
        AddFire(fire);
        BattleManager.Instance.player.GetComponent<Player>().canGetFire = false;
    }
    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "火焰" + fire + "\n";
        description += "本场战斗无法再获得火焰\n唯一";
    }

}
