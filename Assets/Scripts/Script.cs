using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using TCF.Entitys;
using UnityEngine;

public class Script : MonoBehaviour
{
    [SerializeField]
    Backpack backpack;

    [SerializeField]
    Transform start;

    [SerializeField]
    Treasure treasure;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.M))
        {
            backpack.Put(treasure, start.position, new TCF.ProcessState());
        }
    }
}
