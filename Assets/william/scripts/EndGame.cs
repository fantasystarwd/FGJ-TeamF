using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Fungus.Flowchart flowchart;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void check()
    {
        if (!GlobalParameter.stageResult_1 && GlobalParameter.stageResult_2 && !GlobalParameter.stageResult_3)
        {
            Debug.Log("結局A");
            flowchart.ExecuteBlock("A");
        }
        else if (!GlobalParameter.stageResult_1 && !GlobalParameter.stageResult_2 && !GlobalParameter.stageResult_3)
        {
            Debug.Log("結局C_1");
            flowchart.ExecuteBlock("C_1");
        }
        else
        {
            Debug.Log("結局C_2");
            flowchart.ExecuteBlock("C_2");
        }
    }
}
