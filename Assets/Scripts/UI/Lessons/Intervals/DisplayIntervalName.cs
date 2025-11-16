// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 16/11/2025
//  */

using TMPro;
using UnityEngine;

namespace UI.Lessons.Intervals {
    public class DisplayIntervalName : MonoBehaviour {
        private TextMeshProUGUI text;
        
        private void Awake() {
            text = GetComponent<TextMeshProUGUI>();
        }
        
        public void ChangeIntervalName(float semitones) {
            int semitonesInt = Mathf.RoundToInt(semitones);
            string intervalName = semitones switch {
                0 => "Pryma (0 półtonów)",
                1 => "Sekunda mała (1 półton)",
                2 => "Sekunda wielka (2 półtony)",
                3 => "Tercja mała (3 półtony)",
                4 => "Tercja wielka (4 półtony)",
                5 => "Kwarta czysta (5 półtonów)",
                6 => "Tryton (6 półtonów)",
                7 => "Kwinta czysta (7 półtonów)",
                8 => "Seksta mała (8 półtonów)",
                9 => "Seksta wielka (9 półtonów)",
                10 => "Septyma mała (10 półtonów)",
                11 => "Septyma wielka (11 półtonów)",
                12 => "Oktawa (12 półtonów)",
                _ => "Nieznany interwał"
            };
            text.text = intervalName;
        }
    }
}