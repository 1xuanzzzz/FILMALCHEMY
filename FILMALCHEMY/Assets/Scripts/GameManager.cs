using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int correctCount = 0;
    public int targetCount = 5;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterCorrectPlacement()
    {
        correctCount++;
        Debug.Log("Correct objects: " + correctCount);

        if (correctCount >= targetCount)
        {
            Debug.Log("Level complete!");
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

