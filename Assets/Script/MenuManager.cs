using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField newGameField;
    public Text result;
    public GameButton gameButton;

    public RectTransform gameList;
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

        result.text = "Utworzono gre!";
        result.color = Color.green;

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

        if (loadGameObject.activeSelf)
            LoadGameList();
        else
            foreach (RectTransform child in gameList)
                Destroy(child.gameObject);
    }

    private void LoadGameList()
    {
        SaveData sd = FileManagment.ReadFile<SaveData>("save");

        if (sd == null || sd.saves == null)
            return;

        foreach (Game sdSave in sd.saves)
        {
            GameButton b = Instantiate(gameButton, gameList);
            b.nameLabel.text = sdSave.gameName;
            var save = sdSave;
            b.buttonComponent.onClick.AddListener(delegate { LoadGame(save.gameName); });
        }
    }

    public void LoadGame(string gname)
    {
        ApplicationModel.GameName = gname;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}