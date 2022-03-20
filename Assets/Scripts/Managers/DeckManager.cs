using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    public List<GameObject> myCardPfbs;
    public List<GameObject> cardPoses;
    public List<GameObject> myCardInFlow;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(E_EventType.DRAW_CARD, DrawCard);
        EventCenter.AddListener<int>(E_EventType.DELETE_CARD, DeleteCard);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.DRAW_CARD, DrawCard);
        EventCenter.RemoveListener<int>(E_EventType.DELETE_CARD, DeleteCard);
    }
    // Update is called once per frame
    void Update()
    {
        SetCardPosition();
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


}
