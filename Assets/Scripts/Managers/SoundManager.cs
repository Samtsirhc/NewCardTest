using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    public AudioSource sound;
    public AudioClip beAttacked;
    public AudioClip burnCard;
    public AudioClip deleteCard;
    public AudioClip drawCard;
    public AudioClip freezeCard;
    public AudioClip getCold;
    public AudioClip getFire;
    public AudioClip switchCard;

    public void BeAttacked()
    {
        sound.clip = beAttacked;
        sound.Play();
    }
    public void BurnCard()
    {
        sound.clip = burnCard;
        sound.Play();
    }
    public void DeleteCard()
    {
        sound.clip = deleteCard;
        sound.Play();
    }
    public void DrawCard()
    {
        sound.clip = drawCard;
        sound.Play();
    }
    public void FreezeCard()
    {
        sound.clip = freezeCard;
        sound.Play();

    }
    public void GetIce()
    {
        sound.clip = getCold;
        sound.Play();

    }
    public void GetFire()
    {
        sound.clip = getFire;
        sound.Play();

    }
    public void SwitchCard()
    {
        sound.clip = switchCard;
        sound.Play();

    }
}
