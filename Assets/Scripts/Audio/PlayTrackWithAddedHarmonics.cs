// /*
//  * Copyright © 2025 AudiumLab
//  * Author: Mateusz Kaszubowski
//  * Created on: 16/11/2025
//  */

using System.Collections;
using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class PlayTrackWithAddedHarmonics : MonoBehaviour {
        
        [SerializeField] private string prefix = "Violin";
        
        [SerializeField] private string resourcesFolder = "Sound";
        
        [SerializeField] private int numberOfHarmonics = 3;

        private int counter = 0;
        private AudioSource m_source;
        
        private Coroutine m_stopCoroutine;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
            m_source.playOnAwake = false;
        }

        public void PlayNext() {
            string clipName = $"{prefix}_{counter}";
            string fullPath = $"{resourcesFolder}/{clipName}";

            if (counter >= numberOfHarmonics) {
                counter = 0;
            }
            else {
                counter++;
            }
            
            AudioClip clip = Resources.Load<AudioClip>(fullPath);

            m_source.Stop();
            m_source.clip = clip;
            m_source.Play();

            if (m_stopCoroutine != null) {
                StopCoroutine(m_stopCoroutine);
            }

            m_stopCoroutine = StartCoroutine(StopAfter(2f));
        }
        
        private IEnumerator StopAfter(float seconds) {
            yield return new WaitForSeconds(seconds);

            if (m_source.isPlaying) {
                m_source.Stop();
            }
            m_stopCoroutine = null;
        }
    }
}