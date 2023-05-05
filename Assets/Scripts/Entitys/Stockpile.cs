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
        float spacing;

        [SerializeField]
        private List<TreasureViewPool<StoredTreasure>> treasurePools;

        private int volume;
        public int Volume { get { return volume; } }

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
        private Vector3 spacePointer;
        private LinkedList<StockedTreasure> stored = new LinkedList<StockedTreasure>();


        private void Start()
        {
            spacePointer = Vector3.zero;
        }

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
            storedTreasure.ThisTransorm.position = ThisTransorm.position + ThisTransorm.TransformDirection(spacePointer);
            spacePointer = spacePointer + (new Vector3(0, spacing + pool.Prefab.Size.y, 0));
            stored.AddFirst(new StockedTreasure { Stored = storedTreasure, Treasure = treasure });
        }

        public Treasure Pop()
        {
            Treasure treasure;
            TreasureViewPool<StoredTreasure> pool;

            if (stored.First != null)
            {
                pool = treasurePools.First(tp => tp.Treasure == stored.First.Value.Treasure);

                treasure = stored.First.Value.Treasure;
                volume -= treasure.Volume;
                spacePointer = spacePointer - (new Vector3(0, spacing + pool.Prefab.Size.y, 0));
                stored.First.Value.Stored.gameObject.SetActive(false);
                pool.Poool.Push(stored.First.Value.Stored);
                stored.RemoveFirst();

                return treasure;
            }
            else
            {
                return null;
            }
        }

        private class StockedTreasure
        {
            public Treasure Treasure { get; set; }
            public StoredTreasure Stored { get; set; }
        }
    }
}
