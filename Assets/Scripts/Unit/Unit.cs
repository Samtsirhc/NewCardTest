using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public GameObject showArmor;
    public int maxHp;
    private Animator anim;
    public virtual int hp
    {
        get { return _hp; }
        set
        {
            if (_hp > value)
            {
                PlayHurt();
            }
            _hp = value;
        }
    }
    private int _hp;
    public virtual int armor { get { return _armor; }
        set 
        {
            if (value > _armor)
            {
                PlayPower();
            }
            _armor = value;
        }
    }
    private int _armor;
    public virtual int fire { get; set; }
    public virtual int ice { get; set; }
    protected virtual void UpdateUnitStatus()
    {
        string _s = armor.ToString();

        showArmor.GetComponent<Text>().text = _s;
    }
    protected virtual void Start()
    {
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
        anim = GetComponent<Animator>();
        hp = maxHp;
    }
    protected void PlayAttack()
    {
        anim.Play("Attack");
    }
    protected void PlayHurt()
    {
        anim.Play("Hurt");
    }
    protected void PlayPower()
    {
        anim.Play("Power");
    }
    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateUnitStatus();
    }
    public int TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + "takedamage!");
        int _damage = 0;
        SoundManager.Instance.BeAttacked();
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

    /// <summary>
    /// ChuanCi ShangHai 穿刺伤害
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int TakePiercingDamage(int damage){
        hp -= damage;
        return damage;
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
