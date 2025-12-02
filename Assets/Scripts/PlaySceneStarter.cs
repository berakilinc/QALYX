using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneStarter : MonoBehaviour
{
    public string loadSceneIndex = "1";
    public void PlaySceneStarterButton()
    {
        SceneManager.LoadScene(1);
    }
}
