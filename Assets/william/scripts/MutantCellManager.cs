using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantCellManager : MonoBehaviour
{
    public List<MutantCell> cells = new List<MutantCell>();
    public GameObject cellPrefab;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AllCellDoDuplicate", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CallAllCellDoDuplicate(MutantCell owner)
    {
        Debug.Log("CallAllCellDoDuplicate");
        AllCellDoDuplicate();
    }

    void AllCellDoDuplicate()
    {
        List<MutantCell> _cells = new List<MutantCell>();
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i] != null)
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject newObj = Instantiate(cellPrefab) as GameObject;
                    Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f) + cells[i].transform.position.x, Random.Range(-10.0f, 10.0f) + cells[i].transform.position.y, cells[i].transform.position.z);
                    newObj.transform.SetParent(parent);
                    newObj.transform.localScale = Vector3.one;
                    newObj.transform.SetPositionAndRotation(position, Quaternion.identity);
                    _cells.Add(newObj.GetComponent<MutantCell>());
                }
                Destroy(cells[i].gameObject);
            }
        }
        for (int i = 0; i < _cells.Count; i++)
        {
            cells.Add(_cells[i]);
        }
    }

    void PrintMessage(object[] obj)
    {
        Debug.Log("PrintMessage");
    }
}
