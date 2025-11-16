// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 15/11/2025
//  */

using System;
using System.Collections;
using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class PinkNoisePlayer : MonoBehaviour {
        [Range(0f, 1f)]
        [SerializeField] private float amplitude = 0.2f;

        private AudioSource m_source;
        private bool m_isPlaying;

        private Coroutine m_stopCoroutine;

        private float b0, b1, b2, b3, b4, b5;

        private System.Random m_rng;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
            m_source.loop = true;

            if (VolumeManager.Instance != null) {
                m_source.volume = VolumeManager.Instance.Volume;
            }

            m_rng = new System.Random();
        }

        private void OnAudioFilterRead(float[] data, int channels) {
            if (!m_isPlaying) {
                for (int i = 0; i < data.Length; i++) {
                    data[i] = 0f;
                }
                return;
            }

            for (int i = 0; i < data.Length; i += channels) {
                double d = m_rng.NextDouble() * 2.0 - 1.0;
                float white = (float)d;

                b0 = 0.99886f * b0 + white * 0.0555179f;
                b1 = 0.99332f * b1 + white * 0.0750759f;
                b2 = 0.96900f * b2 + white * 0.1538520f;
                b3 = 0.86650f * b3 + white * 0.3104856f;
                b4 = 0.55000f * b4 + white * 0.5329522f;
                b5 = -0.7616f * b5 - white * 0.0168980f;

                float pink = b0 + b1 + b2 + b3 + b4 + b5 + white * 0.5362f;

                pink *= amplitude;

                for (int c = 0; c < channels; c++) {
                    data[i + c] = pink;
                }
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
