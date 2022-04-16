using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : Enemy
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
                nextAct = "»¤¼×8";
                break;
            case 2:
                GetArmor(8);
                nextAct = "¹¥»÷12";
                break;
            case 3:
                AttackPlayer(12);
                nextAct = "¹¥»÷16";
                break;
            case 4:
                AttackPlayer(16);
                nextAct = "»¤¼×12";
                break;
            case 5:
                GetArmor(12);
                nextAct = "¹¥»÷14";
                break;
        }
        actTurn = actTurn % 5 + 1;
    }

    
}
