// 这个是 DisplayPad 的用法
// 这个东西的用处是作为各种显示屏使用
// 假如某个设备有一些信息需要展示，那么你需要在**这个设备**上增加一个脚本，参考本文件的写法完成该脚本
// 然后把一个 DisplayPad 中的 TextMesh 对象拖动到脚本组件的对应属性里面

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPad : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private int count;
    public TextMesh textMesh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        textMesh.text = "Count: " + count.ToString();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            textMesh.text = "Count: " + count.ToString();
        }
    }
}
