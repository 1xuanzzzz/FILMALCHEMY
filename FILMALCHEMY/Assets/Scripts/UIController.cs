using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
