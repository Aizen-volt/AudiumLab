// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using Progress;
using TMPro;
using UnityEngine;

namespace UI.Lessons {
    public class DisplayLessonTitle :MonoBehaviour {
        private void OnEnable() {
            TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) {
                var lessonRuntime = FindFirstObjectByType<LessonRuntime>();
                if (lessonRuntime != null) {
                    text.text = ProgressService.Instance.GetLessonProgress(lessonRuntime.GetLessonId()).lessonName;
                }
            }
        }
    }
}