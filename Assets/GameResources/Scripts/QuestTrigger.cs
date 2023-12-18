using Naninovel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public string questVar;

    public void changeGui()
    {
        Debug.Log("test");
        if (Engine.GetService<ICustomVariableManager>().GetVariableValue(questVar) == "Get")
        {
            gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else if (Engine.GetService<ICustomVariableManager>().GetVariableValue(questVar) == "Complete")
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        }
        else if (Engine.GetService<ICustomVariableManager>().GetVariableValue(questVar) == "true")
        {
            Destroy(gameObject.transform.parent.transform.gameObject);
        }
    }
    // Update is called once per frame

}
