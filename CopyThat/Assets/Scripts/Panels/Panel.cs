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
    private ModuleType[][] panelGrid;

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
    /// Generate a new panel grid of pieces of size 24x8, and then
    /// determine which pieces should go into the panel
    /// </summary>
    public void GeneratePanelGrid()
    {
        GeneratePanelGrid(24, 8);
    }

    /// <summary>
    /// Generate a new panel grid with a given number of tiles, and then
    /// determine which pieces should go into the panel
    /// </summary>
    /// <param name="xGridLength">Horizontal number of grid spaces</param>
    /// <param name="yGridLength">Vertical number of grid spaces
    /// (should be one or greater)</param>
    public void GeneratePanelGrid(uint xGridLength, uint yGridLength)
    {

    }
}