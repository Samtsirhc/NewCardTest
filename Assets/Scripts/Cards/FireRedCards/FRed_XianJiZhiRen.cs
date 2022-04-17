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
        freezed = false;
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
        description += "ÉËº¦" + damage + "\n";
        description += "Ï×¼À£ºÈ¼ÉÕÊ±²»ÏûºÄ»ðÑæ£¬µ«ÊÜµ½" + selfDamage + "ÉËº¦";
    }
}
