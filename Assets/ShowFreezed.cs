using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFreezed : MonoBehaviour
{
    public GameObject host;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Text>().text = host.GetComponent<MyCard>().freezed.ToString();
    }
}
