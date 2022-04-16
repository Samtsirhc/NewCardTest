using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    virtual public string nextAct { get; set; }
    
    protected override void Start()
    {
        base.Start();
        EventCenter.AddListener(E_EventType.ENEMY_TURN, OnEnemyTurn);
    }

    protected override void OnDestroy()
    {

        EventCenter.RemoveListener(E_EventType.ENEMY_TURN, OnEnemyTurn);
        base.OnDestroy();
    }

    protected override void UpdateUnitStatus() {
        base.UpdateUnitStatus();
        ShouUnitStatus.GetComponent<Text>().text += "ÏÂ»ØºÏ£º " + nextAct + "\n";
    }
    protected virtual void OnEnemyTurn()
    {
        armor = 0;
        EnemyAct();
        EventCenter.Broadcast(E_EventType.END_TURN);
    }

    protected virtual void EnemyAct()
    {
        if(hp<=0){
            GameObject.Find("Canvas").transform.Find("NextLevel").gameObject.SetActive(true);
        }
    }
}
