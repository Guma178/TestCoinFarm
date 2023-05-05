using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class DroppedTreasure : ViewTreasure
    {
        private Transform thisTransorm;
        public Transform ThisTransorm
        {
            get
            {
                if (thisTransorm == null)
                {
                    thisTransorm = this.transform;
                }

                return thisTransorm;
            }
        }

        private System.Tuple<bool, Rigidbody> body = System.Tuple.Create<bool, Rigidbody>(false, null);
        public Rigidbody Body
        {
            get
            {
                if (!body.Item1)
                {
                    body = System.Tuple.Create<bool, Rigidbody>(true, this.GetComponent<Rigidbody>());
                }

                return body.Item2;
            }
        }

        public event System.Action Picked;

        public void PickUp()
        {
            this.gameObject.SetActive(false);
            Picked?.Invoke();
        }
    }
}
