// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using Lessons;
using Progress;
using UnityEngine;

namespace UI.MainMenu {
    public class MainMenuController : MonoBehaviour {
        [SerializeField] private LessonCatalog catalog;
        [SerializeField] private Transform lessonsParent;
        [SerializeField] private LessonCardView lessonCardPrefab;

        private void Start() {
            if (catalog == null || lessonsParent == null || lessonCardPrefab == null) {
                Debug.LogWarning("MainMenuController: Missing references.");
                return;
            }

            foreach (var lesson in catalog.Lessons) {
                var card = Instantiate(lessonCardPrefab, lessonsParent);
                ProgressService.Instance.GetLessonProgress(lesson.id, lesson.displayName); // Creates progress entry if not exists
                ProgressService.Instance.Save();
                card.Setup(lesson);
            }
        }
    }
}