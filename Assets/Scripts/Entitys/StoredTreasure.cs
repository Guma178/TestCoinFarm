using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class StoredTreasure : ViewTreasure, IPileable
    {

        private float? height;
        public float Height
        {
            get 
            {
                if (height == null)
                {
                    //height = Vector3.Project(ThisTransorm.TransformPoint(MeshFilter.sharedMesh.bounds.max) - ThisTransorm.TransformPoint(MeshFilter.sharedMesh.bounds.min), ThisTransorm.forward).magnitude;
                    height = Vector3.Project(ThisTransorm.TransformVector(MeshFilter.sharedMesh.bounds.size), ThisTransorm.forward).magnitude;
                }

                return height.Value;
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


        private System.Tuple<bool, MeshFilter> thisMeshFilter = System.Tuple.Create<bool, MeshFilter>(false, null);
        private MeshFilter MeshFilter
        {
            get
            {
                if (!thisMeshFilter.Item1)
                {
                    thisMeshFilter = System.Tuple.Create<bool, MeshFilter>(true, this.GetComponent<MeshFilter>());
                }

                return thisMeshFilter.Item2;
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

        private void OnDrawGizmos()
        {
            //var bounds =  MeshFilter.mesh.bounds;
            //Gizmos.matrix = transform.localToWorldMatrix;
            //Gizmos.color = Color.blue;
            //Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(ThisTransorm.TransformPoint(MeshFilter.sharedMesh.bounds.min), ThisTransorm.TransformPoint(MeshFilter.sharedMesh.bounds.max));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ThisTransorm.position, ThisTransorm.position + ThisTransorm.forward);
        }
    }
}
