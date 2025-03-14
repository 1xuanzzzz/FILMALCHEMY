using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationTrigger : MonoBehaviour, IPointerExitHandler
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // 获取 Animator 组件
        animator.ResetTrigger("PlayAnimation"); // 确保动画开始时不会触发
    }

    public void PlayButtonAnimation()
    {
        animator.SetTrigger("PlayAnimation"); // 触发 Animator 的 PlayAnimation
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.ResetTrigger("PlayAnimation"); //清除触发器
        animator.Play("Idle"); //强制回到Idle状态
    }
}
