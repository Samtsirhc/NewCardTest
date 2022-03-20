using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputField : MonoBehaviour
{
    public E_EventType eventType;
    public Unit host;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(string value)
    {
        int _value;
        if (int.TryParse(value, out _value))
        {
            switch (eventType)
            {
                case E_EventType.TEST_TYPE:
                    break;
                case E_EventType.DRAW_CARD:
                    break;
                case E_EventType.DELETE_CARD:
                    break;
                case E_EventType.SET_HP:
                    host.SetHp(_value);
                    break;
                case E_EventType.SET_ARMOR:
                    host.SetArmor(_value);
                    break;
                case E_EventType.SET_ANGER:
                    host.SetAnger(_value);
                    break;
                case E_EventType.SET_CALM:
                    host.SetCalm(_value);
                    break;
                default:
                    break;
            }
        }
        else
        {
            TipManager.ShowTip(" ‰»Î÷µ“Ï≥££°");
        }
    }
}
