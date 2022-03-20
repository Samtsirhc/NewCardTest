using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_ZhiLen : BlueCard
{
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        for (int i = DeckManager.Instance.myCardInFlow.Count - 1; i >= 0; i--)
        {
            if (DeckManager.Instance.myCardInFlow[i] = gameObject)
            {
                continue;
            }
            if (DeckManager.Instance.myCardInFlow[i].GetComponent<MyCard>().cardType == CardType.BLUE)
            {
                DeckManager.Instance.myCardInFlow[i].GetComponent<MyCard>().freezed += 1;
            }
        }
    }
}
