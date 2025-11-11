// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using UnityEngine;

namespace Lessons {
    [System.Serializable]
    public class LessonDefinition {
        public string id;
        public string displayName;
        public string description;
        public Sprite icon;
        public string sceneName;
        public int totalSlides;
    }
}