using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour
{
    
    public AudioClip micClip;
    // ����� ��������� (���� � ��� ���������� ��������� ����������)
    int micDeviceIndex = 0;
    // ������ ������ � ���������
    void StartRecording()
    {
        if (Microphone.devices.Length > 0)
        {
            micClip = Microphone.Start(Microphone.devices[micDeviceIndex], true, 10, AudioSettings.outputSampleRate);
            
        }
        else
        {
            Debug.LogWarning("No microphone detected!");
        }
    }

    // ��������� ������ � ���������
    void StopRecording()
    {
        Microphone.End(Microphone.devices[micDeviceIndex]);

        

    }

    // ���������� ������ � ��������� ������ ���� (���� �����)
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V)) 
        {
            StartRecording();
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            StopRecording();
        }
    }

    

    // ���������� ��� ���������� ������ �������
    void OnDisable()
    {
        StopRecording();
    }
}
