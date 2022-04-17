using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLevelTip : MonoBehaviour
{
    public GameObject cardObj;
    public Text text;
    private MyCard myCard;
    // Start is called before the first frame update
    void Start()
    {
        myCard = cardObj.GetComponent<MyCard>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        text.text = "Lv." + myCard.cardLevel.ToString();
    }
}
