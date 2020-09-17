using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class GameData
{
    private int starCount;
    private int scoreCount;

    private bool[] characters;

    private int selectedIndex;

    public int StarCount
    {
        get
        {
            return starCount;
        }
        set
        {
            starCount = value;
        }
    }

    public int ScoreCount
    {
        get
        {
            return scoreCount;
        }
        set
        {
            scoreCount = value;
        }
    }

    public bool[] Characters
    {
        get
        {
            return characters;
        }
        set
        {
            characters = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return selectedIndex;
        }
        set
        {
            selectedIndex = value;
        }
    }
}
