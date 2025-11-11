// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using TMPro;
using UnityEngine;

namespace UI.Common {
    [ExecuteAlways]
    public class VersionLabel : MonoBehaviour {
        [SerializeField] private TMP_Text targetText;
        [SerializeField] private string prefix = "Wersja: ";
        [SerializeField] private bool includeBuildType = true;
        [SerializeField] private string buildType = "InDev";

        private void Reset() {
            if (targetText == null) {
                targetText = GetComponent<TMP_Text>();
            }
        }

        private void OnEnable() {
            UpdateLabel();
        }

        private void OnValidate() {
            UpdateLabel();
        }

        private void UpdateLabel() {
            if (targetText == null) {
                return;
            }

            var version = Application.version;

            if (includeBuildType && !string.IsNullOrWhiteSpace(buildType)) {
                targetText.text = $"{prefix}{version} ({buildType})";
            } else {
                targetText.text = $"{prefix}{version}";
            }
        }
    }
}