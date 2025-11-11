// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using Lessons;
using Progress;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu {
    public class LessonCardView : MonoBehaviour {
        [Header("UI")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image iconImage;
        [SerializeField] private Button openButton;
        [SerializeField] private Image progressFillImage;

        private LessonDefinition m_definition;

        public void Setup(LessonDefinition definition) {
            m_definition = definition;

            if (titleText != null) {
                titleText.text = definition.displayName;
            }

            if (iconImage != null) {
                iconImage.sprite = definition.icon;
            }

            if (openButton != null) {
                openButton.onClick.RemoveAllListeners();
                openButton.onClick.AddListener(OpenLesson);
            }
            
            UpdateProgressUI();
        }
        
        private void OnEnable() {
            if (m_definition != null) {
                UpdateProgressUI();
            }
        }
        
        private void UpdateProgressUI() {
            if (m_definition == null) {
                return;
            }

            if (ProgressService.Instance == null) {
                return;
            }

            var percent = ProgressService.Instance
                .GetLessonCompletionPercent(m_definition.id, m_definition.totalSlides);

            if (progressFillImage != null) {
                progressFillImage.fillAmount = percent / 100f;
            }
        }

        private void OpenLesson() {
            if (!string.IsNullOrEmpty(m_definition.sceneName)) {
                SceneManager.LoadScene(m_definition.sceneName);
            }
        }
    }
}