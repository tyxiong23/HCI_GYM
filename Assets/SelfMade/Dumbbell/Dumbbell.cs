using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbbell : MonoBehaviour
{
    // 源物体
    public TextMesh textMesh;
    public GameObject leftHand;
    public GameObject rightHand;

    // 内部数据记录
    private int count;
    private bool isUp;
    private Vector3 originalPos;
    private bool perfect;

    private const float upperBound = 0.9f;
    private const float lowerBound = 0.45f;
    private const float midBound = 0.58f;

    // 传感器信号
    private bool sigLeft;
    private bool sigRight;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        isUp = false;
        originalPos = transform.position;
        perfect = false;

        sigLeft = false;
        sigRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrabed())
        {
            Vector3 position = (leftHand.transform.position + rightHand.transform.position) / 2;
            float rotateZ = Mathf.Rad2Deg * Mathf.Atan(
                (leftHand.transform.position.z - rightHand.transform.position.z) /
                (leftHand.transform.position.x - rightHand.transform.position.x)
            );
            transform.SetPositionAndRotation(position, Quaternion.identity);
            transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
            transform.Rotate(new Vector3(0.0f, 0.0f, rotateZ));

            // 计数
            ChangeCount();
        }

        // 未被拿起
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                originalPos.y,
                transform.position.z
            );
            isUp = false;
        }

        // 渲染文字
        textMesh.text = "Count: " + count.ToString() + "\n";
        if (IsGrabed() && isUp)
        {
            textMesh.text += (perfect ? "Perfect" : "Good");
        }

        // test
        //textMesh.text += "\n" + (perfect ? "p" : "g");
    }

    bool IsGrabed()
    {
        return sigLeft && sigRight
            && Vector3.Distance(transform.position, leftHand.transform.position) < 0.2f
            && Vector3.Distance(transform.position, rightHand.transform.position) < 0.2f
            && Vector3.Distance(rightHand.transform.position, leftHand.transform.position) < 0.25f;
    }

    void ChangeCount()
    {
        if (isUp)
        {
            if (transform.position.y < midBound)
            {
                isUp = false;
                perfect = false;
            }
        }
        else
        {
            if (!perfect)
                perfect = transform.position.y < lowerBound;
            if (transform.position.y > upperBound)
            {
                isUp = true;
                count += 1;
            }
        }
    }

    public void SetSigLeft(bool flag)
    {
        sigLeft = flag;
    }

    public void SetSigRight(bool flag)
    {
        sigRight = flag;
    }

    public int GetCount()
    {
        return count;
    }

    public void Reset()
    {
        count = 0;
    }
}
