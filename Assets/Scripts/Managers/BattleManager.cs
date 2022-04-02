using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public GameObject playerObj;
    public GameObject enemyObj;
    [HideInInspector]
    public Unit player;
    [HideInInspector]
    public Unit enemy;
    public CardType comboColor;
    public int comboCount = 0;
    public int playCardTime;
    [HideInInspector]
    public int maxPlayerCardTime { get { return DeckManager.Instance.playTimes; } }
    // Start is called before the first frame update

    override protected void Awake()
    {
        base.Awake();
        EventCenter.AddListener<MyCard>(E_EventType.CARD_USED, AfterCardUsed);
        EventCenter.AddListener(E_EventType.PLAY_ONE_CARD_IN_TURN, PlayCardInTurn);
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
        playCardTime = maxPlayerCardTime;
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<MyCard>(E_EventType.CARD_USED, AfterCardUsed);
        EventCenter.RemoveListener(E_EventType.PLAY_ONE_CARD_IN_TURN, PlayCardInTurn);
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    void Start()
    {
        player = playerObj.GetComponent<Unit>();
        enemy = enemyObj.GetComponent<Unit>();
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

    public void OnTurnEnd()
    {
        playCardTime = maxPlayerCardTime;
        SetComboCount(CardType.BASIC);
    }
}
