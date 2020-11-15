using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskScript : MonoBehaviour
{
    private int id;
    private bool taskDone = false;

    Coroutine fixing;
    IDictionary<GameObject, Coroutine> playerDict = new Dictionary<GameObject, Coroutine>(); //This dictionary just associates a player with their "progress" on the task

    public int taskDuration;
    public string taskName;
    public GameObject taskLight;

    [System.Serializable]
    public class MyEvent : UnityEvent<int> { } //Requirement to call Invoke with an arg
    public MyEvent taskTrigger;

    private void OnTriggerEnter(Collider other) //When an object tagged as a Player enters a radius around the task, task automatically started
    {
        if (other.tag == "Player" && !playerDict.ContainsKey(other.gameObject) && !taskDone)
        {
            fixing = StartCoroutine(doTask());
            playerDict.Add(other.gameObject, fixing);
            Debug.Log("Task Started!");
        }
    }

    private void OnTriggerExit(Collider other) //If a player leaves the radius while their task is happening, only their task progress is cancelled; Player 1 leaving does not interrupt Player 2
    {
        if (other.tag == "Player" && playerDict.ContainsKey(other.gameObject) && !taskDone)
        {
            StopCoroutine(playerDict[other.gameObject]);
            playerDict.Remove(other.gameObject);
            Debug.Log("Task Interrupted!");
        }
    }

    IEnumerator doTask()
    {
        yield return new WaitForSeconds(taskDuration);

        if (!taskDone)
        {
            taskLight.SetActive(true); //Green light means done
            taskDone = true;
            playerDict.Clear();
            Debug.Log("Task Finished!");
            taskTrigger.Invoke(id);
        }
    }

    public void setId(int x)
    {
        id = x;
    }
}
