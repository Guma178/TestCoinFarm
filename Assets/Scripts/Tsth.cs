using System.Collections;
using System.Collections.Generic;
using TCF.Entitys;
using UnityEngine;

public class Tsth : MonoBehaviour
{
    [SerializeField]
    StoredTreasure o1, o2, o3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("o1 " + o1.Height);
        Debug.Log("o2 " + o2.Height);
        Debug.Log("o3 " + o3.Height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
