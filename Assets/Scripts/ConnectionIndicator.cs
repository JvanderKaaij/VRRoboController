using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionIndicator : MonoBehaviour
{

    public Material connectedMat;

    public Material disconnectedMat;

    public MeshRenderer meshRenderer;

    public void SetConnected()
    {
        meshRenderer.material = connectedMat;
    }

    private void Start()
    {
        meshRenderer.material = disconnectedMat;
    }
}
