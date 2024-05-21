using System;
using UnityEngine;
using UnityEngine.Events;

public class TrafficLightObject : MonoBehaviour
{
    private TrafficLight.Lights _currentLight;
    public UnityAction<TrafficLight.Lights> OnTafficLightChange;

    [SerializeField] 
    private GameObject _redLight;
    [SerializeField] 
    private GameObject _amberLight;
    [SerializeField] 
    private GameObject _greenLight;

    private GameObject _firstLightToAnimate;
    private GameObject _secondLightToAnimate;

    private bool _startAnimation = false;
    private float _timer;

    private TrafficLightController trafficLightController;
    
    private void Start()
    {
        trafficLightController = GetComponentInParent<TrafficLightController>();
        trafficLightController.OnTafficLightChange += OnLightChange;
    }

    private void OnDestroy()
    {
        if(trafficLightController == null) return;
        trafficLightController.OnTafficLightChange -= OnLightChange;
    }

    private void Update()
    {
        if (_startAnimation)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.05f)
            {
                _timer = 0;
                
                Vector3 firstLightSize = _firstLightToAnimate.transform.localScale;
                _firstLightToAnimate.transform.localScale = new Vector3(firstLightSize.x+0.01f,firstLightSize.y+0.01f,firstLightSize.z+0.01f);
                
                if (_secondLightToAnimate == null) return; 
                
                Vector3 secondLightSize = _firstLightToAnimate.transform.localScale;
                _secondLightToAnimate.transform.localScale = new Vector3(secondLightSize.x+0.01f,secondLightSize.y+0.01f,secondLightSize.z+0.01f);
            }

            if (_firstLightToAnimate.transform.localScale.x >= 0.1f)
            {
                _startAnimation = false;
                _timer = 0;
            }
        }
    }

    private void OnLightChange(TrafficLight.Lights currentLight)
    {
        _currentLight = currentLight;
        OnTafficLightChange?.Invoke(_currentLight);
        SetLightObject();
    }

    private void SetLightObject()
    {
        _redLight.transform.localScale = new Vector3(0, 0, 0);
        _amberLight.transform.localScale = new Vector3(0, 0, 0);
        _greenLight.transform.localScale = new Vector3(0, 0, 0);
        
        _firstLightToAnimate = null;
        _secondLightToAnimate = null;
        
        if (_currentLight == TrafficLight.Lights.Red)
        {
            _firstLightToAnimate = _redLight;
        }
        else if (_currentLight == TrafficLight.Lights.Red_Amber)
        {
            _firstLightToAnimate = _redLight;
            _secondLightToAnimate = _amberLight;
        }
        else if (_currentLight == TrafficLight.Lights.Amber)
        {
            _firstLightToAnimate = _amberLight;
        }
        else if (_currentLight == TrafficLight.Lights.Green_Amber)
        {
            _firstLightToAnimate = _greenLight;
            _secondLightToAnimate = _amberLight;
        }
        else
        {
            _firstLightToAnimate = _greenLight;
        }

        _startAnimation = true;
    }
}