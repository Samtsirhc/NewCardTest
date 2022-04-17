using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    public GameObject ShowUnitStatus;
    virtual public string nextAct { get; set; }
    public Unit player { get { return BattleManager.Instance.player; } }

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
        ShowUnitStatus.GetComponent<Text>().text = "" + nextAct + ""; //TODO
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
    public void AttackPlayer(int num)
    {
        player.TakeDamage(num);
        PlayAttack();
    }
    public void GetArmor(int num)
    {
        PlayPower();
        armor += num;
    }
    public void StealArmor(int num)
    {
        if (num <= player.armor)
        {
            player.armor -= num;
            GetArmor(num);
        }
        else
        {
            GetArmor(player.armor);
            player.armor = 0;
        }
    }
    public void StealAllArmor()
    {
        StealArmor(player.armor);
    }
    
    public void StoneCard(int index)
    {
        if(index >= DeckManager.Instance.maxFlowLenth || index < 0){
            return;
        }
        DeckManager.Instance.myCardInFlow[index].GetComponent<MyCard>().StoneCard();
    }
}
