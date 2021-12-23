using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caller : MonoBehaviour
{
    // 源物体
    public GameObject head;
    public GameObject barbell;
    public GameObject runner;
    public GameObject thumbbell;

    public TextMesh textMesh;

    // 内部记录
    private bool callerActive;
    private Vector3 originalPos;
    private GameObject activeDevice;

    // 外界信号
    private bool callBarbell;
    private bool callRunner;
    private bool callThumbbell;
    private bool callEnd;

    // Start is called before the first frame update
    void Start()
    {
        callerActive = false;
        callBarbell = false;
        callRunner = false;
        callThumbbell = false;
        callEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 活跃状态：仅接收结束指令
        if (callerActive)
        {
            if (callEnd && IsInRange())
            {
                callerActive = false;
                activeDevice.transform.position = originalPos;
            }
        }
        // 非活跃状态：仅接收呼唤指令
        else
        {
            if (IsInRange())
            {
                if (callBarbell)
                    activeDevice = barbell;
                else if (callRunner)
                    activeDevice = runner;
                else if (callThumbbell)
                    activeDevice = thumbbell;
                if (callBarbell || callRunner || callThumbbell)
                {
                    callerActive = true;
                    originalPos = activeDevice.transform.position;
                    activeDevice.transform.position = new Vector3(
                        head.transform.position.x, originalPos.y, head.transform.position.z
                    );
                }
            }
        }
        // 清空触发信号，因为语音识别不支持自动置否
        callBarbell = false;
        callRunner = false;
        callThumbbell = false;
        callEnd = false;

        textMesh.text = Vector3.Distance(transform.position, head.transform.position).ToString();
    }

    bool IsInRange()
    {
        Vector3 head2D = new Vector3(head.transform.position.x, 0.0f, head.transform.position.z);
        Vector3 this2D = new Vector3(transform.position.x, 0.0f, transform.position.z);
        return Vector3.Distance(head2D, this2D) < 2f;
    }

    public void SetCallBarbell(bool flag)
    {
        callBarbell = flag;
    }

    public void SetCallRunner(bool flag)
    {
        callRunner = flag;
    }

    public void SetThumbbell(bool flag)
    {
        callThumbbell = flag;
    }

    public void SetCallEnd(bool flag)
    {
        callEnd = flag;
    }

    public void Reset()
    {
        barbell.GetComponent<BarbellBeh>().Reset();
        thumbbell.GetComponent<Dumbbell>().Reset();
        runner.GetComponent<RunnerTreadmill>().Reset();
    }
}
