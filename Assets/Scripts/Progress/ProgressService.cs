// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using System.IO;
using UnityEngine;

namespace Progress {
    public class ProgressService : MonoBehaviour {
        public static ProgressService Instance { get; private set; }

        private ProgressData m_data = new();
        private string m_savePath = string.Empty;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Debug.Log("hiiiiiiiiii");
            DontDestroyOnLoad(gameObject);

            m_savePath = Path.Combine(Application.persistentDataPath, "progress.json");
            Load();
        }

        private void Load() {
            if (!File.Exists(m_savePath)) {
                m_data = new ProgressData();
                return;
            }

            try {
                var json = File.ReadAllText(m_savePath);
                m_data = JsonUtility.FromJson<ProgressData>(json) ?? new ProgressData();
            } catch {
                m_data = new ProgressData();
            }
        }

        public void Save() {
            try {
                var json = JsonUtility.ToJson(m_data, true);
                File.WriteAllText(m_savePath, json);
            } catch {
                Debug.LogError($"ProgressService: Failed to save progress to {m_savePath}");
            }
        }

        public LessonProgressEntry GetLessonProgress(string lessonId, string lessonName = "", int totalSlidesFallback = 1) {
            var entry = m_data.lessons.Find(l => l.lessonId == lessonId);
            if (entry == null) {
                entry = new LessonProgressEntry {
                    lessonId = lessonId,
                    lessonName = lessonName,
                    maxSlideReached = -1,
                    totalSlides = totalSlidesFallback
                };

                m_data.lessons.Add(entry);
            }

            return entry;
        }

        public void UpdateLessonProgress(string lessonId, int slideIndex, int totalSlides) {
            var entry = GetLessonProgress(lessonId, "", totalSlides);

            entry.totalSlides = totalSlides;
            if (slideIndex > entry.maxSlideReached) {
                entry.maxSlideReached = slideIndex;
                Save();
            }
        }

        public float GetLessonCompletionPercent(string lessonId, int totalSlidesFallback = 1) {
            var entry = GetLessonProgress(lessonId, "", totalSlidesFallback);
            if (entry.totalSlides <= 0) {
                return 0f;
            }

            var completedSlides = Mathf.Clamp(entry.maxSlideReached + 1, 0, entry.totalSlides);
            return (float)completedSlides / entry.totalSlides * 100f;
        }

        public void ResetLesson(string lessonId) {
            m_data.lessons.RemoveAll(l => l.lessonId == lessonId);
            Save();
        }

        public void ResetAll() {
            m_data = new ProgressData();
            Save();
        }
    }
}