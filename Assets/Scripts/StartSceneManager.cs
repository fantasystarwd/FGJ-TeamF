using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{
    public Image startText;
    public float fadeTime = 1.5f;
    Tween tween;
    public void playStartSceneAnim()
    {
        GlobalParameter.stageResult_1 = false;
        GlobalParameter.stageResult_2 = false;
        GlobalParameter.stageResult_3 = false;
        FadeOut();
    }

    public void Stop()
    {
        tween.Kill();
    }

    private void FadeOut()
    {
        tween = DOTween.To(() => startText.color, r => startText.color = r, new Color(1,1,1,0), fadeTime).SetEase(Ease.OutCubic);
        tween.onComplete += FadeIn;
    }

    private void FadeIn()
    {
        tween = DOTween.To(() => startText.color, r => startText.color = r, new Color(1, 1, 1, 1), fadeTime).SetEase(Ease.InCubic);
        tween.onComplete += FadeOut;
    }
}
