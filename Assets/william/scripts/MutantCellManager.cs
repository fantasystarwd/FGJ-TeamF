using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantCellManager : MonoBehaviour
{
    public List<MutantCell> cells = new List<MutantCell>();
    public GameObject cellPrefab;
    public Transform parent;
    public int TotalTime = 10;
    public Text countdownText;
    public Text resultText;

    public Fungus.Flowchart flowchart;
    public Fungus.Flowchart talkFlowchart;

    private CooldownTimer timer;
    private float timer_float;

    private int totalSec;
    private int sec;
    private int min;
    private int hr;

    private int touchCount = 0;
    public int touchLine = 5;

    private int lastMutantCellCount;
    // Start is called before the first frame update
    void Start()
    {
        timer = new CooldownTimer(TotalTime);
        timer.TimerCompleteEvent += Stop;
        timer.TimerCompleteEvent += ShowResult;
        timer.Start();
        InvokeRepeating("AllCellDoDuplicate", 2, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update(Time.deltaTime);
        calculateAndShowNowTime();
    }

    void CallAllCellDoDuplicate(MutantCell owner)
    {
        touchCount++;
        if(touchCount > touchLine)
        {
            talkFlowchart.ExecuteBlock("touchTip");
            touchCount = 0;
        }
        Stop();
        
        GameObject newObj = Instantiate(cellPrefab) as GameObject;
        newObj.transform.SetParent(parent);
        newObj.transform.localScale = Vector3.one;
        newObj.transform.SetPositionAndRotation(parent.transform.position, Quaternion.identity);
        cells.Add(newObj.GetComponent<MutantCell>());
        InvokeRepeating("AllCellDoDuplicate", 2, 1.5f);
    }

    void Stop()
    {
        CancelInvoke("AllCellDoDuplicate");
        int sum = 0;
        foreach (var go in cells)
        {
            if (go != null)
            {
                sum++;
                Destroy(go.gameObject);
            }

        }
        cells.Clear();
        Debug.Log("總數 = " + sum);
        lastMutantCellCount = sum;
    }

    void ShowResult()
    {
        flowchart.ExecuteBlock("Show Result");
    }

    public void ShowResultText()
    {
        resultText.text = "總數 = " + lastMutantCellCount;
    }

    void AllCellDoDuplicate()
    {
        List<MutantCell> _cells = new List<MutantCell>();
        if ((cells == null ? 0 : cells.Count) > (Math.Pow(2, 10))) {
            Debug.Log("到上限");
            return; }
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i] != null)
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject newObj = Instantiate(cellPrefab) as GameObject;
                    Vector3 position = new Vector3(UnityEngine.Random.Range(-20.0f, 20.0f) + cells[i].transform.position.x, UnityEngine.Random.Range(-20.0f, 20.0f) + cells[i].transform.position.y, cells[i].transform.position.z);
                    newObj.transform.SetParent(parent);
                    newObj.transform.localScale = Vector3.one;
                    newObj.transform.SetPositionAndRotation(position, Quaternion.identity);
                    _cells.Add(newObj.GetComponent<MutantCell>());
                }
                //cells.Remove(cells[i]);
                Destroy(cells[i].gameObject);
            }
            else
            {
                
            }
        }
        for (int i = 0; i < _cells.Count; i++)
        {
            cells.Add(_cells[i]);
        }
    }

    private void calculateAndShowNowTime()
    {
        if (TotalTime <= 0)
            return;
        timer_float = Time.deltaTime + timer_float;

        if (timer_float >= 1)
        {
            timer_float = 0;

            TotalTime--;
        }

        sec = TotalTime % 60;

        min = (TotalTime / 60) % 60;

        hr = TotalTime / 3600;


        TimeSpan interval = new TimeSpan(hr, min, sec);

        string timeStr = interval.ToString(@"hh\:mm\:ss");

        countdownText.text = timeStr;

    }
}
