using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLayOut : MonoBehaviour
{
    public GameObject obj;
    private void Start()
    {
        EventCenter.AddListener(E_EventType.HIDE_CARDS, Hide);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.HIDE_CARDS, Hide);
    }
    public void Hide()
    {
        Destroy(gameObject);
    }
}
