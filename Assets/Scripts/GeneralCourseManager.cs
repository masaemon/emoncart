using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCourseManager : MonoBehaviour
{
    [SerializeField] private Text LapTimeMinText;
    [SerializeField] private Text LapTimeSecText;
    [SerializeField] private Text LapTimeMSecText;
    [SerializeField] private Text LapCountText;

    private static int laps = 0;
    private static int driverSector = 0;

    private bool isCorrectLap = true;

    private float time = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        calculationTime();
    }

    private void calculationTime()
    {
        if(driverSector != 0)time += Time.deltaTime;
        float fixedTime = time / Time.timeScale;
        int min = (int) fixedTime / 60;
        int sec = (int) fixedTime % 60;
        int msec = (int) (fixedTime * 1000 % 1000);

        LapTimeMinText.text = min < 10 ? "0" + min.ToString() : min.ToString();
        LapTimeSecText.text = sec < 10 ? "0" + sec.ToString() : sec.ToString();
        LapTimeMSecText.text = msec < 10 ? "00" + msec.ToString() : msec < 100 ? "0" + msec.ToString() : msec.ToString();
    }

    private void updateLap()
    {
        time = 0;
        laps += 1;
        LapCountText.text = laps.ToString();
    }

    public void onSectorChange(int sector)
    {
        switch (sector)
        {
            case 1:
                if (driverSector != 0 && driverSector != 5) isCorrectLap = false;
                updateLap();
                driverSector = 1;
                break;
            case 2:
                if (driverSector != 1) isCorrectLap = false;
                driverSector = 2;
                break;
            case 3:
                if (driverSector != 2) isCorrectLap = false;
                driverSector = 3;
                break;
            case 4:
                if (driverSector != 3) isCorrectLap = false;
                driverSector = 4;
                break;
            case 5:
                if (driverSector != 4) isCorrectLap = false;
                driverSector = 5;
                break;
        }
    }
}
