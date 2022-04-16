using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : Enemy
{

    private int actTurn = 1;
    protected override void Start(){
        base.Start();
        nextAct = "¹¥»÷14";
    }
    protected override void EnemyAct()
    {
        base.EnemyAct();
        switch(actTurn){
            case 1:
                AttackPlayer(14);
                nextAct = "¹¥»÷12 Ê¯»¯1";
                break;
            case 2:
                AttackPlayer(12);
                //Ê¯»¯
                nextAct = "·ÀÓù8";
                break;
            case 3:
                GetArmor(8);
                nextAct = "¹¥»÷18 ";
                break;
            case 4:
                AttackPlayer(18);
                nextAct = "¹¥»÷20 Ê¯»¯1";
                break;
            case 5:
                AttackPlayer(20);
                //Ê¯»¯
                nextAct = "¹¥»÷14";
                break;
        }
        actTurn = actTurn % 5 + 1;
    }
}
