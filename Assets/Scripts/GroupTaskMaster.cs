using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroupTaskMaster : MonoBehaviour
{
    List<int> taskDict = new List<int>();
    bool allOcc;
    bool working = false;

    private int id;
    private bool taskDone = false;

    Coroutine fixing;

    public int taskDuration;
    public string taskName;

    [System.Serializable]
    public class MyEvent : UnityEvent<int> { } //Requirement to call Invoke with an arg
    public MyEvent taskTrigger;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(Transform c in transform) //loops through children and sets an individual id to each node
        {
            c.gameObject.GetComponent<GroupTaskScript>().setSubId(i);
            taskDict.Add(0);
            i += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator doTask() //same shit as taskscript
    {
        yield return new WaitForSeconds(taskDuration);

        if (!taskDone)
        {
            foreach (Transform c in transform)
            {
                c.gameObject.GetComponent<GroupTaskScript>().taskFinish();
            }
            taskDone = true;
            taskTrigger.Invoke(id);
            Debug.Log("Group Task Finished!");
        }
    }

    public void numInc(int subId) //increments the number of people at a certain node and checks to see if there is at least 1 at every node
    {
        taskDict[subId] = taskDict[subId] + 1;
        beginTask();
    }

    public void numDec(int subId) //decrements the number of people at a certain node and checks to see if that node now has 0 people
    {
        taskDict[subId] = taskDict[subId] - 1;
        if (taskDict[subId] <= 0)
        {
            StopCoroutine(fixing);
            Debug.Log("Group Task Interrupted!");
            working = false;
        }
    }

    public void beginTask() //loops through each node to see if there are any empty ones, otherwise start the task
    {
        allOcc = true;
        for (int x = 0; x < taskDict.Count; x += 1)
        {
            if (taskDict[x] <= 0)
            {
                allOcc = false;
                break;
            }
        }
        if (allOcc && !working && !taskDone)
        {
            fixing = StartCoroutine(doTask());
            Debug.Log("Group Task Started!");
            working = true;
        }
    }

    public void setId(int x)
    {
        id = x;
    }
}
