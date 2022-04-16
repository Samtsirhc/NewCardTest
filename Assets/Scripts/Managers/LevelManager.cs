using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int playerHp = 100;
    public int switchIndex = 1;
    protected override void Awake(){
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
