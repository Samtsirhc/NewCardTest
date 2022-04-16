using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurnIceButton : MonoBehaviour
{
    public bool isBurn;
    public bool isWork = false;
    public GameObject arrow;
    private MyCard _other;
    private EventTrigger eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        AddPointerEvent(eventTrigger, EventTriggerType.PointerDown, PointerDown);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerUp, PointerUp);
        // arrow.SetActive(true);
        // arrow.GetComponent<MyMouseArrow>().origin.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    void update()
    {

    }

    private void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }

    void PointerDown(BaseEventData arg0)
    {
        arrow.SetActive(true);
        arrow.GetComponent<MyMouseArrow>().origin.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        isWork = true;
    }

    void PointerUp(BaseEventData arg0)
    {
        MyCard _other;
        if (isWork)
        {
            if (UIManager.Instance.ObjBePointed.TryGetComponent(out _other))
            {
                if (isBurn) { _other.BurnButton(); }
                else { _other.IceButton(); }
            }
        }
        isWork = false;
    }
}
