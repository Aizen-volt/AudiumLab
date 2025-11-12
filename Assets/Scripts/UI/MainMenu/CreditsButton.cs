using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu {
    public class CreditsButton : MonoBehaviour
    {
        [SerializeField] private string authorsSceneName = "Authors";
        public void LoadAuthorsScene() {
            if (authorsSceneName is not null) {
                SceneManager.LoadScene(authorsSceneName);
            }
        }
    }
}
