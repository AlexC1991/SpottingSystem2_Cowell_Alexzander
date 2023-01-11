using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class UIManager : MonoBehaviour
    {
        [Header("FPS Text")]
        [SerializeField] private Text fpsCounter;
        private float framesPerSecond;

        [Header("Camera Selected Text")]
        [SerializeField] private Text selectedCameraText;
        [SerializeField] private GameObject cameraTextCanvasGroup;
        private float fadeAway = 2;
        private float fadeAwayKeep;
        private int cameraNumber;
        private float fadeAwayCanvas = 2f;
        private float fadeAwayResetCanvas;
        private bool timeDecrease;
        private bool timeCanvasDecrease;
        
        

        private void Start()
        {
            fadeAwayKeep = fadeAway;
            fadeAwayResetCanvas = fadeAwayCanvas;
            timeCanvasDecrease = false;
            timeDecrease = true;
        }

        // Update is called once per frame
        void Update()
        {
            framesPerSecond = 1.0f / Time.deltaTime;
            
            if (framesPerSecond >= 80)
            {
                framesPerSecond = 80;
            }
            
            fpsCounter.text = "FPS: " + framesPerSecond.ToString("F0");

            if (Input.GetKeyDown(KeyCode.C))
            {
                cameraNumber += 1;
                fadeAway = fadeAwayKeep;
                cameraTextCanvasGroup.GetComponent<CanvasGroup>().alpha = 1;
                timeCanvasDecrease = false;
                timeDecrease = true;
            }

            if (fadeAway <= 0)
            {

                timeDecrease = false;
                timeCanvasDecrease = true;                         
            }

            if (fadeAwayCanvas <= 0)
            {
                   cameraTextCanvasGroup.GetComponent<CanvasGroup>().alpha -= (0.1f);
                if (cameraTextCanvasGroup.GetComponent<CanvasGroup>().alpha == 0)
                {
                    cameraTextCanvasGroup.GetComponent<CanvasGroup>().alpha = 0;
                    timeCanvasDecrease = false;
                }
                fadeAwayCanvas = fadeAwayResetCanvas;
            }

            if (timeDecrease == true)
            {
                StartDecrease();
            }

            if (timeCanvasDecrease == true){

                StartCanvasDecrease();
            }
            

            if (cameraNumber == 0)
            {                  
                    selectedCameraText.text = "Main Camera \n" + "*Selected*".ToString();             
            }

            if (cameraNumber == 1)
            {                   
                    selectedCameraText.text = "Free Roam Camera \n" + " * Selected*".ToString();
            }

            if (cameraNumber == 2)
            {                   
                    selectedCameraText.text = "Turent Cam 1 \n" + " * Selected*".ToString();
            }

            if (cameraNumber == 3)
            {                   
                    selectedCameraText.text = "Turent Cam 2 \n" + " * Selected*".ToString();
            }

            if (cameraNumber == 4)
            {
                cameraNumber = 0;
            }
        }

        private void StartDecrease()
        {
            fadeAway -= 1 * Time.deltaTime;
            Debug.Log("Timer " + fadeAway);
        }

        private void StartCanvasDecrease()
        {
            fadeAwayCanvas -= 1;
            Debug.Log("Canvas "+ fadeAwayCanvas);
        }
    }
}
