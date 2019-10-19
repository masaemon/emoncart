using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float breakTorque;
    public float Downforce;
    
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues (typeof (Joycon.Button)) as Joycon.Button[];
    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;
    
    private void Start ()
    {
        SetControllers ();
        Time.timeScale = 1.6f;
    }
    
    private void FixedUpdate()
    {
        m_pressedButtonL = null;
        m_pressedButtonR = null;
        float motor = 0.0f;
        float steering = 0.0f;

        if (m_joycons == null || m_joycons.Count > 0)
        {
            SetControllers ();
            foreach (var button in m_buttons)
            {
                if (m_joyconL.GetButton(button))
                {
                    m_pressedButtonL = button;
                }
            }
            
            if (m_pressedButtonL == Joycon.Button.DPAD_DOWN) motor = -1 * maxMotorTorque;
            else if(m_pressedButtonL == Joycon.Button.DPAD_LEFT) motor = maxMotorTorque;
            steering = -1 * maxSteeringAngle * m_joycons[0].GetStick()[1];

        }
        else
        {
            motor = -1 * maxMotorTorque * Input.GetAxis("Vertical");
            steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        }
        
        AddDownForce();
        

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor)
            { 
                //if (motor < 0)
                //{
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
                //}
            }
            
            if (motor > 0)
            {
                axleInfo.leftWheel.brakeTorque = breakTorque;
                axleInfo.rightWheel.brakeTorque = breakTorque;
            }
            else
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
            }

        }
    }
    
    
    private void AddDownForce()
    {
        //Debug.Log(axleInfos[0].leftWheel.attachedRigidbody.velocity.magnitude);
       axleInfos[0].leftWheel.attachedRigidbody.AddForce(-transform.up*Downforce*axleInfos[0].leftWheel.attachedRigidbody.velocity.magnitude);
    }
    
    
    private void SetControllers ()
    {
        m_joycons = JoyconManager.Instance.j;
        if (m_joycons == null || m_joycons.Count <= 0) return;
        m_joyconL = m_joycons.Find (c => c.isLeft);
        m_joyconR = m_joycons.Find (c => !c.isLeft);
    }
}
