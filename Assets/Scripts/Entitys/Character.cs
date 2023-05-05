using System.Collections;
using System.Collections.Generic;
using TCF.Data;
using UnityEngine;

namespace TCF.Entitys
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private Backpack backpack;

        private Coroutine unloading;

        private System.Tuple<bool, Animator> animator = System.Tuple.Create<bool, Animator>(false, null);
        private Animator Animator
        {
            get
            {
                if (!animator.Item1)
                {
                    animator = System.Tuple.Create<bool, Animator>(true, this.GetComponent<Animator>());
                }

                return animator.Item2;
            }
        }

        private System.Tuple<bool, Mover> mover = System.Tuple.Create<bool, Mover>(false, null);
        private Mover Mover
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

        public void BeginWalk()
        {
            Animator.SetBool("Walk", true);
        }
        public void EndWalk()
        {
            Animator.SetBool("Walk", false);
        }

        public void Walk(Vector3 direction)
        {
            Mover.Move(direction);
        }

        private void OnTriggerEnter(Collider other)
        {
            DroppedTreasure droppedTreasure;
            Storehouse storehouse;

            droppedTreasure = other.GetComponent<DroppedTreasure>();
            if (droppedTreasure != null)
            {
                if (backpack.Put(droppedTreasure.Treasure, droppedTreasure.ThisTransorm.position, new ProcessState()))
                {
                    droppedTreasure.PickUp();
                }
            }
            else
            {
                storehouse = other.GetComponent<Storehouse>();
                if (storehouse != null)
                {
                    unloading = StartCoroutine(Unload(storehouse));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Storehouse storehouse;

            storehouse = other.GetComponent<Storehouse>();
            if (storehouse != null && unloading != null)
            {
                StopCoroutine(unloading);
            }
        }

        private IEnumerator Unload(Storehouse storehouse)
        {
            Treasure treasure;

            treasure = backpack.Take();
            while (treasure != null) 
            {
                storehouse.Put(treasure, backpack.PilePeak, new ProcessState());
                yield return new WaitForSeconds(0.25f);
                treasure = backpack.Take();
            }
        }
    }
}
