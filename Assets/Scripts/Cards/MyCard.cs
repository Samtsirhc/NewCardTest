using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MyCard : MonoBehaviour
{
    #region 公开属性
    [HideInInspector]
    public virtual CardType cardType { get { return CardType.BASIC; } }
    [HideInInspector]
    public int position;
    public int freezed
    {
        get { return _freezed; }
        set
        {
            _freezed = value;
            if (_freezed >= 1)
            {
                OnFreezed();
                if (coldAlarm)
                {
                    PlayCard();
                }
            }
        } 
    }
    [HideInInspector]
    public bool coldAlarm = false;
    [HideInInspector]
    public Dictionary<string, int> playInfo;

    [HideInInspector]
    public string description;
    public string cardName = "示例卡牌";
    public string originalDescription = "示例卡牌的描述";
    public string tureDescription = "哈哈哈哈哈哈哈哈";

    #endregion
    private int _freezed = 0;
    private EventTrigger eventTrigger;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        InitTriggers();
        SwitchDescriptionType();
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
        playInfo = new Dictionary<string, int>();
        playInfo.Add("伤害", 0);
        playInfo.Add("护甲", 0);
    }
    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    private void FixedUpdate()
    {
        SwitchDescriptionType();
    }

    #region UI事件
    private void InitTriggers()
    {
        eventTrigger = GetComponent<EventTrigger>();
        AddPointerEvent(eventTrigger, EventTriggerType.PointerEnter, PointerEnter);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerExit, PointerExit);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerDown, PointerDown);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerUp, PointerUp);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerClick, PointerClick);

    }

    private void PointerClick(BaseEventData arg0)
    {
        if (Input.GetKey(KeyCode.A))
        {
            PlayCard();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            EventCenter.Broadcast(E_EventType.DELETE_CARD, position);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            freezed += 1;
        }
    }

    private void PointerUp(BaseEventData arg0)
    {
        MyCard _other;
        if (UIManager.Instance.ObjBePointed.TryGetComponent(out _other))
        {
            DeckManager.Instance.SwitchCard(_other.position, position);
        }
    }

    private void PointerDown(BaseEventData arg0)
    {
        if (Input.GetKey(KeyCode.Q))
        {
            EventCenter.Broadcast(E_EventType.SHOW_ARROW);
        }
    }

    private void PointerExit(BaseEventData arg0)
    {
        UIManager.Instance.ObjBePointed = null;
    }

    private void PointerEnter(BaseEventData arg0)
    {
        UIManager.Instance.ObjBePointed = gameObject;
    }

    private void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }
    public void SwitchDescriptionType()
    {
        if (DeckManager.Instance.descriptionType)
        {
            description = originalDescription;
        }
        else
        {
            description = tureDescription;
        }
    }
    #endregion

    #region 战斗
    public virtual int CastDamage(int damage)
    {
        int _damage = BattleManager.Instance.enemy.TakeDamage(damage);
        if (_damage > 0)
        {
            OnCauseDamage();
        }
        return _damage;
    } 
    public virtual void GetArmor(int armor)
    {
        BattleManager.Instance.player.armor += armor;
    }
    public virtual void OnCauseDamage()
    {

    }
    public virtual void AddCold(int cold)
    {
        BattleManager.Instance.enemy.cold += cold;
    }
    #endregion

    #region 流程相关

    public virtual void OnFreezed()
    {

    }

    public virtual void PlayCard()
    {
        if (freezed >= 1 && !coldAlarm)
        {
            TipManager.ShowTip("这张牌被冻结了！");
            return;
        }
        PreUse();
        OnUse();
        AfterUse();
    }

    public virtual void PreUse()
    {

    }
    public virtual void OnUse()
    {

    }
    public virtual void AfterUse()
    {
        ShowCardPlayInfo();
        EventCenter.Broadcast<MyCard>(E_EventType.CARD_USED, this);
    }

    public virtual void ShowCardPlayInfo()
    {
        string _tmp = string.Format("【{0}】-【{1}】伤害，【{2}】护甲", cardName, playInfo["伤害"], playInfo["护甲"]);
        TipManager.ShowTip(_tmp);
    }

    public virtual void OnTurnEnd()
    {
        if (freezed >= 1)
        {
            freezed -= 1;
        }
    }

    public virtual void OnTurnStart()
    {

    }

    #endregion

    #region 获取数据
    public int GetAnger()
    {
        return BattleManager.Instance.player.anger;
    }
    public void SetAnger(int var)
    {
        BattleManager.Instance.player.anger = var;
    }
    public void AddAnger(int var)
    {
        BattleManager.Instance.player.anger += var;
    }
    public int GetCalm()
    {
        return BattleManager.Instance.player.calm;
    }
    public void SetCalm(int var)
    {
        BattleManager.Instance.player.calm = var;
    }
    public void AddCalm(int var)
    {
        BattleManager.Instance.player.calm += var;
    }
    public bool IsStartCard()
    {
        return BattleManager.Instance.maxPlayerCardTime - 1 == BattleManager.Instance.playCardTime;
    }
    public bool IsEndCard()
    {
        return 0 == BattleManager.Instance.playCardTime;
    }
    public int GetComboCount()
    {
        if (BattleManager.Instance.comboColor == cardType)
        {
            return BattleManager.Instance.comboCount;
        }
        else
        {
            return 0;
        }
    }
    #endregion


}
