using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Color pickedColor;

    public Image colorPool;
    public Image hue;

    public GameObject poolKnob;
    public GameObject result;
    public GameObject hueKnob;

    private void Start()
    {
        CreateHue();
    }

    private void CreateHue()
    {
        var texture = new Texture2D(1, 7);

        var hueColors = new[] {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            Color.magenta,
            Color.red 
        };

        for (int i = 0; i < hueColors.Length; i++)
            texture.SetPixel(0, i, hueColors[i]);

        texture.Apply();

        hue.sprite = Sprite.Create(texture, new Rect(0, 0, 1, 6), new Vector2(0.5f, 0.5f));
    }
}
