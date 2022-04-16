using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject quickBar;
    public float curRate;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        quickBar.GetComponent<Slider>().value = curRate;
        GetComponent<Slider>().value = Mathf.Lerp(GetComponent<Slider>().value, curRate, Time.deltaTime * 2);
    }

    public void SetValue(float cur, float max)
    {
        curRate = cur / max;
    }

}
