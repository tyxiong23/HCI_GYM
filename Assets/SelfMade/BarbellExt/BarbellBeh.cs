using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbellBeh : MonoBehaviour
{
    private int count;
    private GameObject S;
    private GameObject M;
    private GameObject L;
    public TextMesh textMesh;
    private bool positionUp;
    private Vector3 originalPos;

    // 信号源
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject head;
    private bool toS;
    private bool toM;
    private bool toL;
    private bool grabFocusedLeft;
    private bool grabFocusedRight;
    private bool configFocused;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        S = this.gameObject.transform.GetChild(0).gameObject;
        M = this.gameObject.transform.GetChild(1).gameObject;
        L = this.gameObject.transform.GetChild(2).gameObject;
        S.SetActive(true);
        M.SetActive(false);
        L.SetActive(false);
        positionUp = false;
        originalPos = transform.position;

        toS = false;
        toM = false;
        toL = false;
        grabFocusedLeft = false;
        grabFocusedRight = false;
        configFocused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 切换重量
        if (IsConfigFocused())
        {
            if (toS)
            {
                S.SetActive(true);
                M.SetActive(false);
                L.SetActive(false);
            }
            else if (toM)
            {
                S.SetActive(false);
                M.SetActive(true);
                L.SetActive(false);
            }
            else if (toL)
            {
                S.SetActive(false);
                M.SetActive(false);
                L.SetActive(true);
            }
        }

        // 被举起
        if (IsGrabFocused())
        {
            Vector3 position = (leftHand.transform.position + rightHand.transform.position) / 2;
            float rotateX = - Mathf.Rad2Deg * Mathf.Atan(
                (leftHand.transform.position.y - rightHand.transform.position.y) /
                (leftHand.transform.position.z - rightHand.transform.position.z)
            );
            float rotateY = Mathf.Rad2Deg * Mathf.Atan(
                (leftHand.transform.position.x - rightHand.transform.position.x) /
                (leftHand.transform.position.z - rightHand.transform.position.z)
            );

            transform.SetPositionAndRotation(position, Quaternion.identity);
            transform.Rotate(new Vector3(rotateX, 0.0f, 0.0f));
            transform.Rotate(new Vector3(0.0f, rotateY, 0.0f));

            // 计数
            ChangeCount();
        }
        else
        {
            Vector3 to = new Vector3(transform.position.x, originalPos.y, transform.position.z);
            transform.SetPositionAndRotation(to, Quaternion.identity);
        }

        // 渲染文字
        RenderText();
    }


    // ----- behaviour ----- //
    void RenderText()
    {
        textMesh.text = count.ToString() + "\n";
        if (IsGrabFocused())
        {
            textMesh.text += "grab\n";
        }
        if (IsConfigFocused())
        {
            textMesh.text += "config\n";
        }
        // test
        //textMesh.text += Vector3.Distance(transform.position, leftHand.transform.position).ToString() + "\n";
        //textMesh.text += Vector3.Distance(transform.position, rightHand.transform.position).ToString() + "\n";
        //textMesh.text += transform.position.y.ToString() + "\n";
    }

    void ChangeCount()
    {
        if (positionUp)
        {
            if (transform.position.y < 1.05f)
                positionUp = false;
        }
        else
        {
            if (transform.position.y > 1.3f)
            {
                positionUp = true;
                count += 1;
            }
        }
    }

    bool IsGrabFocused()
    {
        float deltaL = transform.position.y - leftHand.transform.position.y;
        if (deltaL < 0)
            deltaL = - deltaL;
        float deltaR = transform.position.y - rightHand.transform.position.y;
        if (deltaR < 0)
            deltaR = - deltaR;

        return grabFocusedLeft && grabFocusedRight  // 手势正确
            // 双手足够近
            && Vector3.Distance(transform.position, leftHand.transform.position) < 0.5f
            && Vector3.Distance(transform.position, rightHand.transform.position) < 0.5f;
            // 高度差有更精细的要求
            //&& deltaL < 0.25 && deltaR < 0.25;
    }

    bool IsConfigFocused()
    {
        return configFocused  // 视线对准
            // 头足够近
            && Vector3.Distance(transform.position, head.transform.position) < 1.5f;
    }


    // ----- setters ----- //
    public void SetToS(bool flag)
    {
        toS = flag;
    }

    public void SetToM(bool flag)
    {
        toM = flag;
    }

    public void SetToL(bool flag)
    {
        toL = flag;
    }

    public void SetGrabFofusedLeft(bool flag)
    {
        grabFocusedLeft = flag;
    }

    public void SetGrabFofusedRight(bool flag)
    {
        grabFocusedRight = flag;
    }

    public void SetConfigFocused(bool flag)
    {
        configFocused = flag;
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
