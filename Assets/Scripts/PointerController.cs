using UnityEngine;

public class PointerController : MonoBehaviour
{
    public LineRenderer line;

    public RosSocketCommunication socketCommunication;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            line.SetPosition(1, new Vector3(0,0,hit.distance));
        }

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0.0)
        {
            line.endColor = Color.green;
            socketCommunication.SendPosition(hit.point);
        }
        else
        {
            line.endColor = Color.white;
        }
    }
}
