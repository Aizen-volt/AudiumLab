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

        public void OnResetProgressClicked() {
            ProgressService.Instance.ResetAll();

            if (!string.IsNullOrEmpty(mainMenuSceneName)) {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
    }
}