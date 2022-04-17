using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_ChaiHuo : FireRedCard
{
    protected override void OnCardBurn()
    {
        base.OnCardBurn();
        MyCard _left = GetLeftCard();
        MyCard _right = GetRightCard();
        if (_left != null)
        {
            _left.burn = true;
            _left.freezed = false;
        }
        if (_right != null)
        {
            _right.burn = true;
            _right.freezed = false;
        }
        BattleManager.Instance.playCost += 2;
        DeleteSelf();
    }
    public override void OnGet()
    {
        if (GetFire() <= 10)
        {
            BattleManager.Instance.playCost += 2;
            DeleteSelf();
        }
        base.OnGet();
    }

    protected override void UpdateDes()
    {
        description = "被燃烧后消失\n传递燃烧\n拥有至少10点火焰才出现";
    }
}
