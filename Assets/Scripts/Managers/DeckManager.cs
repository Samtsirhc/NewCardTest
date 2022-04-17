using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : Singleton<DeckManager>
{
    public GameObject cardPosPfb;
    public List<GameObject> myCardPfbs;
    public List<GameObject> cardPoses;
    public List<GameObject> myCardInFlow;
    private List<GameObject> cardBuffer;
    public int maxFlowLenth = 5;
    public int playTimes = 5;
    public int PlayCost = 3;
    public int switchIndex = 1;
    public bool descriptionType;
    public GameObject cardLayout;
    // Start is called before the first frame update
    void Start()
    {
        cardBuffer = new List<GameObject>();
        EventCenter.AddListener(E_EventType.DRAW_CARD, DrawCard);
        EventCenter.AddListener(E_EventType.DRAW_ALL_CARD, DrawAllCard);
        EventCenter.AddListener<int>(E_EventType.DELETE_CARD, DeleteCard);
        EventCenter.AddListener<int, int>(E_EventType.SWITCH_CARD, SwitchCard);
        EventCenter.AddListener<MyCard>(E_EventType.CARD_USED, CardUsed);
        EventCenter.AddListener(E_EventType.SHOW_CARDS, ShowCards);

        InitMyCardPfb();
        myCardInFlow = new List<GameObject>();
        for (int i = 0; i < maxFlowLenth; i++)
        {
            float _x = 210f;
            float _p = ((((float)maxFlowLenth - 1f) / 2f) - (float)i) * _x;
            GameObject _card_pos = Instantiate(cardPosPfb, GameObject.Find("Canvas").transform);
            _card_pos.transform.position = new Vector3(_p + 960, -400 + 540, 0f);
            cardPoses.Add(_card_pos);
            _card_pos.GetComponentInChildren<Text>().text = i.ToString();
            myCardInFlow.Add(null);
        }
        switchIndex = LevelManager.Instance.switchIndex;
        Invoke("DrawAllCard",0.2f);
    }

    private void ShowCards()
    {
        GameObject _layout_obj = Instantiate(cardLayout, GameObject.Find("Canvas").transform);
        foreach (var item in myCardPfbs)
        {
            Instantiate(item, _layout_obj.GetComponent<CardLayOut>().obj.transform);
        }
    }

    private void InitMyCardPfb()
    {
        myCardPfbs = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            myCardPfbs.Add(transform.GetChild(i).gameObject);
        }
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.DRAW_CARD, DrawCard);
        EventCenter.RemoveListener(E_EventType.DRAW_ALL_CARD, DrawAllCard);
        EventCenter.RemoveListener<int>(E_EventType.DELETE_CARD, DeleteCard);
        EventCenter.RemoveListener<MyCard>(E_EventType.CARD_USED, CardUsed);
        EventCenter.RemoveListener<int, int>(E_EventType.SWITCH_CARD, SwitchCard);
    }
    // Update is called once per frame
    void Update()
    {
        SwitchDescriptionType();
    }

    #region 设置卡牌位置
    public void SetCardPosition()
    {
        AlignRight();
        //foreach (var item in myCardInFlow)
        //{
        //    if (item == null)
        //    {
        //        continue;
        //    }
        //    item.transform.position = cardPoses[item.GetComponent<MyCard>().position].transform.position;
        //}
    }

    public void AlignRight()
    {
        myCardInFlow.Remove(null);
        for (int i = myCardInFlow.Count; i < maxFlowLenth; i++)
        {
            myCardInFlow.Add(null);

        }
        for (int i = 0; i < maxFlowLenth; i++)
        {
            if (myCardInFlow[i] == null)
            {
                continue;
            }
            myCardInFlow[i].GetComponent<MyCard>().position = i;
        }
    }
    #endregion


    public void SwitchCard(int _index1, int _index2)
    {
        if (!BattleManager.Instance.isCostEnough(1))
        {
            return;
        }

        if (_index1 == _index2)
        {
            return;
        }
        else if (Mathf.Abs(_index1 - _index2) > switchIndex)
        {
            Debug.Log("交换失败，仅能交换相邻的卡牌。");
            return;
        }

        if (myCardInFlow[_index1] == null || myCardInFlow[_index2] == null)
        {
            return;
        }
        GameObject _tmp = myCardInFlow[_index1];
        myCardInFlow[_index1] = myCardInFlow[_index2];
        myCardInFlow[_index2] = _tmp;
        myCardInFlow[_index1].GetComponent<MyCard>().position = _index2;
        myCardInFlow[_index2].GetComponent<MyCard>().position = _index1;
        SetCardPosition();
        BattleManager.Instance.Cost(1);
    }
    public int curIndex = 0;
    public List<int> indexes;
    public void DrawCard()
    {
        if (IsFlowFull())
        {
            return;
        }
        CheckBuffer();
        GameObject _card = cardBuffer[0];
        cardBuffer.RemoveAt(0);
        AddCard(_card);
        curIndex += 1;
        if (curIndex >= myCardPfbs.Count)
        {
            curIndex = 0;
        }
    }

    public void DrawAllCard()
    {
        while (!IsFlowFull())
        {
            DrawCard();
        }
    }
    public bool IsFlowFull()
    {
        foreach (var item in myCardInFlow)
        {
            if (item == null)
            {
                return false;
            }
        }
        TipManager.ShowTip("牌流满了！！！");
        return true;
    }
    public void AddCard(GameObject card)
    {
        if (IsFlowFull())
        {
            TipManager.ShowTip("牌流满了！！！");
            return;
        }
        for (int i = 0; i < maxFlowLenth; i++)
        {
            if (myCardInFlow[i] == null)
            {
                myCardInFlow[i] = card;
                card.GetComponent<MyCard>().position = i;
                string _s = string.Format("增加了卡牌【{0}】在位置【{1}】", card.GetComponent<MyCard>().cardName, i);
                TipManager.ShowTip(_s);
                card.GetComponent<MyCard>().OnGet();
                SoundManager.Instance.DrawCard();
                break;
            }
        }
        SetCardPosition();
    }
    public void CheckBuffer()
    {
        if (cardBuffer.Count > 0)
        {
            return;
        }
        else
        {
            ResetCardBuffer();
        }
    }
    public void ResetCardBuffer()
    {
        cardBuffer = new List<GameObject>();
        for (int i = 0; i < myCardPfbs.Count; i++)
        {
            if (i == 0)
            {
                cardBuffer.Add(CreateNewCardClone(myCardPfbs[i]));
            }
            else
            {
                cardBuffer.Insert(Random.Range(0, i), CreateNewCardClone(myCardPfbs[i]));
            }
        }
        foreach (var item in cardBuffer)
        {
            item.GetComponent<MyCard>().position = -1;
            item.GetComponent<MyCard>().inBattle = true;
        }
    }
    public GameObject CreateNewCardClone(GameObject obj)
    {
        GameObject _obj = Instantiate(obj, GameObject.Find("Canvas").transform);
        _obj.transform.position = transform.position;
        return _obj;
    }
    public void DeleteCard(int index)
    {
        if (!BattleManager.Instance.isCostEnough(2))
        {
            return;
        }
        if (myCardInFlow.Count <= 0)
        {
            TipManager.ShowTip("你没有！！！");
            return;
        }
        SoundManager.Instance.DeleteCard();
        Destroy(myCardInFlow[index]);
        myCardInFlow[index] = null;
        SetCardPosition();
        DrawCard();
        BattleManager.Instance.Cost(2);
    }
    public void SwitchDescriptionType()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            descriptionType = !descriptionType;
        }
    }
    public void CardUsed(MyCard myCard)
    {
        myCardInFlow[myCard.position] = null;
        StartCoroutine(DelayDestroy(myCard.gameObject));
        SetCardPosition();
        DrawCard();
    }
    IEnumerator DelayDestroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    public void PlayFirstCard()
    {
        myCardInFlow[0].GetComponent<MyCard>().PlayCard();
       
    }
    public void PlayAllCard()
    {
        StartCoroutine(_PlayAllCard());
    }
    IEnumerator _PlayAllCard()
    {
        for (int i = 0; i < BattleManager.Instance.playCardTime; i++)
        {
            PlayFirstCard();
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.playCardTime = 0;
        EventCenter.Broadcast(E_EventType.ENEMY_TURN);
        yield return 0;
    }
}
