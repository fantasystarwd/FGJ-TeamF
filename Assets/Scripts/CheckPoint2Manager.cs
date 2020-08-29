using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint2Manager : MonoBehaviour
{
    public Text mentionText;
    public Image resultImage;
    public Button buttonYes, buttonNo;

    public string[] mentionTextList;
    public bool[] mentionTextResult;
    public int[] mentionTextPoint;

    public int resultBestPoint = 80;

    public Sprite[] resultImageChoose = new Sprite[2]; 

    private int mentionTextCounter = -1;
    private int checkPoint = 0;

    public void activateButton(bool isActivate)
    {
        buttonYes.gameObject.SetActive(isActivate);
        buttonNo.gameObject.SetActive(isActivate);
    }

    public bool updateMentionText()
    {
        mentionTextCounter++;
        if(mentionTextCounter >= mentionTextList.Length)
        {
            return false;
        }
        else
        {
            showTextAnim(mentionTextCounter);
            return true;
        }
    }

    public void click_check_button(bool result)
    {
        if(result == mentionTextResult[mentionTextCounter])
        {
            checkPoint += mentionTextPoint[mentionTextCounter];
        }
        else
        {

        }
    }

    public void Show_Result()
    {
        if(checkPoint >= resultBestPoint)
        {
            showResultAnim(0);
        }
        else
        {
            showResultAnim(1);
        }
    }

    private void showTextAnim(int counter)
    {
        mentionText.text = mentionTextList[mentionTextCounter];
    }

    private void showResultAnim(int result)
    {
        resultImage.sprite = resultImageChoose[result];
    }
    
}
