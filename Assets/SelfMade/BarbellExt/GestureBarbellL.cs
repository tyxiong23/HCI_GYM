using Microsoft.MixedReality.Toolkit.Utilities;

public class GestureBarbellL : GestureWidget
{
    private Handedness handedness = Handedness.Left;

    public override bool GestureCondition()
    {
        int count = 0;
        if (HandPoseUtils.IsThumbGrabbing(handedness))
            count += 1;
        if (HandPoseUtils.IsMiddleGrabbing(handedness))
            count += 1; 
        if (HandPoseUtils.IsIndexGrabbing(handedness))
            count += 1;
        if (IsPinkyGrabbing(handedness))
            count += 1;
        if (IsRingGrabbing(handedness))
            count += 1;
        return count >= 4;
    }
}
