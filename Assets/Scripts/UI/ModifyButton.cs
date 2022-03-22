using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyButton : MonoBehaviour
{
    public InputField inputField;
    public Dropdown dropdown;
    public Unit unit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Modify()
    {
        int number = 0;
        if (!int.TryParse(inputField.text, out number))
        {
            TipManager.ShowTip("ÊäÈëÒì³£");
            return;
        }
        else
        {
            switch (dropdown.options[dropdown.value].text)
            {
                case "ÉúÃü":
                    unit.hp = number;
                    break;
                case "»¤¼×":
                    unit.armor = number;
                    break;
                case "»ðÑæ":
                    unit.fire = number;
                    break;
                case "º®±ù":
                    unit.ice = number;
                    break;
                default:
                    break;
            }
        }
    }
}
