using Microsoft.MixedReality.Toolkit.Utilities;

public class GestureBarbellR : GestureWidget
{
    public override bool GestureCondition()
    {
        int count = 0;
        if (HandPoseUtils.IsThumbGrabbing(_handedness))
            count += 1;
        if (HandPoseUtils.IsMiddleGrabbing(_handedness))
            count += 1; 
        if (HandPoseUtils.IsIndexGrabbing(_handedness))
            count += 1;
        if (IsPinkyGrabbing(_handedness))
            count += 1;
        if (IsRingGrabbing(_handedness))
            count += 1;
        return count >= 4;
    }
}
