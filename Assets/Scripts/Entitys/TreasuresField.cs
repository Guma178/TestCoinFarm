using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class TreasuresField : MonoBehaviour
    {
        [SerializeField]
        float spawnPeriod = 2f, radius = 8f;

        [SerializeField]
        int maxInGame = 5;

        [SerializeField]
        private List<TreasureProbability> treasureProbabilities;

        [SerializeField]
        private List<TreasureViewPool<DroppedTreasure>> treasurePools;

        private int ingameAmount = 0;

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

        private void Start()
        {
            StartCoroutine(Spawning());
            foreach (TreasureViewPool<DroppedTreasure> tp in treasurePools)
            {
                foreach (DroppedTreasure dt in tp.Poool)
                {
                    dt.Picked += delegate () { tp.Poool.Push(dt); ingameAmount--; };
                }
            }
        }

        private IEnumerator Spawning()
        {
            TreasureViewPool<DroppedTreasure> treasurePool;
            Treasure choosed = null;
            int typeChoose, range, side;
            float xPos, zPos;
            range = 0;
            foreach (TreasureProbability treasureProbability in treasureProbabilities)
            {
                range += treasureProbability.Probability;
            }

            while (true)
            {
                yield return new WaitForSeconds(spawnPeriod);

                if (ingameAmount < maxInGame)
                {
                    ingameAmount++;
                    typeChoose = Random.Range(0, range);
                    side = 0;
                    foreach (TreasureProbability treasureProbability in treasureProbabilities)
                    {
                        if (typeChoose >= side && typeChoose < side + treasureProbability.Probability)
                        {
                            choosed = treasureProbability.Treasure;
                            break;
                        }
                        side += treasureProbability.Probability;
                    }

                    treasurePool = treasurePools.First(tp => tp.Treasure == choosed);
                    DroppedTreasure droppedInstance = treasurePool.Poool.Pop();
                    xPos = Random.Range(0f, radius);
                    zPos = Random.Range(0f, Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Sqrt(Mathf.Pow(xPos, 2))));
                    if (Random.Range(0, 2) == 0)
                    {
                        xPos *= -1;
                    }
                    if (Random.Range(0, 2) == 0)
                    {
                        zPos *= -1;
                    }
                    if (droppedInstance == null)
                    {
                        droppedInstance = Instantiate<DroppedTreasure>(treasurePool.Prefab, (ThisTransorm.position + new Vector3(xPos, 0, zPos)), Quaternion.identity, ThisTransorm);
                        droppedInstance.Picked += delegate () { treasurePool.Poool.Push(droppedInstance); ingameAmount--; };
                    }
                    else
                    {
                        droppedInstance.ThisTransorm.position = (ThisTransorm.position + new Vector3(xPos, 0, zPos));
                        droppedInstance.gameObject.SetActive(true);
                    }
                }
            }
        }

        [System.Serializable]
        private class TreasureProbability
        {
            [SerializeField]
            private Treasure treasure;
            public Treasure Treasure => treasure;

            [SerializeField]
            private int probability;
            public int Probability => probability;
        }
    }
}
