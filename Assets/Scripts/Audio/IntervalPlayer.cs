// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 16/11/2025
//  */

using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class IntervalPlayer : MonoBehaviour {
        [Header("Audio")]
        [SerializeField] private AudioClip[] intervalClips;

        private AudioSource m_source;
        private int m_currentIndex;
        private bool m_isPlaying;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
            m_source.loop = true;
        }

        private void Start() {
            SetInterval(0, false);
        }

        public void OnSliderValueChanged(float value) {
            int index = Mathf.RoundToInt(value);
            SetInterval(index, m_isPlaying);
        }

        private void SetInterval(int index, bool playAfterChange) {
            if (intervalClips == null || intervalClips.Length == 0) {
                return;
            }

            index = Mathf.Clamp(index, 0, Mathf.Min(12, intervalClips.Length - 1));
            m_currentIndex = index;

            AudioClip clip = intervalClips[index];
            if (clip == null) {
                Debug.LogWarning($"[IntervalPlayer] No clip for index {index}.");
                m_source.Stop();
                m_isPlaying = false;
                return;
            }

            m_source.clip = clip;

            if (playAfterChange) {
                m_source.Stop();
                m_source.Play();
                m_isPlaying = true;
            }
        }

        public void OnTogglePlayClicked() {
            if (m_isPlaying) {
                StopPlayback();
            } else {
                StartPlayback();
            }
        }

        public void StartPlayback() {
            if (m_source.clip == null) {
                SetInterval(m_currentIndex, false);
            }

            if (m_source.clip == null) {
                return;
            }

            m_source.Stop();
            m_source.Play();
            m_isPlaying = true;
        }

        public void StopPlayback() {
            m_source.Stop();
            m_isPlaying = false;
        }
    }
}