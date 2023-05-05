using System.Collections;
using System.Collections.Generic;
using TCF.Entitys;
using UnityEngine;
using UnityEngine.UI;

namespace TCF.UI
{
    public class WealthLable : MonoBehaviour
    {
        [SerializeField]
        Storehouse storehouse;

        private string messageFormat;

        private System.Tuple<bool, Text> lable = System.Tuple.Create<bool, Text>(false, null);
        private Text Lable
        {
            get
            {
                if (!lable.Item1)
                {
                    lable = System.Tuple.Create<bool, Text>(true, this.GetComponent<Text>());
                }

                return lable.Item2;
            }
        }

        private void Start()
        {
            messageFormat = Lable.text;
            SetCounter(0);
            storehouse.WealthChanged += SetCounter;
        }

        private void SetCounter(int amount)
        {
            Lable.text = string.Format(messageFormat, amount);
        }
    }
}
