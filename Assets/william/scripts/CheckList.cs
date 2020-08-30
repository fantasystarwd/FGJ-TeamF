using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour
{
    public List<GameObject> checkLists = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCheck(int index, bool result)
    {
        checkLists[index].SetActive(true);
        if (result)
        {
            checkLists[index].transform.localPosition = new Vector3(367, checkLists[index].transform.localPosition.y, 0);
        }
        else
        {
            checkLists[index].transform.localPosition = new Vector3(432, checkLists[index].transform.localPosition.y, 0);
        }
    }

    public void Reset()
    {
        foreach (var go in checkLists)
        {
            go.SetActive(false);
        }
    }
}
