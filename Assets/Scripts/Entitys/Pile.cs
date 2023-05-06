using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using static UnityEditor.Progress;

namespace TCF.Entitys
{
    public class Pile : MonoBehaviour
    {
        [SerializeField]
        float spacing;

        private Transform thisTransorm;
        private Transform ThisTransorm
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
        private Vector3 peakPointer = Vector3.zero;
        private LinkedList<IPileable> piled = new LinkedList<IPileable>();
        private ObjectsPool<Transform> peaks = new ObjectsPool<Transform>();

        public Vector3 PeakPosition
        {
            get
            {
                return ThisTransorm.position + ThisTransorm.TransformDirection(peakPointer);
            }
        }

        public Transform TakePeak()
        {
            Transform currentPeak;
            currentPeak = peaks.Pop();
            if (currentPeak == null)
            {
                GameObject newPeak = new GameObject("SomePeack");
                currentPeak = newPeak.transform;
                currentPeak.parent = ThisTransorm;
            }
            currentPeak.position = PeakPosition;
            
            return currentPeak;
        }
        public void BackPeak(Transform transform)
        {
            peaks.Push(transform);
        }

        public void Put(IPileable item)
        {
            Quaternion rot = Quaternion.LookRotation(ThisTransorm.up, ThisTransorm.up);
            
            item.Mover.SetPosition(PeakPosition, rot);
            peakPointer += Vector3.up * item.Height + Vector3.up * spacing;
            piled.AddFirst(item);
        }

        public T Take<T>() where T : class, IPileable
        {
            LinkedListNode<IPileable> taked = null;

            taked = piled.First;
            if (taked != null && taked.Value is T ofType)
            {
                piled.RemoveFirst();
                peakPointer -= Vector3.up * taked.Value.Height + Vector3.up * spacing;
                return ofType;
            }
            else
            {
                return null;
            }
        }

        public IPileable Take()
        {
            LinkedListNode<IPileable> taked = null;

            taked = piled.First;
            if (taked != null)
            {
                piled.RemoveFirst();
                peakPointer -= taked.Value.Height * Vector3.up + Vector3.up * spacing;
                return taked.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
