using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IBlue_JiHanZhiRen : IceBlueCard
{
    public int retainArmor;
    public int nowArmor;
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                retainArmor = 0;
                break;
            case 2:
                retainArmor = 5;
                break;
            case 3:
                retainArmor = 10;
                break;
            default:
                break;
        }
    }

    public override void OnUse()
    {
        base.OnUse();
        nowArmor = BattleManager.Instance.player.armor;
        damage = nowArmor >= retainArmor ? nowArmor-retainArmor : 0;
        CastDamage(damage);
        GetArmor(-1 * damage);
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        nowArmor = BattleManager.Instance.player.armor;
        damage = nowArmor >= retainArmor ? nowArmor-retainArmor : 0;
        description = "";
        description += "失去"+ damage + "点护甲,造成" + damage +"伤害\n";
        description += "至少保留"+ retainArmor +"点护甲";
    }
}
