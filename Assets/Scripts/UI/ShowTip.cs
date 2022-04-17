using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTip : MonoBehaviour
{
    public string Tip;
    public GameObject tipPfb;
    public Vector3 offset;
    private EventTrigger eventTrigger;
    private GameObject obj;
    private void InitTriggers()
    {
        eventTrigger = GetComponent<EventTrigger>();
        AddPointerEvent(eventTrigger, EventTriggerType.PointerEnter, PointerEnter);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerExit, PointerExit);

    }

    private void PointerExit(BaseEventData arg0)
    {
        Destroy(obj);
    }
    private void PointerEnter(BaseEventData arg0)
    {
        obj = Instantiate(tipPfb, transform);
        obj.transform.Translate(offset);
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
