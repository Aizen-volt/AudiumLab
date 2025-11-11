// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class SineWavePlayer : MonoBehaviour {
        [Range(20f, 20000f)]
        [SerializeField] private float frequency = 440f;

        [Range(0f, 1f)]
        [SerializeField] private float amplitude = 0.2f;

        private AudioSource m_source;
        private int m_sampleRate;
        private double m_phase;
        private bool m_isPlaying;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
            m_source.loop = true;
            m_source.volume = 1.0f;

            m_sampleRate = AudioSettings.outputSampleRate;
        }

        private void OnAudioFilterRead(float[] data, int channels) {
            if (!m_isPlaying) {
                for (var i = 0; i < data.Length; i++) {
                    data[i] = 0f;
                }
                return;
            }

            var increment = 2.0 * Mathf.PI * frequency / m_sampleRate;

            for (var i = 0; i < data.Length; i += channels) {
                var sample = (float)(amplitude * Mathf.Sin((float)m_phase));

                for (var c = 0; c < channels; c++) {
                    data[i + c] = sample;
                }

                m_phase += increment;
                if (m_phase > Mathf.PI * 2) {
                    m_phase -= Mathf.PI * 2;
                }
            }
        }

        public void SetFrequency(float newFreq) {
            frequency = Mathf.Clamp(newFreq, 20f, 20000f);
        }

        public void SetAmplitude(float newAmp) {
            amplitude = Mathf.Clamp01(newAmp);
        }

        public void PlayTone() {
            if (m_isPlaying) {
                return;
            }

            m_isPlaying = true;
            if (!m_source.isPlaying) {
                m_source.Play();
            }
        }

        public void StopTone() {
            if (!m_isPlaying) {
                return;
            }

            m_isPlaying = false;
            if (m_source.isPlaying) {
                m_source.Stop();
            }
        }
    }
}