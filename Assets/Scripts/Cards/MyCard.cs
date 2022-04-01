using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MyCard : MonoBehaviour
{
    #region 基础属性
    public int cardLevel = 1;
    public int maxLevel = 3;
    [HideInInspector]
    public virtual CardType cardType { get { return CardType.BASIC; } }
    [HideInInspector]
    public int position;
    [HideInInspector]
    public Dictionary<string, int> playInfo;
    [HideInInspector]
    public string description;
    public string cardName = "示例卡牌";
    public string originalDescription = "示例卡牌的描述";
    public string tureDescription = "哈哈哈哈哈哈哈哈";
    #endregion

    #region 卡牌属性
    public int damage;
    public int armor;
    public int additionalArmor = 0;
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
    public bool coldAlarm = false;
    public bool burn = false;
    public int burnFactor = 2;
    public int fire;
    public int cold;
    public bool icebound = false;
    #endregion

    private int _freezed = 0;
    private EventTrigger eventTrigger;

    #region Unity函数
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        InitTriggers();
        SwitchDescriptionType();
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
        playInfo = new Dictionary<string, int>();
        playInfo.Add("伤害", 0);
        playInfo.Add("护甲", 0);
    }
    protected virtual void Start()
    {

    }
    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    protected virtual void FixedUpdate()
    {
        SwitchDescriptionType();
        UpdateDes();
    }
    #endregion

    #region UI事件
    protected virtual void UpdateDes()
    {

    }

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
        if (Input.GetKey(KeyCode.W))
        {
            DeckManager.Instance.AddCard(Instantiate(gameObject, GameObject.Find("Canvas").transform));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            EventCenter.Broadcast(E_EventType.DELETE_CARD, position);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            freezed += 1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (cardLevel >= maxLevel)
            {
                cardLevel = 1;
            }
            else
            {
                cardLevel += 1;
            }
        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (!burn && !icebound)
            {
                burn = true;
                string _s = string.Format("【{0}】燃烧了", cardName);
                TipManager.ShowTip(_s);
                return;
            }
            else if (burn && !icebound)
            {
                burn = false;
                icebound = true;
                string _s = string.Format("【{0}】冰封了", cardName);
                TipManager.ShowTip(_s);
                return;
            }
            else if (burn || icebound)
            {
                burn = false;
                icebound = false;
                string _s = string.Format("【{0}】恢复正常", cardName);
                TipManager.ShowTip(_s);
                return;
            }
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
        if (burn)
        {
            damage *= burnFactor;
        }
        int _damage = BattleManager.Instance.enemy.TakeDamage(damage);
        if (_damage > 0)
        {
            OnCauseDamage();
            if (icebound)
            {
                GetArmor(_damage);
            }
        }
        playInfo["伤害"] += _damage;
        return _damage;
    } 
    public virtual void GetArmor(int armor)
    {
        BattleManager.Instance.player.armor += armor;
        playInfo["护甲"] += armor;
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
        EventCenter.Broadcast<MyCard>(E_EventType.CARD_USED, this);
    }
    public virtual void TriggerCard()
    {
        PreUse();
        OnUse();
        AfterUse();
    }

    public virtual void PreUse()
    {

    }
    public virtual void OnUse()
    {
        //GetArmor(additionalArmor);
    }
    public virtual void AfterUse()
    {
        ShowCardPlayInfo();
    }
    public virtual void OnGet()
    {
        
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

    public MyCard GetCardByPos(int index)
    {
        if (IsIndexLegal(index))
        {
            return DeckManager.Instance.myCardInFlow[index].GetComponent<MyCard>();
        }
        else
        {
            return null;
        }
    }

    public bool IsIndexLegal(int index)
    {
        int _flow_length = DeckManager.Instance.myCardInFlow.Count;
        if (index >= 0 && index < _flow_length)
        {
            return true;
        }
        return false;
    }
    public bool IsNextCardCombo()
    {
        MyCard _card;
        if (DeckManager.Instance.myCardInFlow[position].TryGetComponent<MyCard>(out _card))
        {
            if (_card.cardType == cardType)
            {
                return true;
            }
        }
        return false;
    }
    #endregion


}
