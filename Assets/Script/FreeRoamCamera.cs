using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AlexzanderCowell
{


    public class FreeRoamCamera : MonoBehaviour
    {

        [Header("Cameras")]
        [SerializeField] GameObject playerCamera;
        [SerializeField] GameObject freeRoamCamera;

        private float cameraSpeed;
        private float cameraTurnSpeed = 2f;
        private bool freeRoamCameraOnOrOff;
        private bool playerCameraOnOrOff;
        private float yaw = 0.0f;
        private float pitch = 0.0f;
        private float moveMousex;
        private float moveMousey;
        private bool switchControls;

        [Header("Mouse Speed")]
        [SerializeField] private float horizontalSpeed = 2.0f;
        [SerializeField] private float verticalSpeed = 2.0f;

        public static event Action<bool> _UsingFreeRoamCameraInstead;


        private void Start()
        {
            switchControls = false;
            freeRoamCameraOnOrOff = false;
            playerCameraOnOrOff = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;
            freeRoamCamera.GetComponent<AudioListener>().enabled = false;
        }

        void Update()
        {
            SwitchCameraEvent();

            moveMousex = +horizontalSpeed * Input.GetAxis("Mouse X");
            moveMousey = horizontalSpeed * Input.GetAxis("Mouse X");
            yaw += horizontalSpeed * Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

            freeRoamCamera.transform.Translate(0, 0, cameraSpeed * Time.deltaTime, Space.Self);

            if (freeRoamCameraOnOrOff == true)
            {
                playerCamera.SetActive(false);
                freeRoamCamera.SetActive(true);
                playerCamera.GetComponent<AudioListener>().enabled = false;
                freeRoamCamera.GetComponent<AudioListener>().enabled = true;

                if(switchControls == true)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        cameraSpeed = +20;
                    }
                    if (Input.GetKeyUp(KeyCode.W))
                    {
                        cameraSpeed = 0;
                    }


                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        cameraSpeed -= 20;
                    }

                    if (Input.GetKeyUp(KeyCode.S))
                    {
                        cameraSpeed = 0;
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        freeRoamCamera.transform.Rotate(0f, -cameraTurnSpeed, 0f);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        freeRoamCamera.transform.Rotate(0f, +cameraTurnSpeed, 0f, Space.Self);
                    }
                }
             
                freeRoamCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (freeRoamCamera.GetComponent<Camera>().fieldOfView >= 1)
                    {
                        freeRoamCamera.GetComponent<Camera>().fieldOfView -= 4;
                    }
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (freeRoamCamera.GetComponent<Camera>().fieldOfView <= 100)
                    {
                        freeRoamCamera.GetComponent<Camera>().fieldOfView += 4;
                    }
                }
            }
                

            if (playerCameraOnOrOff == true)
            {
                playerCamera.SetActive(true);
                freeRoamCamera.SetActive(false);
                playerCamera.GetComponent<AudioListener>().enabled = true;
                freeRoamCamera.GetComponent<AudioListener>().enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                freeRoamCameraOnOrOff = true;
                playerCameraOnOrOff = false;
                switchControls = true;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                freeRoamCameraOnOrOff = false;
                playerCameraOnOrOff = true;
                switchControls = false;
            }

        }

        private void SwitchCameraEvent()
        {
            if (_UsingFreeRoamCameraInstead != null)
            {
                _UsingFreeRoamCameraInstead(switchControls);
            }
        }
    }
}
