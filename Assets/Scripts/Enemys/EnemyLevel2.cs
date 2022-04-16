using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : Enemy
{

    private int actTurn = 1;
    public int Hp = 70;
    public Unit player;
    protected override void Start(){
        base.Start();
        hp = Hp;
        nextAct = "¹¥»÷14";
    }
    protected override void EnemyAct()
    {
        base.EnemyAct();
        switch(actTurn){
            case 1:
                player.TakeDamage(14);
                nextAct = "¹¥»÷12 Ê¯»¯1";
                break;
            case 2:
                player.TakeDamage(12);
                //Ê¯»¯
                nextAct = "·ÀÓù8";
                break;
            case 3:
                armor += 8;
                nextAct = "¹¥»÷18 ";
                break;
            case 4:
                player.TakeDamage(18);
                nextAct = "¹¥»÷20 Ê¯»¯1";
                break;
            case 5:
                player.TakeDamage(20);
                //Ê¯»¯
                nextAct = "¹¥»÷14";
                break;
        }
        actTurn = actTurn % 5 + 1;
    }
}
