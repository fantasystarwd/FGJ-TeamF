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

    public void Middle_End()
    {
        Debug.Log("Moddle End");
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

    void CheckNext()
    {
        for(int i = 0;i < successColorList.Length; i++)
        {
            if(addedColorList[i] != successColorList[i])
            {
                // fail
                Debug.Log("fail");
                flowchart.ExecuteBlock("Leave CheckPoint");
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
