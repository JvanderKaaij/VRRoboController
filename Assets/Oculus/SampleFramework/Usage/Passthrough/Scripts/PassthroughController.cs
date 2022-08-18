using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    OVRPassthroughLayer passthroughLayer;

    void Start()
    {
        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }

        passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        if (passthroughLayer == null)
        {
            Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
        }
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z).normalized);
    }
}
