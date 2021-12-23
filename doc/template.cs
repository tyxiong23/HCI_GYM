using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTemplate : MonoBehaviour
{
    // 设备本身所用的数值存储
    public TextMesh textMesh;
    private int temperature;
    private int humidity;

    // 各类激活信号、行为信号等，一般来自传感器，距离相关的判据也可以直接计算
    private bool focused;
    public GameObject head;
    private bool tempUp;
    private bool tempDown;
    private bool humiUp;
    private bool humiDown;

    // 交互冷却时间
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
        // 如果不设置冷却，可以只留下内层的 if
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

    // 完成设备行为的函数
    void SetDisplay()
    {
        textMesh.text = "Temp: " + temperature.ToString() + "°C\n" +
            "Humi: " + humidity.ToString() + "%";
        if (IsFocused())
        {
            textMesh.text += "\nactive";
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
        return focused && Vector3.Distance(transform.position, head.transform.position) < 1.0f;
    }

    // 方便传感器调用的 setter 函数
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
}
