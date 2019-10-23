using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralCourseManager : MonoBehaviour
{
    
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject[] sector;
    [SerializeField] private GameObject controlLine;
    
    private static int laps = 0;
    private static int driverSector = 0;

    private bool isCorrectLap = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSectorChange(int sector)
    {
        switch (sector)
        {
            case 1:
                if (driverSector != 0 && driverSector != 5) isCorrectLap = false;
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
