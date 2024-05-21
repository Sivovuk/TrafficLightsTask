using System;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private TMP_Text _driveStateTMP;
    private TrafficLight.Lights _currentLight;
    
    private void TrafficLightCheck(TrafficLight.Lights light)
    {
        _currentLight = light;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TrafficLight"))
        {
            other.GetComponent<TrafficLightObject>().OnTafficLightChange += TrafficLightCheck;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TrafficLight"))
        {
            other.GetComponent<TrafficLightObject>().OnTafficLightChange -= TrafficLightCheck;
        }
    }
    
    public void CanDrive()
    {
        if (_currentLight == TrafficLight.Lights.Red || 
            _currentLight == TrafficLight.Lights.Red_Amber || 
            _currentLight == TrafficLight.Lights.Amber)
        {
            _driveStateTMP.text = "Can't drive";
        }
        else
        {
            _driveStateTMP.text = "Can drive";
        }
    }

}
