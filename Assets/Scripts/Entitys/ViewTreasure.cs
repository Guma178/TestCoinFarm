using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class ViewTreasure : MonoBehaviour
    {
        [SerializeField]
        Treasure treasure;

        public Treasure Treasure => treasure;
    }
}
