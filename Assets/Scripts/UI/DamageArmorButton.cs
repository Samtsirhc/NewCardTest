using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageArmorButton : MonoBehaviour
{
    public InputField inputField;
    public Dropdown dropdown;
    public Unit unit;
    public Unit target;
    // Start is called before the first frame update

    public void Modify()
    {
        int number = 0;
        if (!int.TryParse(inputField.text, out number))
        {
            TipManager.ShowTip(" ‰»Î“Ï≥£");
            return;
        }
        else
        {
            switch (dropdown.options[dropdown.value].text)
            {
                case "…À∫¶":
                    target.TakeDamage(number);
                    break;
                case "ª§º◊":
                    unit.armor += number;
                    break;
                default:
                    break;
            }
        }
    }
}
