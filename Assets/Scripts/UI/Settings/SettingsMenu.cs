// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using Progress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Settings {
    public class SettingsMenu : MonoBehaviour {
        [SerializeField] private string mainMenuSceneName = "MainMenu";
        public static string previousScene;

        public void OnResetProgressClicked() {
            ProgressService.Instance.ResetAll();

            if (!string.IsNullOrEmpty(mainMenuSceneName)) {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }

        public void OnSettingsClicked() {
            previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        }

        public void OnReturnClicked() {
            SceneManager.LoadScene(previousScene);
        }
    }
}