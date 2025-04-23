using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Collider2D))]
public class JumpToScene : MonoBehaviour
{
    public int sceneIndex;
    private void OnMouseUp()
    {
       SceneManager.LoadScene (sceneIndex);
    }
}
