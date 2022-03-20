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

    public bool descriptionType;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(E_EventType.DRAW_CARD, DrawCard);
        EventCenter.AddListener<int>(E_EventType.DELETE_CARD, DeleteCard);
        EventCenter.AddListener<int, int>(E_EventType.SWITCH_CARD, SwitchCard);
        EventCenter.AddListener<MyCard>(E_EventType.CARD_USED, CardUsed);
        for (int i = 0; i < 10; i++)
        {
            GameObject _card_pos = Instantiate(cardPosPfb, GameObject.Find("Canvas").transform);
            _card_pos.transform.position = new Vector3((-i + 4.5f) * 170f + 960, -400 + 540, 0f);
            cardPoses.Add(_card_pos);
            _card_pos.GetComponentInChildren<Text>().text = i.ToString();
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
    public void SetCardPosition()
    {
        for (int i = 0; i < myCardInFlow.Count; i++)
        {
            myCardInFlow[i].transform.position = cardPoses[i].transform.position;
            myCardInFlow[i].GetComponent<MyCard>().position = i;
        }
        //MoveForward();
        //List<GameObject> _flow = new List<GameObject>();
        //foreach (var item in myCardInFlow)
        //{
        //    _flow.Add(item);
        //}
        //MoveForward();
        //myCardInFlow = new List<GameObject>();
        //for (int i = 0; i < _flow.Count; i++)
        //{
        //    for (int j = 0; j < _flow.Count; j++)
        //    {
        //        if (_flow[j].GetComponent<MyCard>().position == i)
        //        {
        //            myCardInFlow.Add(_flow[j]);
        //            continue;
        //        }
        //    }
        //    myCardInFlow[i].transform.position = cardPoses[i].transform.position;
        //}
    }
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
    public void DrawCard()
    {
        if (myCardInFlow.Count >= cardPoses.Count)
        {
            TipManager.ShowTip("牌抽满了！！！");
            return;
        }
        int _index = Random.Range(0, myCardPfbs.Count);
        GameObject _card = Instantiate(myCardPfbs[_index], GameObject.Find("Canvas").transform);
        myCardInFlow.Add(_card);
        SetCardPosition();
    }

    public void AddCard(GameObject card)
    {
        if (myCardInFlow.Count >= cardPoses.Count)
        {
            TipManager.ShowTip("牌抽满了！！！");
            return;
        }
        GameObject _card = Instantiate(card, GameObject.Find("Canvas").transform);
        myCardInFlow.Add(_card);
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
        myCardInFlow.RemoveAt(index);
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
        myCardInFlow.Remove(myCard.gameObject);
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
                Debug.Log("打出了 " + i);
                return;
            }
        }
    }

}
