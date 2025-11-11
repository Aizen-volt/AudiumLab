// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 11/11/2025
//  */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Lessons {
    public class LessonBackToMenu : MonoBehaviour {
        [SerializeField] private string mainMenuSceneName = "MainMenu";

        public void Back() {
            if (!string.IsNullOrEmpty(mainMenuSceneName)) {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
    }
}