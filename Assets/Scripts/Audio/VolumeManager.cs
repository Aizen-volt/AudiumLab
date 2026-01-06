// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Tomasz Krępa
//  * Created on: 13/11/2025
//  */

using UnityEngine;

namespace Audio {
    public class VolumeManager : MonoBehaviour {
        public static VolumeManager Instance { get; private set; }

        private const string k_volumePrefsKey = "AudiumLab_MasterVolume";

        private float m_volume = 1.0f;
        public float Volume {
            get => m_volume;
            set {
                m_volume = value;
                AudioListener.volume = m_volume;
                PlayerPrefs.SetFloat(k_volumePrefsKey, m_volume);
                PlayerPrefs.Save();
            }
        }

        private void Awake() {
            // Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey(k_volumePrefsKey)) {
                m_volume = Mathf.Clamp01(PlayerPrefs.GetFloat(k_volumePrefsKey, 1.0f));
                AudioListener.volume = m_volume;
            } else {
                m_volume = 1.0f;
                PlayerPrefs.SetFloat(k_volumePrefsKey, m_volume);
                PlayerPrefs.Save();
            }
        }
    }
}