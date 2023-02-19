using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class BtnOnclick : MonoBehaviour
    {
        public void SceneGameStart()
        {
            SceneManager.LoadScene("SaveLoad");
        }
    
        public void SceneDictionary()
        {
            SceneManager.LoadScene("Dictionary");
        }

        public void SceneLoveStory()
        {
            SceneManager.LoadScene("Select Scene");
        }

        public void OpenOption()
        {
            SceneManager.LoadScene("Friends");
        }

        public void OpenCredit()
        {
            
        }

        public void ExitGame()
        {
            Application.Quit();
        }

    }
}
