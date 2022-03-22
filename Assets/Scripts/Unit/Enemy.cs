using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    protected override void Start()
    {
        base.Start();
        hp = 100;
    }
}
