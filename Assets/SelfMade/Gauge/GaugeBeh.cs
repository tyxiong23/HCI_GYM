using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeBeh : MonoBehaviour
{
    // 展示数值
    public TextMesh textMesh;
    private int temperature;
    private int humidity;

    // 由各种传感器送来的信息
    private bool focused;
    public GameObject head;
    private bool tempUp;
    private bool tempDown;
    private bool humiUp;
    private bool humiDown;

    // 给数值改变设置冷却时间
    private int coolDown;

    // Start is called before the first frame update
    void Start()
    {
        temperature = 20;
        humidity = 30;
        SetDisplay();

        focused = false;
        tempUp = tempDown = humiUp = humiDown = false;

        coolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 仅在冷却完成时执行，如果未执行则不重置冷却
        if (coolDown == 0)
        {
            if (IsFocused())
            {
                coolDown = 60;
                if (tempUp)
                    ChangeVal(true, true);
                else if (tempDown)
                    ChangeVal(true, false);
                else if (humiUp)
                    ChangeVal(false, true);
                else if (humiDown)
                    ChangeVal(false, false);
                else
                    coolDown = 0;
            }
        }
        else
        {
            coolDown -= 1;
        }
        SetDisplay();
    }

    void SetDisplay()
    {
        textMesh.text = "Temp: " + temperature.ToString() + "°C\n" +
            "Humi: " + humidity.ToString() + "%";
        if (IsFocused())
        {
            textMesh.text += "\nactive";
        } else
        {
            // textMesh.text += "\nWalk closer";
            // \nfocus: " + IsFocused().ToString() + "\n" + "Dist:" + Vector3.Distance(transform.position, head.transform.position).ToString();
        }
    }

    void ChangeVal(bool isTemp, bool isUp)
    {
        if (isTemp)
            temperature += (isUp ? 1 : -1);
        else
            humidity += (isUp ? 1 : -1);
    }

    bool IsFocused()
    {
        return focused && Vector3.Distance(transform.position, head.transform.position) < 3.75f;
    }

    public void SetFocused(bool flag)
    {
        focused = flag;
    }

    public void SetTempUp(bool flag)
    {
        tempUp = flag;
    }

    public void SetTempDown(bool flag)
    {
        tempDown = flag;
    }
    public void SetHumiUp(bool flag)
    {
        humiUp = flag;
    }
    public void SetHumiDown(bool flag)
    {
        humiDown = flag;
    }

    public void CallTempUp()
    {
        if (IsFocused())
            temperature += 1;
    }

    public void CallTempDown()
    {
        if (IsFocused())
            temperature -= 1;
    }

    public void CallHumiUp()
    {
        if (IsFocused())
            humidity += 1;
    }

    public void CallHumiDown()
    {
        if (IsFocused())
            humidity -= 1;
    }
}
