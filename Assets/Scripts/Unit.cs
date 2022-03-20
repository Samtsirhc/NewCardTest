using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    virtual public int hp { get; set; }
    virtual public int armor { get; set; }
    virtual public int anger { get; set; }
    virtual public int calm { get; set; }
    virtual public int cold { get; set; }

    public GameObject hpUI;
    public GameObject armorUI;
    public GameObject angerUI;
    public GameObject calmUI;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetUIValue();
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
    }


    #region 设置玩家属性
    public void SetHp(int _var)
    {
        hp = _var;
        SetUIValue();
    }
    public void SetArmor(int _var)
    {
        armor = _var;
        SetUIValue();
    }
    public void SetAnger(int _var)
    {
        anger = _var;
        SetUIValue();
    }
    public void SetCalm(int _var)
    {
        calm = _var;
        SetUIValue();
    }
    public void SetUIValue()
    {
        hpUI.GetComponent<InputField>().text = hp.ToString();
        armorUI.GetComponent<InputField>().text = armor.ToString();
        angerUI.GetComponent<InputField>().text = anger.ToString();
        calmUI.GetComponent<InputField>().text = calm.ToString();
    }
#endregion
    public int TakeDamage(int damage)
    {
        int _damage = 0;
        if (damage >= armor)
        {
            _damage = damage - armor;
            hp -= _damage;
        }
        else
        {
            armor -= damage;
        }
        SetUIValue();
        return _damage;
    }

    #region 流程相关

    public virtual void OnTurnEnd()
    {

    }

    public virtual void OnTurnStart()
    {

    }
    #endregion
}
