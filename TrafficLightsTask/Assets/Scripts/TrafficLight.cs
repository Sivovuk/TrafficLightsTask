using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrafficLight", menuName = "Traffic/Traffic Light", order = 1)]
public class TrafficLight : ScriptableObject
{
    public enum Lights
    {
        Red = 0,
        Red_Amber = 1,
        Amber = 2,
        Green_Amber = 3,
        Green = 4
    }
    
    public Color RedLightColor = Color.red;
    public Color AmberLightColor = Color.yellow;
    public Color GreenLightColor = Color.green;

    [Space(10)]
    
    public float RedDuration = 5f;
    public float RedAmberDuration = 1f;
    public float AmberDuration = 2f;
    public float GreenAmberDuration = 1f;
    public float GreenDuration = 5f;

    public bool IsReverse = false;

    public float GetCurrentLightDuration(Lights light)
    {
        if (light == Lights.Red)
            return RedDuration;
        else if (light == Lights.Red_Amber)
            return RedAmberDuration;
        else if (light == Lights.Amber)
            return AmberDuration;
        else if (light == Lights.Green_Amber)
            return GreenAmberDuration;
        else
            return GreenDuration;
    }

    public Lights GetNextLight(Lights currentLight)
    {
        int index = (int)currentLight;
        var values = Enum.GetValues(typeof(Lights));

        if (index >= 0 && index-1 < values.Length)
        {
            if (!IsReverse)
            {
                index++;
                if (index == values.Length-1) 
                    IsReverse = true;
                return (Lights)values.GetValue(index);
            }
            else
            {
                index--;
                if (index == 0) 
                    IsReverse = false;
                return (Lights)values.GetValue(index);
            }
        }

        return currentLight;
    }
}
