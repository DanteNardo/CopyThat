using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Reperesents the grid of button panels
/// </summary>
public class Panel : MonoBehaviour
{

    // MEMBERS / FIELDS
    #region Members
    public GameObject panelUI;
    #endregion


    // PROPERTIES
    #region Properties

    #endregion


    // METHODS
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Turn the Security UI on or off
    /// </summary>
    /// <param name="active">Should the UI be active? True = yes</param>
    protected void SetUIActive(bool active)
    {
        panelUI.SetActive(active);
    }
}