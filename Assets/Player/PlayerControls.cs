using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;

    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 10f;

    [SerializeField] float positionPitchFactor = 4f;
    [SerializeField] float controlPitchFactor = -5f;
    
    [SerializeField] float positionYawFactor = -8f;
    
    [SerializeField] float controlRollFactor = -10f;

    float xThrow;
    float yThrow;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() 
    {
        movement.Enable();    
    }
    
    void OnDisable() 
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
       
       ProcessTranslation();
       ProcessRotation();




    }




    private void ProcessTranslation()
       {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;
 
 
         float xOffset = xThrow * Time.deltaTime * controlSpeed;
         float yOffset = yThrow * Time.deltaTime * controlSpeed;
 
         float rawXPos = transform.localPosition.x + xOffset;
         float rawYPos = transform.localPosition.y + yOffset;
         
         float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
         float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
         
 
         transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
        }



    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch =  pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
