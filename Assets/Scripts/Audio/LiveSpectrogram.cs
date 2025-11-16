// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 15/11/2025
//  */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Audio {
    [RequireComponent(typeof(RawImage))]
    public class LiveSpectrogram : MonoBehaviour {
        [SerializeField] private RawImage output;

        [Header("Sound sources")]
        [Tooltip("If not empty it will use these AudioSources instead of AudioListener")]
        [SerializeField] private List<AudioSource> targetSources = new();

        [Header("Texture resolution (UI)")]
        [SerializeField] private int width = 512;
        [SerializeField] private int height = 256;

        [Header("FFT")]
        [SerializeField] private int spectrumSize = 512; // 256 / 512 / 1024
        [SerializeField] private FFTWindow window = FFTWindow.BlackmanHarris;

        [Header("Frequency range (Hz)")]
        [SerializeField] private float minFreq = 50f;
        [SerializeField] private float maxFreq = 8000f;

        private Texture2D m_texture;
        private Color32[] m_pixels;
        private float[] m_spectrum;
        private float[] m_tempSpectrum;
        private int m_sampleRate;

        private void Awake() {
            if (output == null) {
                output = GetComponent<RawImage>();
            }

            m_texture = new Texture2D(width, height, TextureFormat.RGB24, false);
            m_pixels = new Color32[width * height];
            m_spectrum = new float[spectrumSize];
            m_tempSpectrum = new float[spectrumSize];
            m_sampleRate = AudioSettings.outputSampleRate;

            ClearTexture();

            if (output != null) {
                output.texture = m_texture;
            }
        }

        private void Update() {
            if (m_texture == null) {
                return;
            }

            if (targetSources != null && targetSources.Count > 0) {
                for (int i = 0; i < spectrumSize; i++) {
                    m_spectrum[i] = 0f;
                }

                int activeCount = 0;

                foreach (var src in targetSources) {
                    if (src == null) continue;

                    src.GetSpectrumData(m_tempSpectrum, 0, window);
                    activeCount++;

                    for (int i = 0; i < spectrumSize; i++) {
                        m_spectrum[i] += m_tempSpectrum[i];
                    }
                }

                if (activeCount > 1) {
                    float inv = 1f / activeCount;
                    for (int i = 0; i < spectrumSize; i++) {
                        m_spectrum[i] *= inv;
                    }
                }
            } else {
                AudioListener.GetSpectrumData(m_spectrum, 0, window);
            }

            float maxMag = 0f;
            for (int i = 0; i < spectrumSize; i++) {
                if (m_spectrum[i] > maxMag) {
                    maxMag = m_spectrum[i];
                }
            }
            if (maxMag <= 0f) {
                maxMag = 1e-7f;
            }

            for (int x = 1; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    m_pixels[(x - 1) + y * width] = m_pixels[x + y * width];
                }
            }

            float nyquist = m_sampleRate * 0.5f;
            float logMin = Mathf.Log10(minFreq);
            float logMax = Mathf.Log10(maxFreq);

            for (int y = 0; y < height; y++) {
                float yNorm = (float)y / (height - 1);

                float logF = Mathf.Lerp(logMin, logMax, yNorm);
                float freq = Mathf.Pow(10f, logF);

                int index = Mathf.RoundToInt(freq / nyquist * (spectrumSize - 1));
                index = Mathf.Clamp(index, 0, spectrumSize - 1);

                float mag = m_spectrum[index];

                float t = mag / maxMag;
                t = Mathf.Clamp01(t);

                m_pixels[(width - 1) + y * width] = MapToColor(t);
            }

            m_texture.SetPixels32(m_pixels);
            m_texture.Apply(false);
        }

        private void ClearTexture() {
            Color32 background = MapToColor(0f);

            for (int i = 0; i < m_pixels.Length; i++) {
                m_pixels[i] = background;
            }

            m_texture.SetPixels32(m_pixels);
            m_texture.Apply(false);
        }


        private static Color32 MapToColor(float t) {
            t = Mathf.Pow(t, 0.6f);

            float r = Mathf.SmoothStep(0f, 1f, t * 1.2f);
            float g = Mathf.SmoothStep(0f, 1f, t - 0.2f);
            float b = Mathf.SmoothStep(0f, 1f, 0.3f - t * 0.7f);

            return new Color(r, g, b);
        }
    }
}
