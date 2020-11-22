using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupTaskScript : MonoBehaviour
{
    private int id;
    private bool taskDone = false;

    GameObject master;

    public GameObject taskLight;

    // Start is called before the first frame update
    void Start()
    {
        master = transform.parent.gameObject; //sets the "master" node of the group task
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) //When an object tagged as a Player enters a radius around the task, increments number of people near the task by 1
    {
        if (other.tag == "Player" && !taskDone)
        {
            master.gameObject.GetComponent<GroupTaskMaster>().numInc(id);
        }
    }

    private void OnTriggerExit(Collider other) //If a player leaves the radius of the task, decrements the number of people by 1
    {
        if (other.tag == "Player" && !taskDone)
        {
            master.gameObject.GetComponent<GroupTaskMaster>().numDec(id);
        }
    }

    public void taskFinish()
    {
        if (!taskDone)
        {
            taskLight.SetActive(true);
            taskDone = true;
        }
    }

    public void setSubId(int x)
    {
        id = x;
    }
}
