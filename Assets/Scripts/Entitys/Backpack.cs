using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField]
        int capacity;

        private System.Tuple<bool, Stockpile> pile = System.Tuple.Create<bool, Stockpile>(false, null);
        private Stockpile Pile
        {
            get
            {
                if (!pile.Item1)
                {
                    pile = System.Tuple.Create<bool, Stockpile>(true, this.GetComponent<Stockpile>());
                }

                return pile.Item2;
            }
        }

        public Vector3 PilePeak { get { return Pile.PeakPointer; } } 
            

        public bool Put(Treasure treasure)
        {
            if (Pile.Volume + treasure.Volume <= capacity)
            {
                Pile.Push(treasure);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Treasure Take()
        {
            return Pile.Pop();
        }
    }
}
