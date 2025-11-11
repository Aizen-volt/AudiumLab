// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Lessons.SoundBasics {
    public class DisplaySlidersPhase : MonoBehaviour {
        [SerializeField] private Slider slider;
        private TextMeshProUGUI m_text;

        private void OnEnable() {
            m_text = GetComponent<TextMeshProUGUI>();
        }
        
        private void Update() {
            int phase = Mathf.RoundToInt(slider.value);
            m_text.text = phase + "°";
        }
    }
}