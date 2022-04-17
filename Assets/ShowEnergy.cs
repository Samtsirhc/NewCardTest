using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnergy : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        GetComponent<Text>().text = "¼¼ÄÜµã£º" + BattleManager.Instance.playCost.ToString();
    }
}
