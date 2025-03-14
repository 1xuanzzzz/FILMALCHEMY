using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; //确保导入 TextMeshPro 命名空间
using DG.Tweening;


public class StartMenu : MonoBehaviour
{
    public enum ModeTypes {LightMode,DarkMode}

    [Header("UI Elements")]
    public Image Background; //背景图
    public TextMeshProUGUI TitleText; //标题文字
    public TextMeshProUGUI TitleUnderlay; //文字背景
    public RectTransform ChangeModeDot;

    [Header("Light Mode Colors")]
    public Color lightModeColor;
    public Color lightTextColor = new Color(0xB8 / 255f, 0xD5 / 255f, 0xE0 / 255f); //LightMode 颜色 #B8D5E0
    public Color lightUnderlayColor = new Color(0.02f, 0.54f, 0.75f, 1); //#048ABF

    [Header("Dark Mode Colors")]
    public Color darkModeColor;
    public Color darkUnderlayColor = new Color(0.75f, 0.02f, 0.02f, 1); //#BF0404
    public Color darkTextColor = new Color(0xF2 / 255f, 0xAC / 255f, 0xAC / 255f); //DarkMode 颜色 #F2ACAC
    
    private ModeTypes myMode;

    void Start()
    {
        myMode = ModeTypes.LightMode; //设置初始模式为 LightMode
        UpdateUI(); //更新UI界面

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
                .Join(ChangeModeDot.DOLocalMoveX(750, 0.5f))
                .Append(TitleUnderlay.DOColor(darkUnderlayColor, 0.1f))
              
                .WaitForCompletion();

        }

        else if (myMode == ModeTypes.LightMode)
        {
            // StartCoroutine(ColorLerp(darkModeColor, lightModeColor, 0.5f));
            DOTween.Sequence()
                .Append(Background.DOColor(lightModeColor, 0.5f).SetEase(Ease.InOutElastic))
                .Join(TitleText.DOColor(lightTextColor, 0.5f).SetEase(Ease.InOutElastic))
                .Join(ChangeModeDot.DOLocalMoveX(700, 0.5f))
                .Append(TitleUnderlay.DOColor(lightUnderlayColor, 0.1f))
                .WaitForCompletion();

        }

        //  UpdateUI(); //切换颜色
    }

    void UpdateUI()
    {
        if (myMode == ModeTypes.LightMode)
        {
            Background.color = lightModeColor;
            TitleText.color = lightTextColor;
            TitleText.fontMaterial.SetColor("_UnderlayColor", lightUnderlayColor); //设置 underlay颜色
        }
        else 
        {
            Background.color = darkModeColor;
            TitleText.color = darkTextColor;
            TitleText.fontMaterial.SetColor("_UnderlayColor", darkUnderlayColor); //设置 Underlay颜色
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
