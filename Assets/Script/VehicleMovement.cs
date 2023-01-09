using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlexzanderCowell
{


    public class VehicleMovement : MonoBehaviour
    {
        [SerializeField] float vehicleForwardSpeed;
        [SerializeField] float vehicleReverseSpeed;
        [SerializeField] float maxForwardSpeed = 5f;
        [SerializeField] float maxReverseSpeed = -3f;
        [SerializeField] float vehicleTurnSpeed = 0.1f;
        [SerializeField] Transform vehicleToMove;
        private float yaw = 0.0f;
        private float pitch = 0.0f;
        [SerializeField] float gravityMultipler;
        [SerializeField] private Material inTangleble; // Material to turn objects invisble.
        [SerializeField] private Material tangleble; // Material to turn objects into wooden blocks.
        private Rigidbody ridgeBody;
        private bool hiddenPlayer;
        
        

        private void Start()
        {
            ridgeBody = GetComponent<Rigidbody>();
            
        }

        private void Update()
       {
            
        }

        private void OnTriggerEnter(Collider other) // Checks for anything entering into the trigger zone.
        {
            if (other.CompareTag("Bush")) // If anything with the "Player" tag comes into the trigger zone it will activate the code below.
            {
                GetComponent<MeshRenderer>().material = tangleble;
                hiddenPlayer = true;
            }

        }

        private void OnTriggerExit(Collider other) // Checks for anything exiting out of the trigger zone.
        {
            if (other.CompareTag("Bush")) // If anything with the "Player" tag comes out of the trigger zone it will activate the code below.
            {
                GetComponent<MeshRenderer>().material = inTangleble;
                hiddenPlayer = false;
            }
        }

        private void OnEnable()
        {
            FreeRoamCamera._UsingFreeRoamCameraInstead += Controls;
        }

        private void OnDisable()
        {
            FreeRoamCamera._UsingFreeRoamCameraInstead -= Controls;
        }

            void Controls(bool switchControls)
        {
            float moveMousex = +vehicleTurnSpeed * Input.GetAxis("Mouse X");
            float moveMousey = vehicleTurnSpeed * Input.GetAxis("Mouse X");
            yaw += vehicleTurnSpeed * Input.GetAxis("Mouse X");
            pitch -= vehicleTurnSpeed * Input.GetAxis("Mouse Y");
            transform.Translate(0, 0, vehicleForwardSpeed * Time.deltaTime, Space.Self);
            transform.Translate(0, 0, vehicleReverseSpeed * Time.deltaTime, Space.Self);


            ridgeBody.AddForce((Vector3.down * gravityMultipler), ForceMode.Acceleration);

            if (switchControls == false)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    vehicleForwardSpeed += 0.1f;
                }

                if (vehicleForwardSpeed >= maxForwardSpeed)
                {
                    vehicleForwardSpeed = maxForwardSpeed;
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    vehicleForwardSpeed = 0;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    vehicleReverseSpeed -= (0.1f);
                }

                if (vehicleReverseSpeed <= maxReverseSpeed)
                {
                    vehicleReverseSpeed = maxReverseSpeed;
                }

                if (Input.GetKeyUp(KeyCode.S))
                {
                    vehicleReverseSpeed = 0;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(0f, -vehicleTurnSpeed, 0f, Space.Self);

                }

                if (Input.GetKey(KeyCode.D))
                {

                    transform.Rotate(0f, +vehicleTurnSpeed, 0f, Space.Self);
                }
            }

            
        }




    }
}
