using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public GameObject ShouUnitStatus;
    virtual public int hp { get; set; }
    virtual public int armor { get; set; }
    virtual public int anger { get; set; }
    virtual public int calm { get; set; }
    virtual public int cold { get; set; }
    virtual public int fire { get; set; }
    virtual public int ice { get; set; }

    private void UpdateUnitStatus()
    {
        string _s = "";
        if (hp > 0)
        {
            _s += "ÉúÃü " + hp + "\n";
        }
        if (armor > 0)
        {
            _s += "»¤¼× " + armor + "\n";
        }
        if (anger > 0)
        {
            _s += "·ßÅ­ " + anger + "\n";
        }
        if (calm > 0)
        {
            _s += "Àä¾² " + calm + "\n";
        }
        if (cold > 0)
        {
            _s += "º®Àä " + cold + "\n";
        }
        if (fire > 0)
        {
            _s += "»ðÑæ " + fire + "\n";
        }
        if (ice > 0)
        {
            _s += "±ùËª " + ice + "\n";
        }
        ShouUnitStatus.GetComponent<Text>().text = _s;
    }
    protected virtual void Start()
    {
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
    }

    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        UpdateUnitStatus();
    }




    public int TakeDamage(int damage)
    {
        int _damage = 0;
        if (damage >= armor)
        {
            _damage = damage - armor;
            armor = 0;
            hp -= _damage;
        }
        else
        {
            armor -= damage;
        }
        return _damage;
    }

    #region Á÷³ÌÏà¹Ø

    public virtual void OnTurnEnd()
    {
        armor = 0;
    }

    public virtual void OnTurnStart()
    {

    }
    #endregion
}
