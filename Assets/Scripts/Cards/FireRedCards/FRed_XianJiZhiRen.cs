using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FRed_XianJiZhiRen : FireRedCard
{
    public int selfDamage = 5;

    public override void SetLevelData()
    {
        base.SetLevelData();
        switch (cardLevel)
        {
            case 1:
                damage = 7;
                break;
            case 2:
                damage = 10;
                break;
            case 3:
                damage = 15;
                break;
            default:
                break;
        }
    }
    protected override void PointerClick(BaseEventData arg0)
    {
        base.PointerClick(arg0);
        if (Input.GetKey(KeyCode.F))
        {
            SacrificeBurn();
        }
    }

    protected void SacrificeBurn()
    {
        burn = true;
        icebound = false;
        DamageSelf(selfDamage);
    }

    public override void OnUse()
    {
        base.OnUse();
        CastDamage(damage);
    }
    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "伤害" + damage + ";";
        description += "献祭：燃烧自己而不消耗火焰，受到" + selfDamage + "伤害";
    }
}
