// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using UnityEngine;

namespace UI.Lessons {
    public class HidePreviousNextOnLastSlide : MonoBehaviour {
        private void OnEnable() {
            LessonRuntime lessonRuntime = FindFirstObjectByType<LessonRuntime>();
            if (lessonRuntime.GetCurrentSlide() == lessonRuntime.GetTotalSlides() - 1) {
                gameObject.SetActive(false);
            } else {
                gameObject.SetActive(true);
            }
        }
    }
}