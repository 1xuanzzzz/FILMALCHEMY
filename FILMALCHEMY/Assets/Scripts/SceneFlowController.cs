using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFlowController : MonoBehaviour
{
    public void StartSceneTransition(GameObject objectToHide, GameObject objectToShow, float delay, string sceneName)
    {
        StartCoroutine(Transition(objectToHide, objectToShow, delay, sceneName));
    }

    private IEnumerator Transition(GameObject objectToHide, GameObject objectToShow, float delay, string sceneName)
    {
        if (objectToHide != null)
            objectToHide.SetActive(false);

        if (objectToShow != null)
            objectToShow.SetActive(true);

        Debug.Log($"⏳ 等待 {delay} 秒后切换场景：{sceneName}");
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}