using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace TCF.Data
{
    [CreateAssetMenu(fileName = "Treasure", menuName = "TCF/Data/Treasure")]
    public class Treasure : ScriptableObject
    {
        [SerializeField]
        int volume;
        public int Volume => volume;

        [SerializeField]
        int wealth;
        public int Wealth => wealth;
    }
}
