// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using TMPro;
using UnityEngine;

namespace UI.Lessons {
    public class LessonFooterSlideCount : MonoBehaviour {
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private LessonRuntime lessonRuntime;

        private void Reset() {
            if (progressText == null)
                progressText = GetComponent<TMP_Text>();
            if (lessonRuntime == null)
                lessonRuntime = FindFirstObjectByType<LessonRuntime>();
        }

        private void OnEnable() {
            UpdateLabel();
        }

        private void UpdateLabel() {
            if (progressText == null)
                return;

            var displayNumber = Mathf.Clamp(lessonRuntime.GetCurrentSlide() + 1, 1, lessonRuntime.GetTotalSlides());
            progressText.text = $"{displayNumber}/{lessonRuntime.GetTotalSlides()}";
        }
    }
}