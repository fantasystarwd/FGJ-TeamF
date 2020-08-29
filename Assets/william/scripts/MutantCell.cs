using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantCell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallManagerMethod()
    {
        this.gameObject.SendMessageUpwards("CallAllCellDoDuplicate", this, SendMessageOptions.DontRequireReceiver);
    }

    public List<MutantCell> Duplicate()
    {
        Debug.Log("Duplicate");

        Transform parent = this.gameObject.transform.parent;
        List<MutantCell> cells = new List<MutantCell>();
        for (int i  = 0; i < 2; i++)
        {
            GameObject newObj = Instantiate(this.gameObject) as GameObject;
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f)+ this.gameObject.transform.position.x, Random.Range(-10.0f, 10.0f) + this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            newObj.transform.SetParent(parent);
            newObj.transform.localScale = Vector3.one;
            newObj.transform.SetPositionAndRotation(position, Quaternion.identity);
            cells.Add(newObj.GetComponent<MutantCell>());
        }

        //Destroy(this.gameObject);
        return cells;
    }
}
