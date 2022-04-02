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
            _left.icebound = false;
        }
        if (_right != null)
        {
            _right.burn = true;
            _right.icebound = false;
        }
        DeleteSelf();
    }
    public override void OnGet()
    {
        if (GetFire() <= 10)
        {
            DeleteSelf();
        }
        base.OnGet();
    }

    protected override void UpdateDes()
    {
        description = "被燃烧后消失，传递燃烧；拥有至少10点火焰才出现";
    }
}
