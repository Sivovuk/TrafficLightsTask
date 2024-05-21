using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TrafficLightController : MonoBehaviour
{
    [SerializeField]
    private TrafficLight _trafficLightConfig;

    [SerializeField] 
    private TMP_Text _lightStateTMP;

    public UnityAction<TrafficLight.Lights> OnTafficLightChange;
    
    private float _currentLightTimer;
    private TrafficLight.Lights _currentLight = TrafficLight.Lights.Red;

    private List<string> _lightStateText = new List<string>()
    {
        "Traffic may not proceed beyond the stop line or otherwise enter the intersection",
        "The Signal is about to change, but the red rules do apply",
        "Traffic may not pass the stop line or enter the intersection unless it cannot safely stop when the light shows",
        "Traffic may proceed unless it would not clear the intersection before the next change of phase"
    };
    
    private void Start()
    {
        _trafficLightConfig.IsReverse = false;
        _currentLightTimer = _trafficLightConfig.GetCurrentLightDuration(_currentLight);
        SwitchLight();
    }

    private void Update()
    {
        _currentLightTimer -= Time.deltaTime;

        if (_currentLightTimer <= 0)
        {
            _currentLight = _trafficLightConfig.GetNextLight(_currentLight);
            _currentLightTimer = _trafficLightConfig.GetCurrentLightDuration(_currentLight);
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        OnTafficLightChange?.Invoke(_currentLight);
        ChangeLightStateText();
    }

    private void ChangeLightStateText()
    {
        if ((int)_currentLight < 3)
            _lightStateTMP.text = _lightStateText[(int)_currentLight];
        else
            _lightStateTMP.text = _lightStateText[_lightStateText.Count - 1];
    }
}
