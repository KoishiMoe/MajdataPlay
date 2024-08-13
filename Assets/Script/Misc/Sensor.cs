using MajdataPlay.IO;
using MajdataPlay.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool IsJudging { get; set; } = false;
    public SensorStatus Status = SensorStatus.Off;
    public SensorType Type;
    public SensorGroup Group 
    { 
        get
        {
            var i = (int)Type;
            if (i <= 7)
                return SensorGroup.A;
            else if (i <= 15)
                return SensorGroup.B;
            else if (i <= 16)
                return SensorGroup.C;
            else if (i <= 24)
                return SensorGroup.D;
            else
                return SensorGroup.E;
        }
    }

    public event EventHandler<InputEventArgs> OnStatusChanged;//oStatus nStatus
    public void PushEvent(in InputEventArgs args)
    {
        if (OnStatusChanged is not null)
            OnStatusChanged(this, args);
    }
    void Awake() => DontDestroyOnLoad(this);
}
