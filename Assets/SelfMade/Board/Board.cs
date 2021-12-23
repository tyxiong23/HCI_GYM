using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public TextMesh textMesh;
    private int countBarbell;
    private int countDumbbell;
    private int countTreadMill;

    public GameObject barbell;
    public GameObject runner;
    public GameObject dumbbell;

    // Start is called before the first frame update
    void Start()
    {
        countBarbell = 0;
        countDumbbell = 0;
        countTreadMill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countBarbell = barbell.GetComponent<BarbellBeh>().GetCount();
        countDumbbell = dumbbell.GetComponent<Dumbbell>().GetCount();
        countTreadMill = runner.GetComponent<RunnerTreadmill>().GetDistance();

        textMesh.text = "今日成果：\n";
        textMesh.text += "杠铃：" + countBarbell.ToString() + "次\n";
        textMesh.text += "哑铃深蹲：" + countDumbbell.ToString() + "次\n";
        textMesh.text += "跑步：" + countTreadMill.ToString() + "米\n";
    }
}
