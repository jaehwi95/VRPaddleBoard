using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectWithVive : MonoBehaviour {

    private SteamVR_Controller.Device leftDevice;
    private SteamVR_Controller.Device rightDevice;

    // Use this for initialization
    void Start()
    {
        int leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        leftDevice = SteamVR_Controller.Input(leftIndex);

        int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        rightDevice = SteamVR_Controller.Input(rightIndex);
    }


    // Update is called once per frame
    void Update () {
        
    }
}
