using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour
{
    
    public AudioClip micClip;
    // Номер микрофона (если у вас подключено несколько микрофонов)
    int micDeviceIndex = 0;
    // Запуск записи с микрофона
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

    // Остановка записи с микрофона
    void StopRecording()
    {
        Microphone.End(Microphone.devices[micDeviceIndex]);

        

    }

    // Обновление данных с микрофона каждый кадр (если нужно)
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

    

    // Вызывается при завершении работы скрипта
    void OnDisable()
    {
        StopRecording();
    }
}
