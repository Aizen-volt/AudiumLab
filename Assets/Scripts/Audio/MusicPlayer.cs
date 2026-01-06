// /*
//  * Copyright © 2026 AudiumLab
//  * Author: Tomasz Krępa
//  * Created on: 05/01/2026
//  */

using UnityEngine;

namespace Audio {
    public class MusicPlayer : MonoBehaviour {
        
        private void Awake() {
            var source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;

            if (VolumeManager.Instance != null) {
                source.volume = VolumeManager.Instance.Volume;
            }
        }

        private void OnEnable() {
            var source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = true;

            if (VolumeManager.Instance != null) {
                source.volume = VolumeManager.Instance.Volume;
            }
        }
    }
}