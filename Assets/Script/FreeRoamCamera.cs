using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AlexzanderCowell
{


    public class FreeRoamCamera : MonoBehaviour
    {

        [Header("Cameras")]
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private GameObject freeRoamCamera;
        [SerializeField] private GameObject turentCam1;
        [SerializeField] private GameObject turentCam2;


        private int selectionCamera;
        private float cameraSpeed;
        private float cameraTurnSpeed = 2f;
        private bool freeRoamCameraOnOrOff;
        private bool playerCameraOnOrOff;
        private bool turentCam02OnOrOff;
        private bool turentCam01OnOrOff;
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
            selectionCamera = 0;
            switchControls = false;
            freeRoamCameraOnOrOff = false;
            playerCameraOnOrOff = false;
            turentCam02OnOrOff = false;
            turentCam01OnOrOff = false;
            playerCamera.GetComponent<AudioListener>().enabled = false;
            freeRoamCamera.GetComponent<AudioListener>().enabled = false;
            turentCam1.GetComponent<AudioListener>().enabled = false;
            turentCam2.GetComponent<AudioListener>().enabled = false;
        }

        void Update()
        {

            SwitchCameraEvent();


            if ( selectionCamera == 0)
            {
                playerCameraOnOrOff = true;
                freeRoamCameraOnOrOff = false;
                turentCam01OnOrOff = false;
                turentCam02OnOrOff = false;
                switchControls = false;
            }

            if (selectionCamera == 1)
            {
                playerCameraOnOrOff = false;
                freeRoamCameraOnOrOff = true;
                turentCam01OnOrOff = false;
                turentCam02OnOrOff = false;
                switchControls = true;
            }

            if (selectionCamera == 2)
            {
                playerCameraOnOrOff = false;
                freeRoamCameraOnOrOff = false;
                turentCam01OnOrOff = true;
                turentCam02OnOrOff = false;
                switchControls = true;
            }

            if (selectionCamera == 3)
            {
                playerCameraOnOrOff = false;
                freeRoamCameraOnOrOff = false;
                turentCam01OnOrOff = false;
                turentCam02OnOrOff = true;
                switchControls = true;
            }

            if (selectionCamera == 4)
            {
                selectionCamera = 0;
            }



            moveMousex = +horizontalSpeed * Input.GetAxis("Mouse X");
            moveMousey = horizontalSpeed * Input.GetAxis("Mouse X");
            yaw += horizontalSpeed * Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

            


            if (turentCam01OnOrOff == true)
            {                              
                    playerCamera.SetActive(false);
                    freeRoamCamera.SetActive(false);
                    turentCam1.SetActive(true);
                    turentCam2.SetActive(false);
                    playerCamera.GetComponent<AudioListener>().enabled = false;
                    freeRoamCamera.GetComponent<AudioListener>().enabled = false;
                    turentCam1.GetComponent<AudioListener>().enabled = true;
                    turentCam2.GetComponent<AudioListener>().enabled = false;

                turentCam1.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (turentCam1.GetComponent<Camera>().fieldOfView >= 1)
                    {
                        turentCam1.GetComponent<Camera>().fieldOfView -= 4;
                    }
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (turentCam1.GetComponent<Camera>().fieldOfView <= 100)
                    {
                        turentCam1.GetComponent<Camera>().fieldOfView += 4;
                    }
                }
            }
            if (turentCam02OnOrOff == true)
            {               
                    playerCamera.SetActive(false);
                    freeRoamCamera.SetActive(false);
                    turentCam1.SetActive(false);
                    turentCam2.SetActive(true);
                    playerCamera.GetComponent<AudioListener>().enabled = false;
                    freeRoamCamera.GetComponent<AudioListener>().enabled = false;
                    turentCam1.GetComponent<AudioListener>().enabled = false;
                    turentCam2.GetComponent<AudioListener>().enabled = true;

                turentCam2.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (turentCam2.GetComponent<Camera>().fieldOfView >= 1)
                    {
                        turentCam2.GetComponent<Camera>().fieldOfView -= 4;
                    }
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (turentCam2.GetComponent<Camera>().fieldOfView <= 100)
                    {
                        turentCam2.GetComponent<Camera>().fieldOfView += 4;
                    }
                }
            }

            if (freeRoamCameraOnOrOff == true)
            {
                playerCamera.SetActive(false);
                freeRoamCamera.SetActive(true);
                turentCam1.SetActive(false);
                turentCam2.SetActive(false);
                playerCamera.GetComponent<AudioListener>().enabled = false;
                freeRoamCamera.GetComponent<AudioListener>().enabled = true;
                turentCam1.GetComponent<AudioListener>().enabled = false;
                turentCam2.GetComponent<AudioListener>().enabled = false;
                

                if (switchControls == true)
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

                freeRoamCamera.transform.Translate(0, 0, cameraSpeed * Time.deltaTime, Space.Self);

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
                turentCam1.SetActive(false);
                turentCam2.SetActive(false);
                playerCamera.GetComponent<AudioListener>().enabled = true;
                freeRoamCamera.GetComponent<AudioListener>().enabled = false;
                turentCam1.GetComponent<AudioListener>().enabled = false;
                turentCam2.GetComponent<AudioListener>().enabled = false;
                

                playerCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
                
                            
                
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (playerCamera.GetComponent<Camera>().fieldOfView >= 1)
                    {
                        playerCamera.GetComponent<Camera>().fieldOfView -= 4;
                    }
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (playerCamera.GetComponent<Camera>().fieldOfView <= 100)
                    {
                        playerCamera.GetComponent<Camera>().fieldOfView += 4;
                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                selectionCamera += (1);
            }          
        }

        private void SwitchCameraEvent()
        {
            if (_UsingFreeRoamCameraInstead != null)
            {
                _UsingFreeRoamCameraInstead(switchControls);
            }
        }

        private void OnEnable()
        {
            VehicleMovement._TurnPlayerCameraLeft += LeftTurn;
            VehicleMovement._TurnPlayerCameraLeft += RightTurn;
        }

        private void OnDisable()
        {
            VehicleMovement._TurnPlayerCameraLeft -= LeftTurn;
            VehicleMovement._TurnPlayerCameraLeft -= RightTurn;
        }

        private void LeftTurn(bool leftTurn)
        {
            if (leftTurn == true)
            {
                playerCamera.transform.Rotate(0f, +0.5f, 0f);
            }
        }

        private void RightTurn(bool rightTurn)
        {
            if (rightTurn == true)
            {
                playerCamera.transform.Rotate(0f, -0.5f, 0f);
            }
        }
        
    }
}
