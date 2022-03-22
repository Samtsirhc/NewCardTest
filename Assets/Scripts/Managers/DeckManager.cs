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
    private int maxFlowLenth = 10;

    public bool descriptionType;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(E_EventType.DRAW_CARD, DrawCard);
        EventCenter.AddListener<int>(E_EventType.DELETE_CARD, DeleteCard);
        EventCenter.AddListener<int, int>(E_EventType.SWITCH_CARD, SwitchCard);
        EventCenter.AddListener<MyCard>(E_EventType.CARD_USED, CardUsed);
        myCardInFlow = new List<GameObject>();
        for (int i = 0; i < maxFlowLenth; i++)
        {
            GameObject _card_pos = Instantiate(cardPosPfb, GameObject.Find("Canvas").transform);
            _card_pos.transform.position = new Vector3((-i + 4.5f) * 170f + 960, -400 + 540, 0f);
            cardPoses.Add(_card_pos);
            _card_pos.GetComponentInChildren<Text>().text = i.ToString();
            myCardInFlow.Add(null);
        }
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.DRAW_CARD, DrawCard);
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
        foreach (var item in myCardInFlow)
        {
            if (item == null)
            {
                continue;
            }
            item.transform.position = cardPoses[item.GetComponent<MyCard>().position].transform.position;
        }
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
    }
    public void MoveForward()
    {
        for (int i = 0; i < myCardInFlow.Count; i++)
        {
            if (myCardInFlow[i].GetComponent<MyCard>().freezed >= 1)
            {
                for (int j = i + 1; j < myCardInFlow.Count; j++)
                {
                    if (myCardInFlow[j].GetComponent<MyCard>().position == 0)
                    {
                        continue;
                    }
                    if (myCardInFlow[j].GetComponent<MyCard>().freezed <= 0)
                    {
                        myCardInFlow[j].GetComponent<MyCard>().position -= 1;
                        continue;
                    }
                }
            }
            else
            {
                myCardInFlow[i].GetComponent<MyCard>().position -= 1;
            }
        }
    }

    public int curIndex = 0;
    public List<int> indexes;
    public void DrawCard()
    {
        if (IsFlowFull())
        {
            return;
        }
        if (curIndex == 0)
        {
            indexes = new List<int>();
            for (int i = 0; i < myCardPfbs.Count; i++)
            {
                indexes.Add(i);
            }
            for (int i = 0; i < myCardPfbs.Count; i++)
            {
                int _tmp = indexes[i];
                int _tmp2 = Random.Range(0, myCardPfbs.Count);
                indexes[i] = indexes[_tmp2];
                indexes[_tmp2] = _tmp;
            }
        }
        GameObject _card = Instantiate(myCardPfbs[indexes[curIndex]], GameObject.Find("Canvas").transform);
        AddCard(_card);
        curIndex += 1;
        if (curIndex >= myCardPfbs.Count)
        {
            curIndex = 0;
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
                break;
            }
        }
        SetCardPosition();
    }
    public void DeleteCard(int index)
    {
        if (myCardInFlow.Count <= 0)
        {
            TipManager.ShowTip("你没有！！！");
            return;
        }
        Destroy(myCardInFlow[index]);
        myCardInFlow[index] = null;
        SetCardPosition();
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
        Destroy(myCard.gameObject);
        SetCardPosition();
    }
    public void PlayFirstCard()
    {
        for (int i = 0; i < 10; i++)
        {
            if (myCardInFlow[i].GetComponent<MyCard>().freezed <= 0)
            {
                myCardInFlow[i].GetComponent<MyCard>().PlayCard();
                //Debug.Log("打出了 " + i);
                return;
            }
        }
    }

}
