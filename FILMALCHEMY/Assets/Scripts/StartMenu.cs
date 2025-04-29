using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; //ȷ������ TextMeshPro �����ռ�
using DG.Tweening;


public class StartMenu : MonoBehaviour
{
    public enum ModeTypes {LightMode,DarkMode}

    [Header("UI Elements")]
    public Image Background; //����ͼ
    public TextMeshProUGUI TitleText; //��������
    public TextMeshProUGUI TitleUnderlay; //���ֱ���
    public RectTransform ChangeModeDot;
    public Image button01;
    public Image button02;
    public Image button03;
    public Image SwitchButtonOutline;
    public Image SwitchButton;


    [Header("Light Mode Colors")]
    public Color lightModeColor;
    public Color lightTextColor = new Color(0xB8 / 255f, 0xD5 / 255f, 0xE0 / 255f); //LightMode ��ɫ #B8D5E0
    public Color lightUnderlayColor = new Color(0.02f, 0.54f, 0.75f, 1); //#048ABF

    [Header("Dark Mode Colors")]
    public Color darkModeColor;
    public Color darkUnderlayColor = new Color(0.75f, 0.02f, 0.02f, 1); //#BF0404
    public Color darkTextColor = new Color(0xF2 / 255f, 0xAC / 255f, 0xAC / 255f); //DarkMode ��ɫ #F2ACAC

    public Color middleColor;
    
    private ModeTypes myMode;

    void Start()
    {
        myMode = ModeTypes.LightMode; //���ó�ʼģʽΪ LightMode
        UpdateUI(); //����UI����

        Debug.Log("Initial Mode = " + myMode + ", Color = " + Background.color);
        myMode = ModeTypes.LightMode;
        // Background.color = lightModeColor;
        Debug.Log("my Mode = " + myMode + ", " + Background.color);
    }

    public void ChangeMode() 
    {
        Debug.Log("Button Clicked! Changing Mode...");
        myMode = (myMode == ModeTypes.LightMode) ? ModeTypes.DarkMode : ModeTypes.LightMode;

        if (myMode == ModeTypes.DarkMode) 
        {
            //StartCoroutine(ColorLerp(lightModeColor, darkModeColor, 0.5f));
            DOTween.Sequence()
                .Append(Background.DOColor(darkModeColor, 0.5f).SetEase(Ease.InOutElastic))
                .Join(TitleText.DOColor(darkTextColor, 0.5f).SetEase(Ease.InOutElastic))
                .Join(ChangeModeDot.DOLocalMoveX(790, 0.5f)) // circle pos
                .Join
                (DOTween.Sequence()
                        .Append(TitleUnderlay.DOColor(middleColor, 0.1f))
                        .Append(TitleUnderlay.DOColor(darkUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(button01.DOColor(middleColor, 0.1f))
                        .Append(button01.DOColor(darkUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(button02.DOColor(middleColor, 0.1f))
                        .Append(button02.DOColor(darkUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(button03.DOColor(middleColor, 0.1f))
                        .Append(button03.DOColor(darkUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(SwitchButtonOutline.DOColor(middleColor, 0.1f))
                        .Append(SwitchButtonOutline.DOColor(darkUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(SwitchButton.DOColor(middleColor, 0.1f))
                        .Append(SwitchButton.DOColor(darkUnderlayColor, 0.1f)))
                .WaitForCompletion();

        }

        else if (myMode == ModeTypes.LightMode)
        {
            // StartCoroutine(ColorLerp(darkModeColor, lightModeColor, 0.5f));
            DOTween.Sequence()
                .Append(Background.DOColor(lightModeColor, 0.5f).SetEase(Ease.InOutElastic))
                .Join(TitleText.DOColor(lightTextColor, 0.5f).SetEase(Ease.InOutElastic))
                .Join(ChangeModeDot.DOLocalMoveX(700, 0.5f)) // circle pos
                .Join
                (DOTween.Sequence()
                        .Append(TitleUnderlay.DOColor(middleColor, 0.1f))
                        .Append(TitleUnderlay.DOColor(lightUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(button01.DOColor(middleColor, 0.1f))
                        .Append(button01.DOColor(lightUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(button02.DOColor(middleColor, 0.1f))
                        .Append(button02.DOColor(lightUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(button03.DOColor(middleColor, 0.1f))
                        .Append(button03.DOColor(lightUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(SwitchButtonOutline.DOColor(middleColor, 0.1f))
                        .Append(SwitchButtonOutline.DOColor(lightUnderlayColor, 0.1f)))
                .Join
                (DOTween.Sequence()
                        .Append(SwitchButton.DOColor(middleColor, 0.1f))
                        .Append(SwitchButton.DOColor(lightUnderlayColor, 0.1f)))
                .WaitForCompletion();

        }

        //  UpdateUI(); //�л���ɫ
    }

    void UpdateUI()
    {
        if (myMode == ModeTypes.LightMode)
        {
            Background.color = lightModeColor;
            TitleText.color = lightTextColor;
            TitleText.fontMaterial.SetColor("_UnderlayColor", lightUnderlayColor); //���� underlay��ɫ
        }
        else 
        {
            Background.color = darkModeColor;
            TitleText.color = darkTextColor;
            TitleText.fontMaterial.SetColor("_UnderlayColor", darkUnderlayColor); //���� Underlay��ɫ
        }
        Debug.Log("Mode Switched to: " + myMode);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator ColorLerp(Color startColor, Color endColor, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            Background.color = Color.Lerp(startColor, endColor, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Background.color = endColor;
    }
}
