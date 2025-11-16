// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 15/11/2025
//  */

using System.Collections;
using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class WhiteNoisePlayer : MonoBehaviour {
        [Range(0f, 1f)]
        [SerializeField] private float amplitude = 0.2f;

        private AudioSource m_source;
        private bool m_isPlaying;
        
        private Coroutine m_stopCoroutine;
        
        private System.Random m_rng;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
            m_source.loop = true;
            m_rng = new System.Random();

            if (VolumeManager.Instance != null)
                m_source.volume = VolumeManager.Instance.Volume;
        }

        private void OnAudioFilterRead(float[] data, int channels) {
            if (!m_isPlaying) {
                for (int i = 0; i < data.Length; i++)
                    data[i] = 0f;
                return;
            }

            for (int i = 0; i < data.Length; i++) {
                double d = m_rng.NextDouble() * 2.0 - 1.0;
                float white = (float)d;
                data[i] = white;
            }
        }

        public void PlayNoise() {
            m_isPlaying = true;
            if (!m_source.isPlaying)
                m_source.Play();
        }

        public void StopNoise() {
            m_isPlaying = false;
            if (m_source.isPlaying)
                m_source.Stop();
        }
        
        public void PlayNoise(float duration) {
            if (duration <= 0f) return;

            if (m_stopCoroutine != null) {
                StopCoroutine(m_stopCoroutine);
                m_stopCoroutine = null;
            }

            PlayNoise();

            m_stopCoroutine = StartCoroutine(StopAfter(duration));
        }

        private IEnumerator StopAfter(float duration) {
            yield return new WaitForSeconds(duration);
            StopNoise();
            m_stopCoroutine = null;
        }
    }
}