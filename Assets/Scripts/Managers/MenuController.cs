using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject characterMenu;

    public Text starText;

    public Image musicImage;
    public Sprite musicOff, musicOn;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void CharacterMenu()
    {
        characterMenu.SetActive(true);
        starText.text = "" + GameManager.instance.starCount;
    }

    public void BackToHome()
    {
        characterMenu.SetActive(false);
    }

    public void Music()
    {
        if(GameManager.instance.playSound)
        {
            musicImage.sprite = musicOff;
            GameManager.instance.playSound = false;
        }
        else
        {
            musicImage.sprite = musicOn;
            GameManager.instance.playSound = true;
        }
    }
}
