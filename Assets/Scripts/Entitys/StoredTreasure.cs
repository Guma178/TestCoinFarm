using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class StoredTreasure : ViewTreasure
    {

        private float? height;
        public float Height
        {
            get 
            {
                if (height == null)
                {
                    Quaternion rotation = ThisTransorm.rotation;
                    ThisTransorm.rotation = Quaternion.LookRotation(Vector3.up, Vector3.up);
                    height = Vector3.Project(Renderer.bounds.size, Vector3.up).magnitude;
                    ThisTransorm.rotation = rotation;
                }

                return height.Value;
            }
        }

        private Vector3 size;
        public Vector3 Size
        {
            get
            {
                return Renderer.bounds.size;
            }
        }

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


        private System.Tuple<bool, Renderer> thisRenderer = System.Tuple.Create<bool, Renderer>(false, null);
        private Renderer Renderer
        {
            get
            {
                if (!thisRenderer.Item1)
                {
                    thisRenderer = System.Tuple.Create<bool, Renderer>(true, this.GetComponent<Renderer>());
                }

                return thisRenderer.Item2;
            }
        }

        private System.Tuple<bool, Mover> mover = System.Tuple.Create<bool, Mover>(false, null);
        public Mover Mover
        {
            get
            {
                if (!mover.Item1)
                {
                    mover = System.Tuple.Create<bool, Mover>(true, this.GetComponent<Mover>());
                }

                return mover.Item2;
            }
        }
    }
}
