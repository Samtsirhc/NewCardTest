using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public override int fire
    {
        get { return _fire; }
        set
        {
            if (canGetFire)
            {
                if (value > _fire)
                {
                    SoundManager.Instance.GetFire();
                }
                _fire = value;
                Debug.Log(value + "»ðÑæ");
            }
            else
            {
                if (value - _fire <= 0)
                {
                    _fire = value;
                }
            }
        }
    }
    public override int ice
    {
        get { return _ice; }
        set
        {
            if (value > _ice)
            {
                SoundManager.Instance.GetIce();
            }
            _ice = value;
            Debug.Log(value + "º®±ù");
        }
    }
    [HideInInspector]
    public bool canGetFire = true;
    private int _fire = 0;
    private int _ice = 0;

    protected override void Start()
    {
        base.Start();
        canGetFire = true;
        hp = LevelManager.Instance.playerHp;
    }

    protected override void UpdateUnitStatus() {
        base.UpdateUnitStatus();
        //ShowUnitStatus.GetComponent<Text>().text += "·ÑÓÃ " + BattleManager.Instance.playCost + "\n";   // TODO
    }

    public override void OnTurnEnd(){
        armor = 0;
    }
}
