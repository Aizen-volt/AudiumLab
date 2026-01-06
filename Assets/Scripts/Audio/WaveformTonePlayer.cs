// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using System.Collections;
using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class WaveformTonePlayer : MonoBehaviour {
        [SerializeField] private WaveformType waveform = WaveformType.Sine;

        [Range(20f, 20000f)]
        [SerializeField] private float frequency = 440f;

        [Range(0f, 1f)]
        [SerializeField] private float amplitude = 0.2f;

        private AudioSource m_source;
        private int m_sampleRate;
        private double m_phase;
        private bool m_isPlaying;
        
        private Coroutine m_stopCoroutine;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
            m_source.loop = true;

            m_sampleRate = AudioSettings.outputSampleRate;
        }

        private void OnEnable() {
            m_isPlaying = false;
            if (m_source.isPlaying) {
                m_source.Stop();
            }
        }

        private void OnAudioFilterRead(float[] data, int channels) {
            if (!m_isPlaying) {
                for (var i = 0; i < data.Length; i++) {
                    data[i] = 0f;
                }
                return;
            }

            if (m_sampleRate <= 0) {
                m_sampleRate = AudioSettings.outputSampleRate;
            }

            var increment = 2.0 * Mathf.PI * frequency / m_sampleRate;

            for (var i = 0; i < data.Length; i += channels) {
                var rawSample = GenerateSample(m_phase, waveform);
                var sample = amplitude * rawSample;

                for (var c = 0; c < channels; c++) {
                    data[i + c] = (float)sample;
                }

                m_phase += increment;
                if (m_phase > Mathf.PI * 2) {
                    m_phase -= Mathf.PI * 2;
                }
            }
        }

        private static float GenerateSample(double phase, WaveformType type) {
            var p = (float)phase;

            switch (type) {
                case WaveformType.Sine:
                    return Mathf.Sin(p);

                case WaveformType.Square:
                    return Mathf.Sign(Mathf.Sin(p));

                case WaveformType.Saw: {
                    var t = (float)(phase / (2.0 * Mathf.PI));
                    t -= Mathf.Floor(t);
                    return 2f * t - 1f;
                }

                case WaveformType.Triangle: {
                    var t = (float)(phase / (2.0 * Mathf.PI));
                    t -= Mathf.Floor(t);
                    var tri = 2f * Mathf.Abs(2f * t - 1f) - 1f;
                    return tri;
                }

                default:
                    return 0f;
            }
        }

        public void SetFrequency(float newFreq) {
            frequency = Mathf.Clamp(newFreq, 20f, 20000f);
        }

        public void SetAmplitude(float newAmp) {
            amplitude = Mathf.Clamp01(newAmp);
        }

        public void SetWaveform(WaveformType type) {
            waveform = type;
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
        
        public void PlayTone(float duration) {
            if (duration <= 0f) return;

            if (m_stopCoroutine != null) {
                StopCoroutine(m_stopCoroutine);
                m_stopCoroutine = null;
            }

            PlayTone();

            m_stopCoroutine = StartCoroutine(StopAfter(duration));
        }

        private IEnumerator StopAfter(float duration) {
            yield return new WaitForSeconds(duration);
            StopTone();
            m_stopCoroutine = null;
        }
    }
}
