using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    public Button buttonComponent;
    public Text nameLabel;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
    }

    public void Setup(string gameName)
    {
        nameLabel.text = gameName;
    }
}