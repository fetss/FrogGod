using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SmoothMouseLook : MonoBehaviour
{

    public float sensitivityY = 8F;
    public float minimumY = -60F;
    public float maximumY = 80F;
    float rotationY = 0F;
    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;
    public float frameCounter = 10;
    Quaternion originalRotation;

    public Quaternion xQuaternion { get; private set; }

    void Update()
    {
        //Resets the average rotation
        rotAverageY = 0f;

        //Gets rotational input from the mouse
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

        rotationY = ClampAngle(rotationY, minimumY, maximumY);

        //Adds the rotation values to their relative array
        rotArrayY.Add(rotationY);

        //If the arrays length is bigger or equal to the value of frameCounter remove the first value in the array
        if (rotArrayY.Count >= frameCounter)
        {
            rotArrayY.RemoveAt(0);
        }

        //Adding up all the rotational input values from each array
        for (int j = 0; j < rotArrayY.Count; j++)
        {
            rotAverageY += rotArrayY[j];
        }

        //Standard maths to find the average
        rotAverageY /= rotArrayY.Count;

        //Clamp the rotation average to be within a specific value range
        rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);

        //Get the rotation you will be at next as a Quaternion
        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);

        //Rotate
        transform.localRotation = originalRotation * yQuaternion;

    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}