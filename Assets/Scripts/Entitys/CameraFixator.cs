using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCF.Entitys
{
    public class CameraFixator : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        Vector3 distance;

        private Transform cameraTransorm;
        private Transform CameraTransorm
        {
            get
            {
                if (cameraTransorm == null)
                {
                    cameraTransorm = this.transform;
                }

                return cameraTransorm;
            }
        }

        public void SetUp()
        {
            distance = CameraTransorm.position - target.position;
        }

        private void Start()
        {
            SetUp();
        }

        private void LateUpdate()
        {
            CameraTransorm.position = target.position + distance;
        }
    }
}
