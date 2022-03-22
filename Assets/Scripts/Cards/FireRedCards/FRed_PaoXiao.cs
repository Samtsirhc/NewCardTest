using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRed_PaoXiao : FireRedCard
{
    private int burnPoint;
    private int burnPointStart;
    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                burnPoint = 3;
                burnPointStart = 2;
                break;
            case 2:
                burnPoint = 4;
                burnPointStart = 2;
                break;
            case 3:
                burnPoint = 5;
                burnPointStart = 3;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        switch (cardLevel)
        {
            case 1:
                burnPoint = 3;
                burnPointStart = 2;
                break;
            case 2:
                burnPoint = 4;
                burnPointStart = 2;
                break;
            case 3:
                burnPoint = 5;
                burnPointStart = 3;
                break;
            default:
                break;
        }
        if (IsStartCard())
        {
            BattleManager.Instance.player.fire += burnPointStart;
        }
        BattleManager.Instance.player.fire += burnPoint;
    }
}
