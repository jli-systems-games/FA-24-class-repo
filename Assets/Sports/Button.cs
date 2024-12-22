using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void zSceneing()
    {
        GameObject foundObject = GameObject.Find("SportSwitcher");

        GameObject.Find("SportSwitcher").GetComponent<SceneChanger>().Counter();


        // Ensure the object was found to avoid null reference errors
        if (foundObject != null)
        {
            // Get the script component (replace `YourScript` with the actual script name)
            SceneChanger script = foundObject.GetComponent<SceneChanger>();

            if (script != null)
            {
                // Call the method on the script
                script.Counter();
            }
            else
            {
                Debug.LogError("Script 'YourScript' not found on ObjectName");
            }
        }
        else
        {
            Debug.LogError("GameObject 'ObjectName' not found in the scene");
        }

    }
}
