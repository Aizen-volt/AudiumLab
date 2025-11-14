// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Tomasz Krępa
//  * Created on: 13/11/2025
//  */


using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

namespace Audio {
    public class VolumeManager : MonoBehaviour {
        public static VolumeManager Instance { get; private set; }
        public float Volume { get; set; } = 1.0f;


        private void Awake() {
            // Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Debug.Log("hiiiiiiiiii");
            DontDestroyOnLoad(gameObject);
        }

    }
}