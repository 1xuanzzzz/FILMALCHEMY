using UnityEngine;

public class TakeoutController : MonoBehaviour
{

    public FinalDragOutTrigger mydragoutcontroller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeoutFinish()
    {
        Debug.Log("Take Out Finish");
        StartCoroutine(mydragoutcontroller.HandleSuccess());
    }
}
