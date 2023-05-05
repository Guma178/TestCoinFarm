using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TCF.Entitys
{
    public class Input : MonoBehaviour
    {
        [SerializeField]
        Transform cameraTransform;

        [SerializeField]
        Character controlled;

        [SerializeField]
        UI.Joystick moveInput;

        private bool inMove = false;

        private void Start()
        {
            moveInput.Begin += MoveStart;
            moveInput.End += MoveEnd;
        }

        private void MoveStart()
        {
            controlled.BeginWalk();
            inMove = true;
            StartCoroutine(Moving());
        }
        private void MoveEnd()
        {
            controlled.EndWalk();
            inMove = false;
        }

        private IEnumerator Moving()
        {
            while (inMove)
            {
                controlled.Walk(new Vector3(moveInput.Value.x, 0, moveInput.Value.y));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
