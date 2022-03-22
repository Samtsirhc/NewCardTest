using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{

    //override public int anger 
    //{
    //    get 
    //    {
    //        return _anger;
    //    }
    //    set
    //    {
    //        if (value <= 5)
    //        {
    //            _anger = 5;
    //        }
    //        else
    //        {
    //            _anger = value;
    //        }
    //        SetUIValue();
    //    }
    //}
    //private int _anger;
    //override public int calm
    //{
    //    get
    //    {
    //        return _calm;
    //    }
    //    set
    //    {
    //        if (value <= 3)
    //        {
    //            _calm = 3;
    //        }
    //        else
    //        {
    //            _calm = value;
    //        }
    //        SetUIValue();
    //    }
    //}
    //private int _calm;

    // Start is called before the first frame update
    protected override void Start()
    {
        //_anger = 10;
        //_calm = 3;
        base.Start();
        hp = 100;
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
