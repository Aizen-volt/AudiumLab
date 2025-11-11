// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using UnityEngine;

namespace UI.Lessons {
    public class HidePreviousSlideButtonOnFirstSlide : MonoBehaviour {
        private void OnEnable() {
            if (FindFirstObjectByType<LessonRuntime>()?.GetCurrentSlide() == 0) {
                gameObject.SetActive(false);
            } else {
                gameObject.SetActive(true);
            }
        }
    }
}