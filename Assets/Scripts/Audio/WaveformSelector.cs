// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 15/11/2025
//  */

using TMPro;
using UnityEngine;

namespace Audio {
    public class WaveformSelector : MonoBehaviour {
        [SerializeField] private WaveformTonePlayer tonePlayer;
        [SerializeField] private TextMeshProUGUI waveformLabel;

        public void SelectSine()     => SetWaveform(WaveformType.Sine);
        public void SelectSquare()   => SetWaveform(WaveformType.Square);
        public void SelectSaw()      => SetWaveform(WaveformType.Saw);
        public void SelectTriangle() => SetWaveform(WaveformType.Triangle);

        private void SetWaveform(WaveformType type) {
            if (tonePlayer == null) {
                return;
            }

            tonePlayer.SetWaveform(type);

            if (waveformLabel != null) {
                waveformLabel.text = GetDisplayName(type);
            }
        }

        private static string GetDisplayName(WaveformType type) {
            switch (type) {
                case WaveformType.Sine:
                    return "Sinusoida";
                case WaveformType.Square:
                    return "Prostokąt";
                case WaveformType.Saw:
                    return "Piłokształtny";
                case WaveformType.Triangle:
                    return "Trójkątny";
                default:
                    return type.ToString();
            }
        }
    }
}