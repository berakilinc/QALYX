using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneStarter : MonoBehaviour
{
    public string loadSceneIndex = "0";
    public void MainMenuSceneStarterButton()
    {
        SceneManager.LoadScene(0);
    }
}
