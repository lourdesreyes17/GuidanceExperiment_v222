using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UXF; 

public class StartPointController : MonoBehaviour
{
    // reference to the UXF Session - so we can start the trial.
    public Session session; 

    // define 3 public variables - we can then assign their color values in the inspector.
    public Color startPoint;
    public Color amber;
    public Color green;

    // reference to the material we want to change the color of.
    Material material;

    /// Awake is called when the script instance is being loaded.
    void Awake()
    {
        // get the material that is used to render this object (via the MeshRenderer component)
        material = GetComponent<MeshRenderer>().material;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f); // coroutine does not take affect until 0.5 seconds
        material.color = green;

        if (transform.parent.name == "Mentor Side")
        {
            session.BeginNextTrial(); 
            Debug.Log("New Trial");
        }
        
    }

    /// OnTriggerEnter is called when the Collider 'other' enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        // only do something when we are NOT currently in a trial.
        if (other.name == "NeedleTip" & !session.InTrial)
        {
            material.color = amber;
            StartCoroutine(Countdown());
        }  
    }

    /// OnTriggerExit is called when the Collider 'other' has stopped touching the trigger.
    void OnTriggerExit(Collider other)
    {
        if (other.name == "NeedleTip")
        {
            StopAllCoroutines();
            material.color = startPoint;
        }
    }

    public void PositionStartPoint(Block block)
    {
        float z_pos = session.CurrentBlock.settings.GetFloat("start_z");
        transform.localPosition = new Vector3(0, 0, z_pos);
        Debug.Log("Start point position: " + transform.transform.localPosition);
        Debug.Log("Start point position: " + transform.transform.position);
    }

    public void ResetStartPoint(Block block)
    {
        float z_pos = session.settings.GetFloat("start_z");
        transform.localPosition = new Vector3(0, 0, z_pos);
        Debug.Log("Reset start point");
    }
}
