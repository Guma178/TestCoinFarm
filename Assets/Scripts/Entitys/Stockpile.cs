using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class Stockpile : MonoBehaviour
    {
        [SerializeField]
        float spacing = 0.05f;

        [SerializeField]
        private List<TreasureViewPool<StoredTreasure>> treasurePools;

        private int volume;
        public int Volume { get { return volume; } }

        private Vector3 peakPointer = Vector3.zero;
        public Vector3 PeakPosition
        {
            get
            {
                return ThisTransorm.position + ThisTransorm.TransformDirection(peakPointer);
            }
        }

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
        private LinkedList<StoredTreasure> stored = new LinkedList<StoredTreasure>();

        private void Start()
        {
            peakPointer = Vector3.zero;
        }

        public void Push(Treasure treasure)
        {
            Quaternion rot;
            StoredTreasure storedTreasure;
            TreasureViewPool<StoredTreasure> pool;

            volume += treasure.Volume;

            pool = treasurePools.First(tp => tp.Treasure == treasure);

            storedTreasure = pool.Poool.Pop();
            if (storedTreasure == null)
            {
                storedTreasure = Instantiate(pool.Prefab, ThisTransorm);
            }
            storedTreasure.gameObject.SetActive(true);
            rot = Quaternion.LookRotation(ThisTransorm.up, ThisTransorm.up);
            storedTreasure.Mover.SetPosition(PeakPosition, rot);
            peakPointer += Vector3.up * storedTreasure.Height + Vector3.up * spacing;
            stored.AddFirst(storedTreasure);
        }

        public void Push(Treasure treasure, Vector3 from, ProcessState process)
        {
            StoredTreasure storedTreasure;
            TreasureViewPool<StoredTreasure> pool;
            Vector3 targetPointer;

            volume += treasure.Volume;

            pool = treasurePools.First(tp => tp.Treasure == treasure);

            storedTreasure = pool.Poool.Pop();
            if (storedTreasure == null)
            {
                storedTreasure = Instantiate(pool.Prefab);
            }

            storedTreasure.gameObject.SetActive(true);
            storedTreasure.ThisTransorm.parent = null;
            storedTreasure.Mover.SetPosition(from, storedTreasure.ThisTransorm.rotation);
            targetPointer = peakPointer;
            peakPointer += Vector3.up * storedTreasure.Height + Vector3.up * spacing;
            process.Completed += delegate () 
            {
                storedTreasure.ThisTransorm.parent = ThisTransorm;
                storedTreasure.Mover.SetPosition(ThisTransorm.position + ThisTransorm.TransformDirection(targetPointer), Quaternion.LookRotation(ThisTransorm.up, ThisTransorm.up));
            };
            storedTreasure.Mover.MoveTo(ThisTransorm.position + ThisTransorm.TransformDirection(targetPointer), process);
            stored.AddFirst(storedTreasure);
        }

        public Treasure Pop()
        {
            StoredTreasure storedTreasure;
            Treasure treasure;
            TreasureViewPool<StoredTreasure> pool;

            if (stored.First != null)
            {
                pool = treasurePools.First(tp => tp.Treasure == stored.First.Value.Treasure);

                treasure = pool.Treasure;
                volume -= treasure.Volume;
                storedTreasure = stored.First.Value;
                peakPointer -= Vector3.up * storedTreasure.Height + Vector3.up * spacing;
                pool.Poool.Push(storedTreasure);
                storedTreasure.gameObject.SetActive(false);
                stored.RemoveFirst();

                return treasure;
            }
            else
            {
                return null;
            }
        }
    }
}
