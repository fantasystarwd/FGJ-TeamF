using UnityEngine;
using UnityEngine.UI;

public enum InjectionResultKey
{
    Alife,
    Dead,
    Deform,
    SuperDeform,
}

public class MouseManager : MonoBehaviour
{
    public Animator mouseAnimator;
    public Text mouseTag;
    public Text mouseResult;
    public Text survivalRateText;
    public GameObject Syringe;
    public int roundCount = 10;
    public float[] ProbabilityArray = new float[] { 50, 25, 20, 5 };

    public GameObject SummaryPanel;

    private bool canOut = false;
    private bool canInjection = false;
    private int mouseTagValue = 1;

    private float survivalRate = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallMouseIn()
    {
        if (!canOut)
        {
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
            mouseResult.text = "";
            Syringe.SetActive(false);
            mouseAnimator.SetTrigger("MouseOut");
            canOut = false;
            if(mouseTagValue == roundCount)
            {
                mouseTagValue = 1;
                Debug.Log("存活率 = " + survivalRate);
                survivalRateText.text = "目標存活率 : "+ survivalRate + "%";
                SummaryPanel.SetActive(true);
            }
            else
            {
                mouseTagValue++;
            }
        } 
    }

    public void CallSyringeInjection()
    {
        if (canInjection)
        {
            //mouseAnimator.SetTrigger("ResetSyringe");
            Syringe.SetActive(true);
            mouseAnimator.SetTrigger("SyringeInjection");
        }
    }

    public void CheckResult()
    {
        canInjection = false;
        int result = (int)Choose(ProbabilityArray);
        Debug.Log("CheckResult = " + ProbabilityArray[result]);
        switch (result)
        {
            case (int)InjectionResultKey.Alife:
                mouseResult.text = "存活";
                survivalRate += 10;
                Debug.Log("存活");
                break;
            case (int)InjectionResultKey.Dead:
                mouseResult.text = "死亡";
                Debug.Log("死亡");
                break;
            case (int)InjectionResultKey.Deform:
                mouseResult.text = "變異";
                Debug.Log("變異");
                break;
            case (int)InjectionResultKey.SuperDeform:
                mouseResult.text = "超級變異";
                Debug.Log("超級變異");
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
}
