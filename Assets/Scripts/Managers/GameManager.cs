using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameData data;
    private string fileName = "GameData.dat";

    [HideInInspector]
    public int starCount, scoreCount, selectedIndex;
    [HideInInspector]
    public bool[] characters;
    [HideInInspector]
    public bool playSound = true;

    void Awake()
    {
        MakeSingleton();
        InitializeGameData();
    }
    
    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameData()
    {
        LoadGameData();

        if(data == null)
        {
            // We are running game for the first time
            starCount = 9000;
            scoreCount = 0;
            selectedIndex = 0;

            characters = new bool[9];
            characters[0] = true;

            for (int i = 1; i < characters.Length; i++)
            {
                characters[i] = false;
            }

            data = new GameData();
            data.Characters = characters;
            data.StarCount = starCount;
            data.ScoreCount = scoreCount;
            data.SelectedIndex = selectedIndex;

            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + fileName);

            if(data != null)
            {
                data.Characters = characters;
                data.StarCount = starCount;
                data.ScoreCount = scoreCount;
                data.SelectedIndex = selectedIndex;

                bf.Serialize(file, data);
            }
        }
        catch(Exception e)
        {

        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }

    public void LoadGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            data = (GameData)bf.Deserialize(file);

            if (data != null)
            {
                characters = data.Characters;
                starCount = data.StarCount;
                scoreCount = data.ScoreCount;
                selectedIndex = data.SelectedIndex;

                bf.Serialize(file, data);
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}
