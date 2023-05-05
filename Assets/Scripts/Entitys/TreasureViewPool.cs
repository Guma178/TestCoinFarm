using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    [System.Serializable]
    public class TreasureViewPool<T> where T : ViewTreasure
    {
        [SerializeField]
        private Treasure treasure;
        public Treasure Treasure => treasure;

        [SerializeField]
        private T prefab;
        public T Prefab => prefab;

        [SerializeField]
        private List<T> initial;

        private ObjectsPool<T> pool;
        public ObjectsPool<T> Poool
        {
            get
            {
                if (pool == null)
                {
                    pool = new ObjectsPool<T>(initial);
                }

                return pool;
            }
        }
    }
}
