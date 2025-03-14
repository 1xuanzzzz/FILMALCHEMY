using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationTrigger : MonoBehaviour, IPointerExitHandler
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // ��ȡ Animator ���
        animator.ResetTrigger("PlayAnimation"); // ȷ��������ʼʱ���ᴥ��
    }

    public void PlayButtonAnimation()
    {
        animator.SetTrigger("PlayAnimation"); // ���� Animator �� PlayAnimation
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.ResetTrigger("PlayAnimation"); //���������
        animator.Play("Idle"); //ǿ�ƻص�Idle״̬
    }
}
