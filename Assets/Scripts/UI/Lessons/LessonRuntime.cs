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

        private void Start() {
            m_currentSlide = 0;
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
            if (string.IsNullOrWhiteSpace(lessonId) || slidePanels == null) {
                return;
            }

            ProgressService.Instance.UpdateLessonProgress(
                lessonId,
                m_currentSlide,
                slidePanels.Length
            );
        }
    }
}