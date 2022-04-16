using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3 : Enemy
{
    private int actTurn = 1;
    public int Hp = 90;
    public Unit player;
    protected override void Start(){
        base.Start();
        hp = Hp;
        nextAct = "攻击20";
    }
    protected override void EnemyAct()
    {
        base.EnemyAct();
        switch(actTurn){
            case 1:
                player.TakeDamage(24);
                nextAct = "偷取你的所有的护盾";
                break;
            case 2:
                armor = player.armor;
                player.armor = 0;
                nextAct = "攻击14";
                break;
            case 3:
                player.TakeDamage(14);
                nextAct = "攻击25 石化1";
                break;
            case 4:
                player.TakeDamage(25);
                //石化
                nextAct = "防御10";
                break;
            case 5:
                armor += 10;
                nextAct = "偷取你的所有的护盾";
                break;
            case 6:
                armor = player.armor;
                player.armor = 0;
                nextAct = "攻击20";
                break;
        }
        actTurn = actTurn % 6 + 1;
    }
}
