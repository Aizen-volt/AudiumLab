using Audio;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderManager : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("VolumeSliderManager: Slider missing!");
        }
        
        if (VolumeManager.Instance == null)
        {
            Debug.LogError("VolumeSliderManager: No VolumeManager found in scene!");
            return;
        }

        slider.value = VolumeManager.Instance.Volume;
    }

    public void UpdateVolume()
    {
        if (VolumeManager.Instance == null)
        {
            Debug.LogError("VolumeSliderManager: No VolumeManager found in scene!");
            return;
        }

        VolumeManager.Instance.Volume = slider.value;
    }
}
