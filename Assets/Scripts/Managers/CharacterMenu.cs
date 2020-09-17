using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject starIcon;

    public Text selectedText;
    public Text starCountText;
    public Image SelectedButtonImage;
    public Sprite greenButton, blueButton;

    private bool[] activeCharacters;

    private int currentIndex;

    void Start()
    {
        InitializeCharacters();
    }

    void InitializeCharacters()
    {
        currentIndex = GameManager.instance.selectedIndex   ;
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        characters[currentIndex].SetActive(true);
        activeCharacters = GameManager.instance.characters;
    }

    public void NextCharacter()
    {
        characters[currentIndex].SetActive(false);

        if(currentIndex + 1 == characters.Length)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        characters[currentIndex].SetActive(true);
        CheckStatusOfCharacter();
    }

    public void PreviousCharacter()
    {
        characters[currentIndex].SetActive(false);

        if(currentIndex - 1 == -1)
        {
            currentIndex = characters.Length - 1;
        }
        else
        {
            currentIndex--;
        }
        characters[currentIndex].SetActive(true);
        CheckStatusOfCharacter();
    }

    void CheckStatusOfCharacter()
    {
        if(activeCharacters[currentIndex])
        {
            starIcon.SetActive(false);

            if(currentIndex == GameManager.instance.selectedIndex)
            {
                SelectedButtonImage.sprite = greenButton;
                selectedText.text = "Selected";
            }
            else
            {
                SelectedButtonImage.sprite = blueButton;
                selectedText.text = "Select";
            }
        }
        else
        {
            SelectedButtonImage.sprite = blueButton;
            starIcon.SetActive(true);
            selectedText.text = "1000";
        }
    }

    public void SelectCharacter()
    {
        if(!activeCharacters[currentIndex])
        {
            if (currentIndex != GameManager.instance.selectedIndex)
            {
                if (GameManager.instance.starCount >= 1000)
                {
                    GameManager.instance.starCount -= 1000;
                    SelectedButtonImage.sprite = greenButton;
                    selectedText.text = "Selected";
                    starIcon.SetActive(false);
                    activeCharacters[currentIndex] = true;

                    starCountText.text = GameManager.instance.starCount.ToString();
                    GameManager.instance.selectedIndex = currentIndex;
                    GameManager.instance.characters = activeCharacters;

                    GameManager.instance.SaveGameData();
                }
            }
        }
        else
        {
            SelectedButtonImage.sprite = greenButton;
            selectedText.text = "Selected";

            GameManager.instance.selectedIndex = currentIndex;

            GameManager.instance.SaveGameData();
        }
    }
}
