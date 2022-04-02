using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_LianXuanQuan : FireRedCard
{
    public override void SetLevelData()
    {
        base.SetLevelData();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                fire = 3;
                break;
            case 2:
                damage = 7;
                fire = 4;
                break;
            case 3:
                damage = 10;
                fire = 5;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage(damage);
        if (GetComboCount() >= 1)
        {
            AddFire(fire);
        }
        //后面有也算连携
        //else if (IsNextCardCombo())
        //{
        //    BattleManager.Instance.player.fire += fire;
        //}
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "伤害" + damage + ";";
        description += "连携：火焰" + fire + ";";
    }
}
