using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField loadGameField;
    public InputField newGameField;
    public Text result;

    public GameObject optionsObject;
    public GameObject createGameObject;
    public GameObject loadGameObject;
    public OptionsManager optionsManager;

    private void Start()
    {
        optionsManager.LoadOptions();
    }

    public void LoadSave()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleOptions()
    {
        optionsObject.SetActive(!optionsObject.activeSelf);
        if (!optionsObject.activeSelf && optionsManager.valueChanged)
        {
            optionsManager.valueChanged = false;
            optionsManager.SaveOptions();
        }
    }

    public void CreateNewGame()
    {
        SaveData sd = FileManagment.ReadFile<SaveData>("save");
        string gameName = newGameField.text;

        if (sd == null || sd.saves == null)
            sd = new SaveData { saves = new List<Game>() };

        if (sd.saves.Exists(x => x.gameName == gameName))
        {
            result.text = "Gra o takiej nazwie istnieje!";
            result.color = Color.red;
            return;
        }

        Game newGame = new Game { gameName = gameName };
        sd.saves.Add(newGame);

        FileManagment.WriteFile("save", sd);
    }

    public void ToogleNewGame()
    {
        createGameObject.SetActive(!createGameObject.activeSelf);
        result.text = "";
        newGameField.text = "";
    }

    public void ToogleLoadGame()
    {
        loadGameObject.SetActive(!loadGameObject.activeSelf);
    }

    public void LoadGame()
    {
        ApplicationModel.GameName = loadGameField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}