using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject objBePointed;
    public GameObject gameTipPfb;
    private GameObject gameTipObj;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(gameObject);
        EventCenter.AddListener<string>(E_EventType.SHOW_GAME_TIP, ShowGameTip);
        EventCenter.AddListener(E_EventType.HIDE_GAME_TIP, HideGameTip);

    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(E_EventType.SHOW_GAME_TIP, ShowGameTip);
        EventCenter.RemoveListener(E_EventType.HIDE_GAME_TIP, HideGameTip);
    }
    void ShowGameTip(string tip)
    {
        gameTipObj = Instantiate(gameTipPfb, GameObject.Find("Canvas").transform);
        gameTipObj.GetComponentInChildren<Text>().text = tip;
    }
    void HideGameTip()
    {
        Destroy(gameTipObj);
    }

}
