using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public GameObject playerObj { get { return GameObject.Find("Player"); } }
    public GameObject enemyObj { get { return GameObject.Find("Enemy"); } }
    [HideInInspector]
    public Unit player { get { return playerObj.GetComponent<Unit>(); } }
    [HideInInspector]
    public Unit enemy { get { return enemyObj.GetComponent<Unit>(); } }
    public CardType comboColor;
    public int comboCount = 0;
    public int playCardTime;
    public int playCost;
    
    [HideInInspector]
    public int maxPlayerCardTime { get { return DeckManager.Instance.playTimes; } }
    public int maxPlayerCost{ get { return DeckManager.Instance.PlayCost; } }
    // Start is called before the first frame update

    override protected void Awake()
    {
        base.Awake();
        EventCenter.AddListener<MyCard>(E_EventType.CARD_USED, AfterCardUsed);
        EventCenter.AddListener(E_EventType.PLAY_ONE_CARD_IN_TURN, PlayCardInTurn);
        EventCenter.AddListener(E_EventType.PLAY_ALL_CARD, PlayAllCard);
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<MyCard>(E_EventType.CARD_USED, AfterCardUsed);
        EventCenter.RemoveListener(E_EventType.PLAY_ONE_CARD_IN_TURN, PlayCardInTurn);
        EventCenter.RemoveListener(E_EventType.PLAY_ALL_CARD, PlayAllCard);
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playCardTime = maxPlayerCardTime;
        playCost = maxPlayerCost;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AfterCardUsed(MyCard card)
    {
        SetComboCount(card.cardType);
    }

    public void PlayCardInTurn()
    {
        if (playCardTime <= 0)
        {
            TipManager.ShowTip("没有打牌次数了！！");
            return;
        }
        else
        {
            playCardTime -= 1;
            DeckManager.Instance.PlayFirstCard();
        }
    }

    public void PlayAllCard(){
        if (playCardTime <= 0)
        {
            TipManager.ShowTip("没有打牌次数了！！");
            return;
        }
        else
        {
            DeckManager.Instance.PlayAllCard();
        }
    }
    void SetComboCount(CardType card_type)
    {
        if (comboColor == card_type)
        {
            comboCount += 1;
        }
        else
        {
            comboColor = card_type;
            comboCount = 1;
        }
    }

    public bool isCostEnough(int cost)
    {
        return playCost >= cost;
    }

    public void Cost(int cost){
        playCost -= cost;
    }

    public void OnTurnStart()
    {
        // playCardTime = maxPlayerCardTime;
        // playCost = maxPlayerCost;
        // SetComboCount(CardType.BASIC);
        //EventCenter.Broadcast(E_EventType.DRAW_ALL_CARD);
    }
    public void OnTurnEnd()
    {
        playCardTime = maxPlayerCardTime;
        playCost = maxPlayerCost;
        SetComboCount(CardType.BASIC);
    }
}
