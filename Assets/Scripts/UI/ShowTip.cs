using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ShowTip : MonoBehaviour
{
    public string tip = "";
    private EventTrigger eventTrigger;
    private void Start()
    {
        InitTriggers();
    }
    private void InitTriggers()
    {
        eventTrigger = GetComponent<EventTrigger>();
        AddPointerEvent(eventTrigger, EventTriggerType.PointerEnter, PointerEnter);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerExit, PointerExit);

    }

    private void PointerExit(BaseEventData arg0)
    {
        EventCenter.Broadcast(E_EventType.HIDE_GAME_TIP);
    }
    private void PointerEnter(BaseEventData arg0)
    {
        EventCenter.Broadcast(E_EventType.SHOW_GAME_TIP, tip);
    }
    private void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }
}
