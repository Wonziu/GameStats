using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public bool valueChanged = false;

    public OptionsData optionsData;
    public Dropdown resolutionDropdown;
    public List<Resolutions> resolutions;

    private void Awake()
    {
        optionsData = new OptionsData();
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });     
    }

    private void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, false);
        optionsData.resolutionIndex = resolutionDropdown.value;
        valueChanged = true;
    }

    public void LoadOptions()
    {
        OptionsData od = FileManagment.ReadFile<OptionsData>("preferences");

        if (od == null)
            return;

        resolutionDropdown.value = od.resolutionIndex;
        resolutionDropdown.RefreshShownValue();

        valueChanged = false;
        optionsData = od;
    }

    public void SaveOptions()
    {
        FileManagment.WriteFile("preferences", optionsData);
    }
}