using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// ReSharper disable SpecifyACultureInStringConversionExplicitly

public class GameManager : MonoBehaviour
{
    private Game gameSave;

    [Header("Text")]
    public Text title;
    public Text recentWins;
    public Text recentLosses;
    public Text recentRatio;
    public Text historyWins;
    public Text historyLosses;
    public Text historyRatio;

    [Header("Background")]
    public Image titlePanel;
    public Image mainPanel;

    private Game _currentGameSave;

    private int _historyWins;
    private int _historyLosses;
    private int _recentWins;
    private int _recentLosses;

    private void Start()
    {       
        KeyInput.EnableHook();
        KeyInput.onKeyDown += PressedButton;
       
        LoadSave();
        SetValues();
    }

    private void OnDisable()
    {
        KeyInput.DisableHook();
        // ReSharper disable once DelegateSubtraction
        KeyInput.onKeyDown -= PressedButton;
    }

    private void PressedButton(Key k)
    {
        if (k == Key.F9)
        {
            _recentWins++;
            recentWins.text = _recentWins.ToString();
            recentRatio.text = GetRatio(_recentWins, _recentLosses);
        }
        else if (k == Key.F10)
        {
            _recentLosses++;
            recentLosses.text = _recentLosses.ToString();
            recentRatio.text = GetRatio(_recentWins, _recentLosses);
        }
    }

    public void Save()
    {
        _historyLosses += _recentLosses;
        _historyWins += _recentWins;
        _recentLosses = 0;
        _recentWins = 0;

        gameSave.losses = _historyLosses;
        gameSave.wins = _historyWins;

        SaveToFile();
        SetValues();
    }

    public void ResetValues()
    {
        _historyLosses = 0;
        _historyWins = 0;
        _recentLosses = 0;
        _recentWins = 0;

        gameSave.losses = 0;
        gameSave.wins = 0;

        SaveToFile();
        SetValues();
    }

    public void ChangeGame()
    {
        Save();
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Save();
        Application.Quit();
    }

    private void SaveToFile()
    {
        SaveData sd = FileManagment.ReadFile<SaveData>("save");
        sd.saves[sd.saves.FindIndex(x => x.gameName == ApplicationModel.GameName)] = gameSave;
        FileManagment.WriteFile("save", sd);
    }

    private void LoadSave()
    {
        SaveData sd = FileManagment.ReadFile<SaveData>("save");
        gameSave = sd.saves.Find(x => x.gameName == ApplicationModel.GameName);
        
        _historyWins = gameSave.wins;
        _historyLosses = gameSave.losses;
    }

    private void SetValues()
    {
        title.text = gameSave.gameName;

        historyWins.text = "Wins:  " + _historyWins;
        historyLosses.text = "Losses: " + _historyLosses;
        historyRatio.text = "W/L: " + GetRatio(_historyWins, _historyLosses);

        recentWins.text = "0";
        recentLosses.text = "0";    
        recentRatio.text = "0";
    }

    private string GetRatio(int wins, int losses)
    {
        if (losses != 0)
            return Math.Round((double)wins / losses, 2).ToString();
        return "Perfect";
    }

}
