// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using Progress;
using UnityEngine;

namespace UI.Lessons {
    public class LessonRuntime : MonoBehaviour {
        [SerializeField] private string lessonId;
        [SerializeField] private GameObject[] slidePanels;

        private int m_currentSlide;
        private static int? s_previousSlide; // Going back from settings retriggers progress check, this will avoid change
        private void Start() {
            m_currentSlide = CalculateStartSlideIndex();
            ShowSlide(m_currentSlide);
            ReportProgress();
        }

        public void NextSlide() {
            if (slidePanels == null || slidePanels.Length == 0) {
                return;
            }

            if (m_currentSlide < slidePanels.Length - 1) {
                m_currentSlide++;
                ShowSlide(m_currentSlide);
                ReportProgress();
            }
        }

        public void PreviousSlide() {
            if (slidePanels == null || slidePanels.Length == 0) {
                return;
            }

            if (m_currentSlide > 0) {
                m_currentSlide--;
                ShowSlide(m_currentSlide);
            }
        }

        private int CalculateStartSlideIndex() {
            if (s_previousSlide.HasValue) {
                var totalSlides = slidePanels?.Length ?? 0;
                if (totalSlides <= 0) {
                    return 0;
                }

                var restored = Mathf.Clamp(s_previousSlide.Value, 0, totalSlides - 1);
                s_previousSlide = null;
                return restored;
            }
            
            if (slidePanels == null || slidePanels.Length == 0) {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(lessonId) || ProgressService.Instance == null) {
                return 0;
            }

            var entry = ProgressService.Instance.GetLessonProgress(lessonId, "", slidePanels.Length);
            if (entry == null) {
                return 0;
            }

            var lastViewed = entry.maxSlideReached;
            var totalSlidesNow = slidePanels.Length;

            var completedPreviously =
                lastViewed >= totalSlidesNow - 1 &&
                entry.totalSlides == totalSlidesNow;

            if (completedPreviously) {
                return 0;
            }

            if (lastViewed < 0) {
                return 0;
            }

            return Mathf.Clamp(lastViewed, 0, totalSlidesNow - 1);
        }

        private void ShowSlide(int index) {
            if (slidePanels == null) {
                return;
            }

            for (var i = 0; i < slidePanels.Length; i++) {
                if (slidePanels[i] != null) {
                    slidePanels[i].SetActive(i == index);
                }
            }
        }

        private void ReportProgress() {
            if (string.IsNullOrWhiteSpace(lessonId) || slidePanels == null || ProgressService.Instance == null) {
                return;
            }

            ProgressService.Instance.UpdateLessonProgress(
                lessonId,
                m_currentSlide,
                slidePanels.Length
            );
        }

        public int GetCurrentSlide() {
            return m_currentSlide;
        }

        public int GetTotalSlides() {
            return slidePanels?.Length ?? 0;
        }
        
        public string GetLessonId() {
            return lessonId;
        }
        
        public void SaveCurrentSlideOnSceneChange() {
            s_previousSlide = m_currentSlide;
        }
        
        public static void ClearSavedSlideOnSceneChange() {
            s_previousSlide = null;
        }
    }
}