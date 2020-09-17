using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource moveAudio, jumpAudio, powerupAudio, backgroundAudio;
    public AudioClip powerup, die, coin, gameover;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        if(GameManager.instance.playSound)
        {
            backgroundAudio.Play();
        }
        else
        {
            backgroundAudio.Stop();
        }
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void PlayMoveAudio()
    {
        moveAudio.Play();
    }

    public void PlayJumpAudio()
    {
        jumpAudio.Play();
    }

    public void PlayDieAudio()
    {
        powerupAudio.clip = die;
        powerupAudio.Play();
    }

    public void PlayPoweupAudio()
    {
        powerupAudio.clip = powerup;
        powerupAudio.Play();
    }

    public void PlayCoinAudio()
    {
        powerupAudio.clip = coin;
        powerupAudio.Play();
    }

    public void PlayGameOverAudio()
    {
        backgroundAudio.Stop();
        backgroundAudio.clip = gameover;
        backgroundAudio.loop = false;
        backgroundAudio.Play();
    }
}
