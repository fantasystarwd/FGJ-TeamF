using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Fungus;


public class CheckPoint2Manager : MonoBehaviour
{
    public Flowchart flowchart;

    [SerializeField]
    public enum TestTubeColor
    {
        Red = 0,
        Yellow,
        Green,
        Blue
    }

    private int counter = 0;

    private List<Color> addedColorList = new List<Color>();
    private Color[] successColorList = new Color[3] { Color.yellow, Color.green, Color.blue };

    public Image petrl_water;

    //挖腳
    public void Middle_End()
    {
        GlobalParameter.goOtherCompany = true;
    }

    public void Add_New_Color_In_Petrl(TestTubeColor tubeColor)
    {
        Debug.Log(tubeColor);
        switch (tubeColor)
        {
            case TestTubeColor.Red:
                addedColorList.Add(Color.red);
                break;
            case TestTubeColor.Yellow:
                addedColorList.Add(Color.yellow);
                break;
            case TestTubeColor.Green:
                addedColorList.Add(Color.green);
                break;
            case TestTubeColor.Blue:
                addedColorList.Add(Color.blue);
                break;

        }

        Color targetColor = Color.black;
        foreach (Color item in addedColorList)
        {
            targetColor += item;
        }
        targetColor.a = 0.5f;

        Tween tween = DOTween.To(() => petrl_water.color, r => petrl_water.color = r, targetColor, 1.5f).SetEase(Ease.OutCubic);

        counter++;
        if(counter >= successColorList.Length)
        {
            Invoke("CheckNext", 1.5f);
        }
    }

    // 製作藥物成功 or 失敗
    void CheckNext()
    {
        for(int i = 0;i < successColorList.Length; i++)
        {
            if(addedColorList[i] != successColorList[i])
            {
                // fail
                Debug.Log("fail");
                GlobalParameter.stageResult_2_fail = true;
                flowchart.ExecuteBlock("Middle End Game");
                return;
            }
        }
        //
        //success
        Debug.Log("succcess");
        GlobalParameter.stageResult_2 = true;
       flowchart.ExecuteBlock("Leave CheckPoint");
    }
}
