using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public Animator mouseAnimator;
    public Text mouseTag;
    public GameObject Syringe;

    private bool canOut = false;
    private bool canInjection = false;
    private int mouseTagValue = 1;
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
            Syringe.SetActive(false);
            mouseAnimator.SetTrigger("MouseOut");
            canOut = false;
            mouseTagValue++;
        } 
    }

    public void CallSyringeInjection()
    {
        if (canInjection)
        {
            //mouseAnimator.SetTrigger("ResetSyringe");
            Syringe.SetActive(true);
            mouseAnimator.SetTrigger("SyringeInjection");
            canInjection = false;
        }
    }
}
