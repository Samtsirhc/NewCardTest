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
    public int position = -1;    // 在牌流里面的位置 0开始，最右侧
    [HideInInspector]
    public Dictionary<string, int> playInfo;    // 记录一些伤害之类的东西
    [HideInInspector]
    public string description;
    public string cardName = "示例卡牌";
    public string originalDescription = "示例卡牌的描述";
    public string tureDescription = "哈哈哈哈哈哈哈哈";
    public bool inBattle = false;

    bool playing = false;
    bool played = false;
    float moveSpeed = 20;
    #endregion

    #region 卡牌属性
    public int damage;
    public int armor;

    // 火焰
    public int fire;
    public bool burn
    {
        get { return _burn; }
        set
        {
            _burn = value;
            if (value)
            {
                OnCardBurn();
            }
        }
    }
    private bool _burn = false;
    public int burnFactor = 2;

    // 寒冰
    public int ice;
    public bool icebound = false;
    public int iceboundFactor = 1;

    #endregion

    #region 其他
    private EventTrigger eventTrigger;
    #endregion

    #region Unity函数
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        InitTriggers();
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
        playInfo = new Dictionary<string, int>();
        playInfo.Add("伤害", 0);
        playInfo.Add("护甲", 0);
    }
    protected virtual void Start()
    {
        SetLevelData();
    }
    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    private void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
        UpdateDes();
        if (inBattle)
        {
            if (playing)
            {
                MoveAndEnlarge();
            }
            else
            {
                SetCardPosition();
            }
        }
    }
    public void SetCardPosition()
    {
        if (position < 0)
        {
            return;
        }
        Vector3 des = DeckManager.Instance.cardPoses[position].transform.position;
        Vector3 direction = des - transform.position;
        if (direction.magnitude <= 3f)
        {
            transform.position = des;
            return;
        }
        float _speed = 5 + moveSpeed * direction.magnitude / 500;
        transform.Translate(direction.normalized * _speed);
    }
    private void MoveAndEnlarge()
    {
        Vector3 des = new Vector3(1920 / 2, 1080 / 2, 0);
        Vector3 direction = des - transform.position;
        if (direction.magnitude <= 3f)
        {
            if (!played)
            {
                GetComponent<Animator>().Play("fade");
                PreUse();
                OnUse();
                AfterUse();
                EventCenter.Broadcast<MyCard>(E_EventType.CARD_USED, this);
                played = true;
            }
            transform.position = des;
            return;
        }
        float _speed = 5 + moveSpeed * direction.magnitude / 500;
        transform.Translate(direction.normalized * _speed);
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
    protected virtual void PointerClick(BaseEventData arg0)
    {
        // if (Input.GetKey(KeyCode.A))
        // {
        //     PlayCard();
        // }
        if (Input.GetKey(KeyCode.W))
        {
            CloneCard();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            DeleteSelf();
        }
        // else if (Input.GetKey(KeyCode.E))
        // {
        //     if (cardLevel >= maxLevel)
        //     {
        //         cardLevel = 1;
        //     }
        //     else
        //     {
        //         cardLevel += 1;
        //     }
        // }
        else if (Input.GetKey(KeyCode.R))
        {
            if (!burn && GetFire() >= 10)
            {
                if (icebound)
                {
                    icebound = false;
                    AddIce(10);
                }
                BurnCard();
                return;
            }
            else if (burn)
            {
                burn = false;
                AddFire(10);
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (!icebound && GetIce() >= 10)
            {
                if (burn)
                {
                    burn = false;
                    AddFire(10);
                }
                FreezeCard();
                return;
            }
            else if (icebound)
            {
                icebound = false;
                AddIce(10);
            }
        }
        // else if (Input.GetKey(KeyCode.R))
        // {
        //     if (!burn && !icebound)
        //     {
        //         BurnCard();
        //         return;
        //     }
        //     else if (burn && !icebound)
        //     {
        //         IceboundCard();
        //         AddFire(10);
        //         return;
        //     }
        //     else if (burn || icebound)
        //     {
        //         burn = false;
        //         icebound = false;
        //         string _s = string.Format("【{0}】恢复正常", cardName);
        //         TipManager.ShowTip(_s);
        //         AddIce(10);
        //         return;
        //     }
        // }
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
    #endregion

    #region 战斗
    public virtual int CastDamage(int num)
    {
        if (burn)
        {
            num *= burnFactor;
        }
        int _damage = BattleManager.Instance.enemy.TakeDamage(num);
        if (_damage > 0)
        {
            OnCauseDamage();
            if (icebound)
            {
                GetArmor(_damage * iceboundFactor);
            }
        }
        playInfo["伤害"] += _damage;
        return _damage;
    }

    public virtual int CastPiercingDamage(int num)
    {
        if (burn)
        {
            num *= burnFactor;
        }
        int _damage = BattleManager.Instance.enemy.TakePiercingDamage(num);
        if (_damage > 0)
        {
            OnCauseDamage();
            if (icebound)
            {
                GetArmor(_damage * iceboundFactor);
            }
        }
        playInfo["伤害"] += _damage;
        return _damage;
    }
    public virtual int DamageSelf(int num)
    {
        return BattleManager.Instance.player.TakeDamage(num);
    }

    /// <summary>
    /// add armor
    /// </summary>
    /// <param name="armor"></param>
    public virtual void GetArmor(int armor)
    {
        BattleManager.Instance.player.armor += armor;
        playInfo["护甲"] += armor;
    }
    public virtual void OnCauseDamage()
    {
    }
    #endregion

    #region 战斗相关的状态事件
    public void BurnButton()
    {
        if (!burn && GetFire() >= 10)
        {
            if (icebound)
            {
                icebound = false;
                AddIce(10);
            }
            BurnCard();
            return;
        }
        else if (burn)
        {
            burn = false;
            AddFire(10);
        }
    }
    public void IceButton()
    {
        if (!icebound && GetIce() >= 10)
        {
            if (burn)
            {
                burn = false;
                AddFire(10);
            }
            FreezeCard();
            return;
        }
        else if (icebound)
        {
            icebound = false;
            AddIce(10);
        }
    }
    protected virtual void BurnCard()
    {
        SoundManager.Instance.BurnCard();
        burn = true;
        icebound = false;
        string _s = string.Format("【{0}】燃烧了", cardName);
        TipManager.ShowTip(_s);
        AddFire(-10);
    }
    protected virtual void OnCardBurn()
    {

    }
    protected virtual void FreezeCard()
    {
        SoundManager.Instance.FreezeCard();
        burn = false;
        icebound = true;
        string _s = string.Format("【{0}】冰封了", cardName);
        TipManager.ShowTip(_s);
        AddIce(-10);
    }
    #endregion

    #region 流程相关
    public virtual void SetLevelData()
    {
    }
    public virtual void PlayCard()
    {
        playing = true;
    }   // 打出卡牌

    public virtual void CloneCard()
    {
        if (!BattleManager.Instance.isCostEnough(2))
        {
            return;
        }
        DeckManager.Instance.AddCard(Instantiate(gameObject, GameObject.Find("Canvas").transform));
        BattleManager.Instance.Cost(2);
    }
    public virtual void TriggerCard()
    {
        PreUse();
        OnUse();
        AfterUse();
    }   // 触发卡牌，而不是打出卡牌
    public virtual void PreUse()
    {

    }
    public virtual void OnUse()
    {

    }
    public virtual void AfterUse()
    {
        OnLose();
        ShowCardPlayInfo();
    }
    public virtual void OnGet()
    {

    }
    public virtual void DeleteSelf()
    {
        OnDelete();
        EventCenter.Broadcast(E_EventType.DELETE_CARD, position);
    }
    public virtual void OnDelete()
    {
        OnLose();
    }
    public virtual void OnLose()    // 被删除、打出都会触发
    {

    }
    public virtual void ShowCardPlayInfo()
    {
        string _tmp = string.Format("【{0}】-【{1}】伤害，【{2}】护甲", cardName, playInfo["伤害"], playInfo["护甲"]);
        TipManager.ShowTip(_tmp);
    }
    public virtual void OnTurnEnd()
    {
    }
    public virtual void OnTurnStart()
    {

    }
    #endregion

    #region 获取数据
    public int GetFire() { return BattleManager.Instance.player.fire; }
    public void SetFire(int num) { BattleManager.Instance.player.fire = num; }
    public void AddFire(int num) { BattleManager.Instance.player.fire += num; }
    public int GetIce() { return BattleManager.Instance.player.ice; }
    public void SetIce(int num) { BattleManager.Instance.player.ice = num; }
    public void AddIce(int num) { BattleManager.Instance.player.ice += num; }
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
    public MyCard GetLeftCard()
    {
        if (IsIndexLegal(position + 1))
        {
            return DeckManager.Instance.myCardInFlow[position + 1].GetComponent<MyCard>();
        }
        else
        {
            return null;
        }
    }
    public MyCard GetRightCard()
    {
        if (IsIndexLegal(position - 1))
        {
            return DeckManager.Instance.myCardInFlow[position - 1].GetComponent<MyCard>();
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
    public bool IsNextCardCombo()   // 自己后面的牌是不是一个颜色的
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
