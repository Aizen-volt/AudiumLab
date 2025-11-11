// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using System;
using System.Collections.Generic;

namespace Progress {
    [Serializable]
    public class LessonProgressEntry {
        public string lessonId;
        public int maxSlideReached;
        public int totalSlides;
    }

    [Serializable]
    public class ProgressData {
        public List<LessonProgressEntry> lessons = new();
    }
}