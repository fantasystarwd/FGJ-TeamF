using UnityEngine;
using UnityEngine.UI;

public enum InjectionResultKey
{
    Alife,
    Dead,
    //Deform,
    //SuperDeform,
}

public class MouseManager : MonoBehaviour
{
    public Animator mouseAnimator;
    public Text mouseTag;
    public Text mouseResult;
    public Text summaryResult;
    public GameObject Syringe;
    public int eachRoundCount = 10;
    public int totalRoundCount = 2;
    public float[] ProbabilityArray = new float[] { 30, 70 };
    public float[] FinalProbabilityArray = new float[] { 100 };

    public GameObject SummaryPanel;
    public GameObject NormalSummaryBtn;
    public GameObject FinalSummaryBtn;

    public Image mouse;
    public Sprite alifeMouse;
    public Sprite deadMouse;

    private bool canOut = false;
    private bool canInjection = false;
    private int mouseTagValue = 1;
    private int roundTimes = 1;
    public CheckList checkList;

    private float afileRate = 0;

    public GameObject nextBtn;
    public GameObject injectionBtn;
    // Start is called before the first frame update
    void Start()
    {
        nextBtn.SetActive(false);
        Invoke("CallMouseIn", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallMouseIn()
    {
        if (!canOut)
        {
            injectionBtn.SetActive(true);
            mouse.sprite = alifeMouse;
            Syringe.SetActive(false);
            mouseAnimator.SetTrigger("MouseIn");
            mouseTag.text = mouseTagValue.ToString();
            canOut = true;
            canInjection = true;
        }
    }

    public void CallMouseIOut()
    {
        if (canOut && !canInjection)
        {
            //nextBtn.SetActive(true);
            
            mouseResult.text = "";
            Syringe.SetActive(false);
            nextBtn.SetActive(false);
            mouseAnimator.SetTrigger("MouseOut");
            canOut = false;
            if(mouseTagValue == eachRoundCount)
            {
                mouseTagValue = 1;
                roundTimes++;
                
                if (roundTimes == totalRoundCount)
                {
                    summaryResult.text = "目標存活率 =  "+ afileRate+"%";
                    SummaryPanel.SetActive(true);
                    NormalSummaryBtn.SetActive(false);
                    FinalSummaryBtn.SetActive(true);
                }
                else
                {
                    summaryResult.text = "目標存活率 =  " + afileRate + "%";
                    SummaryPanel.SetActive(true);
                    NormalSummaryBtn.SetActive(true);
                    FinalSummaryBtn.SetActive(false);
                }
                afileRate = 0;
            }
            else
            {
                mouseTagValue++;
            }
            Invoke("CallMouseIn", 1);
            Invoke("AfterOut", 1.5f);
        } 
    }

    private void AfterOut()
    {
        injectionBtn.SetActive(true);
    }

    public void CallSyringeInjection()
    {
        if (canInjection)
        {
            //mouseAnimator.SetTrigger("ResetSyringe");
            Syringe.SetActive(true);
            mouseAnimator.SetTrigger("SyringeInjection");
            injectionBtn.SetActive(false);
            Invoke("AfterInjection", 1);
        }
    }

    private void AfterInjection()
    {
        nextBtn.SetActive(true);
        
    }

    public void CheckResult()
    {
        canInjection = false;
        int result;
        if (roundTimes == totalRoundCount - 1)
        {
            result = (int)Choose(ProbabilityArray);
        }
        else
        {
            result = (int)Choose(FinalProbabilityArray);
        }
        
        Debug.Log("CheckResult = " + ProbabilityArray[result]);
        switch (result)
        {
            case (int)InjectionResultKey.Alife:
                mouseResult.text = "存活";
                afileRate += 10;
                Debug.Log("存活");
                checkList.SetCheck(mouseTagValue-1, true);
                break;
            case (int)InjectionResultKey.Dead:
                mouseResult.text = "死亡";
                Debug.Log("死亡");
                mouse.sprite = deadMouse;
                checkList.SetCheck(mouseTagValue-1, false);
                mouseAnimator.SetTrigger("MouseDead");
                break;
            //case (int)InjectionResultKey.Deform:
            //    mouseResult.text = "變異";
            //    Debug.Log("變異");
            //    break;
            //case (int)InjectionResultKey.SuperDeform:
            //    mouseResult.text = "超級變異";
            //    Debug.Log("超級變異");
            //    break;
            default:
                mouseResult.text = "存活";
                afileRate += 10;
                Debug.Log("存活");
                break;
        }
    }

    float Choose(float[] Probs)
    {
        //將事件元素加入到數組中，如上面有4個元素，分別爲50,25,20,5

        float total = 0;
        foreach (float elem in Probs)
        {
            total += elem;
        }

        //Random.value方法返回一個0—1的隨機數
        float randomPoint = Random.value * total;
        for (int i = 0; i < Probs.Length; i++)
        {
            if (randomPoint < Probs[i])
                return i;
            else
                randomPoint -= Probs[i];
        }
        return Probs.Length - 1;
    }

    public void Reset()
    {
        checkList.Reset();
    }
}
