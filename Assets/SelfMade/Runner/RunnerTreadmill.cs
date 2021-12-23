using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
public class RunnerTreadmill : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 original;
    public GameObject plane;
    public GameObject mainCamera;
    private float speed = 0.1f, minSpeed = 0.06f, maxSpeed = 0.3f;
    private bool focused = false;
    private bool running = false;
    private float maxDistance = 1;
    public GameObject lHand, rhand;
    public GameObject cubeLeft, cubeRight;
    private int coolDown = 0;
    private int time = 0, timecount = 0;
    private float distance = 0;
    public TextMesh distanceMesh, timeMesh, paceMesh;
    public GameObject musicPlayer;
    private bool fiveFlag;

    private bool speedIncr, speedDecr;

    private bool ifBothHandClose()
    {
        bool result= false;
        if (Vector3.Distance(lHand.gameObject.transform.position, rhand.gameObject.transform.position) < 0.05f)
        {
            result = true;
        }
        
        return result && fiveFlag;
    }

    public void setFive(bool flag)
    {
        fiveFlag = focused & flag;
    }
    
    void Start()
    {
        original = transform.position;
        running = false;
        focused = false;
        coolDown = 0;
        time = 0;
        timecount = 0;
        speed = minSpeed;
        distance = 0;
        speedIncr = speedDecr = false;
        cubeRight.SetActive(false);
        fiveFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ifCloseEnough())
        {
            focused = true;
            // 双手合在一起停止
            if (running && ifBothHandClose())
            {
                coolDown += 1;
                if (coolDown >= 20 && timecount >= 300)
                    stopRun();
            }
            else if (!running && ifBothHandClose())
            {
                coolDown += 1;
                if (coolDown == 60)
                {
                    running = true;
                    coolDown = 0;
                }
            }
            else if (ifHandClose())
            {

                coolDown += 1;
                if (running)
                {
                    if (coolDown >= 20 && timecount >= 300)
                    {
                        stopRun();
                    }
                }
                else if (coolDown == 60)
                {
                    running = true;
                    coolDown = 0;
                }
            } else
            {
                coolDown = 0;
            }

            if (running)
            {
                distance += speed / 3.8f;
                timecount += 1;
                if (timecount % 60 == 0)
                {
                    time += 1;

                    float delta = 0.06f;
                    if (speedIncr)
                    {
                        if (speed < maxSpeed) speed += delta;
                        setMusicSpeed();
                    }
                    else if (speedDecr)
                    {
                        if (speed > minSpeed) speed -= delta;
                        setMusicSpeed();
                    }
                }
            } else
            {
                timecount = 0;
            }

        } else
        {
            deactivate();
        }
    }

    private void LateUpdate()
    {
        if (focused)
        {
            Renderer rend = plane.GetComponent<Renderer>();

            plane.gameObject.SetActive(true);
            distanceMesh.text = ((int)distance).ToString() + " m";
            timeMesh.text = time.ToString() + " s";

            if (running)
            {
                paceMesh.text = ((int)(speed / 3.8f * 60 * 100) / 100f).ToString() + " m/s"; 
                float offset = Time.time * speed;
                rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
            } else
            {
                paceMesh.text = "0.0 m/s";
            }            
        }
    }

    private float horizDistance;
    private bool ifCloseEnough()
    {
        Vector3 pos1 = transform.position, pos2 = mainCamera.transform.position;
        horizDistance = (pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.z - pos2.z) * (pos1.z - pos2.z);
        if (horizDistance < 1)
        {
            return true;
        }
        return false;
    }

    public void activateFar()
    {
        transform.position.Set(mainCamera.transform.position.x, mainCamera.transform.position.y, original.z);
    }

    private void deactivate()
    {
        speed = minSpeed;
        if (running)
            setMusicSpeed();
        plane.gameObject.SetActive(false);
        transform.position.Set(original.x, original.y, original.z);
        running = false; focused = false;
        time = 0; timecount = 0;
        speedDecr = speedIncr = false;
        fiveFlag = false;
    }

    private void setMusicSpeed()
    {
        musicPlayer.GetComponent<MusicPlayerScript>().setSpeed(speed);
    }

    private float dis1, dis2;
    private bool ifHandClose()
    {
        Vector3 cubeLPos = cubeLeft.transform.position, cubeRPos = cubeRight.transform.position;
        dis1 = Vector3.Distance(lHand.transform.position, cubeLPos);
        dis2 = Vector3.Distance(rhand.transform.position, cubeRPos);
        float maxHandDistance = 0.07f;
        if (maxHandDistance > dis1)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void deActivateVoice(bool flag)
    {
        if (flag)
        {
            stopRun();
        }
    }

    private void stopRun()
    {
        running = false;
        coolDown = -180;
        speedDecr = speedIncr = false;
        timecount = 0;
        speed = minSpeed;
        setMusicSpeed();
    }

    public int GetDistance()
    {
        return (int)distance;
    }

    public void speedUp(bool flag)
    {
        speedIncr = flag;
    }

    public void slowDown(bool flag)
    {
        speedDecr = flag;
    }

    public void Reset()
    {
        speed = minSpeed;
        time = 0;
        timecount = 0;
        coolDown = 0;
        distance = 0;
    }

    public void callRun()
    {
        if (focused && !running)
            running = true;
    }

    public void callStopRun()
    {
        if (focused && running)
            stopRun();
    }

    public void callSpeedUp()
    {
        float delta = 0.06f;
        if (focused && running)
        {
            if (speed < maxSpeed)
            {
                speed += delta;
                setMusicSpeed();
            }
        }
    }

    public void callSpeedDown()
    {
        float delta = 0.06f;
        if (focused && running)
        {
            if (speed > minSpeed)
            {
                speed -= delta;
                setMusicSpeed();
            }
        }
    }

}
