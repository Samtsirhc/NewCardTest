using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_XueHua : BlueCard
{
    public float kkk = 0.8f;
    public float kkk2 = 0f;
    protected override void Start()
    {
        base.Start();
        coldAlarm = true;
    }
    public override void OnUse()
    {
        base.OnUse();
        CastDamage((int)(GetCalm()*kkk));
    }
    public override void OnFreezed()
    {
        base.OnFreezed();
        for (int i = 0; i < DeckManager.Instance.myCardInFlow.Count; i++)
        {
            Blue_XueHua _target;
            if (DeckManager.Instance.myCardInFlow[i].TryGetComponent(out _target))
            {
                if (_target != this)
                {
                    _target.kkk += kkk;
                    _target.kkk += kkk2;
                    DeckManager.Instance.DeleteCard(position);
                }
                break;
            }
        }
    }
}
