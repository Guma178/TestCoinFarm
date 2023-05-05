using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class Storehouse : MonoBehaviour
    {
        [SerializeField]
        int stockpileCapacity = 52;

        [SerializeField]
        List<Stockpile> stockpiles= new List<Stockpile>();

        private int currentPileInd = 0, wealth = 0;

        public int Wealth => wealth;

        public event System.Action<int> WealthChanged;

        public void Put(Treasure treasure)
        {
            if (currentPileInd + 1 < stockpiles.Count &&
                stockpiles[currentPileInd].Volume + treasure.Volume > stockpileCapacity)
            {
                currentPileInd++;
            }
            if (stockpiles[currentPileInd].Volume + treasure.Volume < stockpileCapacity)
            {
                stockpiles[currentPileInd].Push(treasure);
                wealth += treasure.Wealth;
                WealthChanged?.Invoke(wealth);
            }
        }
    }
}
