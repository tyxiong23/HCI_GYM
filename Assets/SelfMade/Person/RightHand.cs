using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    private Handedness handedness = Handedness.Right;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, handedness, out var palmPose);
        transform.position = palmPose.Position;
        transform.rotation = palmPose.Rotation;
    }
}
