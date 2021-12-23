using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class Drink : MonoBehaviour
{
    private Handedness handedness = Handedness.Right;
    private int drinkid = 0;
    public GameObject Beginner, Intermediate, Advanced;
    private Vector3 beginner_pos, advanced_pos, intermediate_pos;
    private UnityEngine.Quaternion beginner_rot, intermediate_rot, advanced_rot;
    private bool drink;
    public GameObject runner, dumpbell, barbell;
    private bool drinkFlag;
    private int drinkFlagCoolDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        drinkid = 1;
        drink = false; drinkFlag = false;

        beginner_pos = Beginner.transform.position;
        beginner_rot = Beginner.transform.rotation;
        intermediate_pos = Intermediate.transform.position;
        intermediate_rot = Intermediate.transform.rotation;
        advanced_pos = Advanced.transform.position;
        advanced_rot = Advanced.transform.rotation;
        drinkFlagCoolDown = 0;
    }

    private Vector3 old_position;
    private int coolDown;
    // Update is called once per frame
    void Update()
    {
        HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, handedness, out var palmPose);

        upDateID();

        if (drink)
        {
            if (Vector3.Distance(old_position, palmPose.Position) > 0.03f)
            {
                coolDown += 1;
                if (coolDown == 10)
                {
                    drink = false;
                    coolDown = 0;
                }
            } else
            {
                coolDown = 0;
            }
        } else
        {
            coolDown = 0;
        }

        if (drink)
        {
            if (drinkid == 1)
            {
                Beginner.transform.position = palmPose.Position;
                

            }
            else if (drinkid == 2)
            {
                Intermediate.transform.position = palmPose.Position;
                // Intermediate.transform.rotation = palmPose.Rotation;

            }
            else if (drinkid == 3)
            {
                Advanced.transform.position = palmPose.Position;
                // Advanced.transform.rotation = palmPose.Rotation;

            }
        }
        else
        {
            Beginner.transform.position = beginner_pos;
            Beginner.transform.rotation = beginner_rot;
            Intermediate.transform.position = intermediate_pos;
            Intermediate.transform.rotation = intermediate_rot;
            Advanced.transform.position = advanced_pos;
            Advanced.transform.rotation = advanced_rot;
        }
        old_position = palmPose.Position;

        // ”√ ÷ ∆ªΩ–—drink
        if (drinkFlag && !drink)
        {
            drinkFlagCoolDown += 1;
            if (drinkFlagCoolDown == 150)
            {
                drink = true;
            }
        }
        else
        {
            drinkFlagCoolDown = 0;
        }
    }

    private void upDateID()
    {
        int res = runner.GetComponent<RunnerTreadmill>().GetDistance() / 500;
        res += barbell.GetComponent<BarbellBeh>().GetCount() * 2;
        res += dumpbell.GetComponent<Dumbbell>().GetCount();
        if (res < 10)
            drinkid = 1;
        else if (res < 30)
            drinkid = 2;
        else
            drinkid = 3;
    }

    public void setBottle()
    {
        drink = true;
    }

    public void endBottle()
    {
        drink = false;
    }

    public void setDrinkFlag(bool flag)
    {
        drinkFlag = flag;
    }

}
