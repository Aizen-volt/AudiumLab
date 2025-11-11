// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 08/11/2025
//  */

using UnityEngine;

namespace Lessons {
    public class LessonCatalog : MonoBehaviour {
        [SerializeField]
        private LessonDefinition[] lessons;

        public LessonDefinition[] Lessons => lessons;
    }
}