// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using Progress;
using UI.Lessons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Settings {
    public class SettingsMenu : MonoBehaviour {
        [SerializeField] private string mainMenuSceneName = "MainMenu";
        [SerializeField] private LessonRuntime lessonRuntime;
        public static string previousScene;

        private void Start() {
            lessonRuntime = FindFirstObjectByType<LessonRuntime>();
        }

        public void OnResetProgressClicked() {
            ProgressService.Instance.ResetAll();

            if (!string.IsNullOrEmpty(mainMenuSceneName)) {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }

        public void OnSettingsClicked() {
            previousScene = SceneManager.GetActiveScene().name;
            if (lessonRuntime != null) {
                lessonRuntime.SaveCurrentSlideOnSceneChange();
            }
            SceneManager.LoadScene("Settings");
        }

        public void OnReturnClicked() {
            SceneManager.LoadScene(previousScene);
        }
    }
}