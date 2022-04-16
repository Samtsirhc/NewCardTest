using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BurnIceButton : MonoBehaviour
{
    public bool isBurn;
    public bool isWork = false;
    private MyCard _other;
    private EventTrigger eventTrigger;
    private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        AddPointerEvent(eventTrigger, EventTriggerType.PointerDown, PointerDown);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerUp, PointerUp);
        _text = transform.GetChild(0).GetComponent<Text>();
    }

    private void FixedUpdate() {
        if(isBurn){
            _text.text = "»ðÑæ£º " + BattleManager.Instance.player.fire;
        }
        else{
            _text.text = "º®±ù£º " + BattleManager.Instance.player.ice;
        }
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
        EventCenter.Broadcast(E_EventType.SHOW_ARROW);
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
