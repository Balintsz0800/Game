using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Dropdown resolutionDropdown;
    
    private float sensitivity = 1f;
    private Resolution[] resolutions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        sensitivitySlider.value = sensitivity;
        
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new Dropdown.OptionData(option));

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetSensitivity(float value)
    {
        sensitivity = value;
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }
}
