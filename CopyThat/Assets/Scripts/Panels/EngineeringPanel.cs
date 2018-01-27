using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringPanel : Panel {

    // MEMBERS / FIELDS
    #region Members
    private List<GameObject> panelModules;
    #endregion


    // PROPERTIES
    #region Properties

    #endregion


    // METHODS
    // Use this for initialization
    void Start ()
    {
        // Add all modules that are a child of this panel to the list of modules
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Module>() != null)
            {

            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
