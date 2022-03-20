using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    override public int anger 
    {
        get 
        {
            return _anger;
        }
        set
        {
            if (value <= 5)
            {
                _anger = 5;
            }
            else
            {
                _anger = value;
            }
            SetUIValue();
        }
    }
    private int _anger;

    // Start is called before the first frame update
    protected override void Start()
    {
        _anger = 10;
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        anger -= 3;
        SetUIValue();

    }
}
