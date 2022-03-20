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
    }

    public void DrawCard()
    {
        if (myCardInFlow.Count >= cardPoses.Count)
        {
            TipManager.ShowTip("ÅÆ³éÂúÁË£¡£¡£¡");
            return;
        }
        int _index = Random.Range(0, myCardPfbs.Count);
        GameObject _card = Instantiate(myCardPfbs[_index], GameObject.Find("Canvas").transform);
        myCardInFlow.Add(_card);
        SetCardPosition();
    }

    public void DeleteCard(int index)
    {
        if (myCardInFlow.Count <= 0)
        {
            TipManager.ShowTip("ÄãÃ»ÓÐ£¡£¡£¡");
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

}
