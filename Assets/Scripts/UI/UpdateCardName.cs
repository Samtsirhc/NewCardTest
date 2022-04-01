using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCardName : MonoBehaviour
{
    public GameObject myCard;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = myCard.GetComponent<MyCard>().cardName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
    }
}
