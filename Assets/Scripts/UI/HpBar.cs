using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject quickBar;
    public float curRate;
    public Text text;
    public GameObject unitObj;
    private Unit unit;
    // Start is called before the first frame update
    void Start()
    {
        unit = unitObj.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        SetValue(unit.hp, unit.maxHp);
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
        text.text = cur + "/" + max;
    }

}  
