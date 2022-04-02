using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_BingYuHuoZhiJian : FireRedCard
{
    public override void SetLevelData()
    {
        base.SetLevelData();
        switch (cardLevel)
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 7;
                break;
            case 3:
                damage = 12;
                break;
            default:
                break;
        }
    }
    public override int CastDamage(int num)
    {
        int _damage = 0;
        if (burn || icebound)
        {
            num *= burnFactor;
            _damage = BattleManager.Instance.enemy.TakeDamage(num);
            if (_damage > 0)
            {
                OnCauseDamage();
                GetArmor(_damage * iceboundFactor);
            }
        }
        else
        {
            _damage = BattleManager.Instance.enemy.TakeDamage(num);
            if (_damage > 0)
            {
                OnCauseDamage();
            }
        }
        playInfo["伤害"] += _damage;
        return _damage;
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
        description += "被燃烧或结冰时，同时有两种效果";
    }
}
