using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FRed_HuoYanChongJi : FireRedCard
{
    public int maxFireCost; //最大火焰消耗
    public int nowFire;
    public override void SetLevelData()
    {
        base.SetLevelData();
        switch(cardLevel){
            case 1:
                maxFireCost = 10;
                break;
            case 2:
                maxFireCost = 15;
                break;
            case 3:
                maxFireCost = 22;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        nowFire = GetFire();
        damage = nowFire >= maxFireCost ? maxFireCost : nowFire; 
        CastDamage(damage);
        AddFire(-1 * damage);
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        nowFire = GetFire();
        damage = nowFire >= maxFireCost ? maxFireCost : nowFire; 
        description = "";
        description += "消耗"+ damage + "点火焰,造成" + damage +"伤害\n";
        description += "至多消耗"+ maxFireCost +"点火焰。";
    }


}
