using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
[System.Serializable]

public class MapRotationScript : MonoBehaviour
{
    // Rotation Parameter
    [SerializeField] private float rotationSpeed = 10f;
    private Vector3 currentRotationAngle;
    private Vector3 targetRotationAngle;
    private bool isRotating;
    private bool isRotateDirX;
    private float rotateDirDiff;
    private bool isKeyDownE;
    private float process;

    // Stardard axis for map to rotate arround 
    [SerializeField] private Transform StAxis;
    
    // Start is called before the first frame update
    void Start()
    {
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(StAxis.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        //Debug.Log(isRotateDirX);
        float step = rotationSpeed * Time.deltaTime;
        if (isRotating)
        {
            // if (!isKeyDownE)
            // {
            //     transform.RotateAround(StAxis.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            //     Debug.Log("Cur: " + transform.rotation.eulerAngles.z + "  Target: " + targetRotationAngle.z);
            //     if (isRotateDirX && Mathf.Approximately(transform.rotation.eulerAngles.x, targetRotationAngle.x))
            //     {
            //         isRotating = false;
            //     } else if (!isRotateDirX && Mathf.Approximately(transform.rotation.eulerAngles.z, targetRotationAngle.z))
            //     {
            //         isRotating = false;
            //     }
            // } 
            // else {
            //     transform.RotateAround(StAxis.position, Vector3.up, rotationSpeed * Time.deltaTime);
            // }
            
            if (isRotateDirX) {
                Debug.Log("Cur: " + process + "  Target X: " + targetRotationAngle.x);
                process = Mathf.MoveTowardsAngle(transform.eulerAngles.x, targetRotationAngle.x, step);
                transform.eulerAngles = new Vector3(process, transform.eulerAngles.y, transform.eulerAngles.z);

                if (Mathf.Approximately(process, targetRotationAngle.x))
                {
                    isRotating = false;
                    transform.eulerAngles = new Vector3(targetRotationAngle.x, transform.eulerAngles.y, transform.eulerAngles.z);
                }

            } else {
                Debug.Log("Cur: " + process + "  Target Z: " + targetRotationAngle.z);
                process = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotationAngle.z, step);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, process);

                if (Mathf.Approximately(process, targetRotationAngle.z))
                {
                    isRotating = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetRotationAngle.z);
                }
            }
        }

        // Using keypress for testing
        // TODO: Change to mouse control later on
        else if (Input.GetKeyDown(KeyCode.E))
        {
            isRotating = true;
            currentRotationAngle = transform.rotation.eulerAngles;
            transform.RotateAround(StAxis.position, Vector3.left, rotationSpeed * Time.deltaTime);
            Debug.Log("E Pressed: " + currentRotationAngle);
            Debug.Log("E Pressed: " + transform.rotation.eulerAngles);

            // Round float to 2 decimal place in order to ignore tiny shake on unwanted component
            rotateDirDiff = currentRotationAngle.x - transform.rotation.eulerAngles.x;
            rotateDirDiff = (float)Math.Round((double)rotateDirDiff, 2);
            Debug.Log("rotationDiff: " + rotateDirDiff);

            // Compare to checkout which component is rotating
            // TODO: Could we simplify this to a lambda expression?
            if (rotateDirDiff != 0 && (Mathf.Abs(rotateDirDiff) > 359 || Mathf.Abs(rotateDirDiff) < 1))
            {
                isRotateDirX = true;
                targetRotationAngle = currentRotationAngle;

                //TODO: Rotation too fast, change -1 to larger number if rotation speed has been changed to a larger number
                if ((rotateDirDiff < 0 && rotateDirDiff > -45) || rotateDirDiff > 359) 
                {
                    targetRotationAngle.x += 90.0f;
                } else {
                    targetRotationAngle.x -= 90.0f;
                }
            } else {
                rotateDirDiff = currentRotationAngle.z - transform.rotation.eulerAngles.z;
                isRotateDirX = false;
                targetRotationAngle = currentRotationAngle;
                if ((rotateDirDiff < 0 && rotateDirDiff > -45) || rotateDirDiff > 359) 
                {
                    targetRotationAngle.z += 90.0f;
                } else {
                    targetRotationAngle.z -= 90.0f;
                }
            }
        }
        // {
        //     isKeyDownE = true;
        //     isRotating = true;
        //     currentRotationAngle = transform.rotation.eulerAngles;
        //     transform.RotateAround(StAxis.position, Vector3.right, rotationSpeed * Time.deltaTime);
        //     Debug.Log("E Pressed: " + currentRotationAngle);
        //     Debug.Log("E Pressed: " + transform.rotation.eulerAngles);

        //     // Compare to checkout which component is rotating
        //     // TODO: Could we simplify this to a lambda expression?
        //     rotateDirDiff = currentRotationAngle.x - transform.rotation.eulerAngles.x;
        //     if (rotateDirDiff != 0 && (rotateDirDiff < 1)) 
        //     {
        //         isRotateDirX = true;
        //         targetRotationAngle = currentRotationAngle;
        //         targetRotationAngle.x -= 22.5f;
        //     } else {
        //         isRotateDirX = false;
        //         targetRotationAngle = currentRotationAngle;
        //         targetRotationAngle.z += 22.5f;
        //     } 
        // }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            isRotating = true;
            currentRotationAngle = transform.rotation.eulerAngles;
            transform.RotateAround(StAxis.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            Debug.Log("Q Pressed: " + currentRotationAngle);
            Debug.Log("Q Pressed: " + transform.rotation.eulerAngles);

            // Round float to 2 decimal place in order to ignore tiny shake on unwanted component
            rotateDirDiff = currentRotationAngle.x - transform.rotation.eulerAngles.x;
            rotateDirDiff = (float)Math.Round((double)rotateDirDiff, 2);
            Debug.Log("rotationDiff: " + rotateDirDiff);

            // Compare to checkout which component is rotating
            // TODO: Could we simplify this to a lambda expression?
            if (rotateDirDiff != 0 && (Mathf.Abs(rotateDirDiff) > 359 || Mathf.Abs(rotateDirDiff) < 1))
            {
                isRotateDirX = true;
                targetRotationAngle = currentRotationAngle;

                //TODO: Rotation too fast, change -1 to larger number if rotation speed has been changed to a larger number
                if ((rotateDirDiff < 0 && rotateDirDiff > -45) || rotateDirDiff > 359) 
                {
                    targetRotationAngle.x += 90.0f;
                } else {
                    targetRotationAngle.x -= 90.0f;
                }
            } else {
                rotateDirDiff = currentRotationAngle.z - transform.rotation.eulerAngles.z;
                isRotateDirX = false;
                targetRotationAngle = currentRotationAngle;
                if ((rotateDirDiff < 0 && rotateDirDiff > -45) || rotateDirDiff > 359) 
                {
                    targetRotationAngle.z += 90.0f;
                } else {
                    targetRotationAngle.z -= 90.0f;
                }
            }
        }


    }
}
