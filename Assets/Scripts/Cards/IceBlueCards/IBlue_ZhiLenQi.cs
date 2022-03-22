using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_ZhiLenQi : IceBlueCard
{
    private int iceAdder;

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        switch (cardLevel)
        {
            case 1:
                iceAdder = 1;
                break;
            case 2:
                iceAdder = 2;
                break;
            case 3:
                iceAdder = 4;
                break;
            default:
                break;
        }
        if (position != 0 && position != 9)
        {
            if (DeckManager.Instance.myCardInFlow[position - 1] != null)
            {
                DeckManager.Instance.myCardInFlow[position - 1].GetComponent<MyCard>().additionalArmor += iceAdder;
            }
            if (DeckManager.Instance.myCardInFlow[position + 1] != null)
            {
                DeckManager.Instance.myCardInFlow[position + 1].GetComponent<MyCard>().additionalArmor += iceAdder;
            }
        }
        if (position == 0)
        {
            if (DeckManager.Instance.myCardInFlow[position + 1] != null)
            {
                DeckManager.Instance.myCardInFlow[position + 1].GetComponent<MyCard>().additionalArmor += iceAdder;
            }
        }
        if (position == 9)
        {
            if (DeckManager.Instance.myCardInFlow[position - 1] != null)
            {
                DeckManager.Instance.myCardInFlow[position - 1].GetComponent<MyCard>().additionalArmor += iceAdder;
            }
        }
    }

}
