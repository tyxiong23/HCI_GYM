using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class GestureFistL : GestureWidget
{
    private Handedness handedness = Handedness.Left;

    public override bool GestureCondition()
    {
        return HandPoseUtils.IsThumbGrabbing(handedness) && 
            HandPoseUtils.IsMiddleGrabbing(handedness) && 
            HandPoseUtils.IsIndexGrabbing(handedness) &&
            IsPinkyGrabbing(handedness) &&
            IsRingGrabbing(handedness);
    }
}
