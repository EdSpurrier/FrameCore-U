using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PresetURL
{
    public string presetName;
    public string url = "http://www.google.com/";
}

public class OpenURL : MonoBehaviour
{
    public List<PresetURL> presets;


    public void OpenPresetURL(string presetName)
    {
        string url = presets.First(preset => preset.presetName == presetName).url;
        Debug.Log("Opening Preset URL: " + url);
        Application.OpenURL(url);
    }



    public void OpenAddressURL (string url)
    {
        Debug.Log("Opening URL: " + url);
        Application.OpenURL(url);
    }

}
