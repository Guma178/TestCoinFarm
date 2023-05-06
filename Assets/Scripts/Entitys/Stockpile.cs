using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TCF.Data;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace TCF.Entitys
{
    public class Stockpile : MonoBehaviour
    {
        [SerializeField]
        Pile pile;

        [SerializeField]
        private List<TreasureViewPool<StoredTreasure>> treasurePools;

        private int volume;
        public int Volume { get { return volume; } }

        public Vector3 PeakPointer
        {
            get
            {
                return pile.PeakPosition;
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

        public void Push(Treasure treasure)
        {
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
            pile.Put(storedTreasure);
            stored.AddFirst(storedTreasure);
        }

        public void Push(Treasure treasure, Vector3 from, ProcessState process)
        {
            StoredTreasure storedTreasure;
            Transform currentPeak;
            TreasureViewPool<StoredTreasure> pool;

            volume += treasure.Volume;

            pool = treasurePools.First(tp => tp.Treasure == treasure);

            storedTreasure = pool.Poool.Pop();
            if (storedTreasure == null)
            {
                storedTreasure = Instantiate(pool.Prefab);
            }

            storedTreasure.gameObject.SetActive(true);
            storedTreasure.ThisTransorm.parent = null;
            storedTreasure.ThisTransorm.position = from;
            currentPeak = pile.TakePeak();
            process.Completed += delegate () 
            {
                storedTreasure.ThisTransorm.parent = ThisTransorm;
                pile.Put(storedTreasure);
            };
            process.Finished += delegate ()
            {
                pile.BackPeak(currentPeak);
            };
            storedTreasure.Mover.MoveTo(currentPeak, process);
            stored.AddFirst(storedTreasure);
        }

        public Treasure Pop()
        {
            StoredTreasure storedTreasure;
            Treasure treasure;
            TreasureViewPool<StoredTreasure> pool;
            LinkedListNode<StoredTreasure> storedHere;

            if (stored.First != null)
            {
                pool = treasurePools.First(tp => tp.Treasure == stored.First.Value.Treasure);

                treasure = pool.Treasure;
                volume -= treasure.Volume;
                storedTreasure = pile.Take<StoredTreasure>();
                storedHere = stored.Find(storedTreasure);
                if (storedHere != null)
                {
                    pool.Poool.Push(storedTreasure);
                    stored.Remove(storedTreasure);
                    storedTreasure.gameObject.SetActive(false);
                }

                return treasure;
            }
            else
            {
                return null;
            }
        }
    }
}
