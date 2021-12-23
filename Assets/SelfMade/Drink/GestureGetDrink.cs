using Microsoft.MixedReality.Toolkit.Utilities;

public class GestureGetDrink : GestureWidget
{
    private Handedness right = Handedness.Right, left = Handedness.Left;
    public override bool GestureCondition()
    {
        bool flag_right =  !HandPoseUtils.IsThumbGrabbing(right) &&
            !HandPoseUtils.IsMiddleGrabbing(right) &&
            !HandPoseUtils.IsIndexGrabbing(right) &&
            !IsPinkyGrabbing(right) &&
            !IsRingGrabbing(right);
        bool flag_left = HandPoseUtils.IsThumbGrabbing(left) &&
            HandPoseUtils.IsMiddleGrabbing(left) &&
            HandPoseUtils.IsIndexGrabbing(left) &&
            IsPinkyGrabbing(left) &&
            IsRingGrabbing(left);
        return flag_left && flag_right;
    }
}
