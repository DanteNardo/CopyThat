/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/28/2017
/// Purpose: PENLAL
/// </summary>
/// 
using UnityEngine; 

public class PanelManager : Singleton<PanelManager>
{
    public CommsPanel m_commsPanel;
    public SecurityPanel m_securityPanel;
    public EngineeringPanel m_engineeringPanel;
    public MapLogic m_flightPanel;

    private GameObject comsObj;
    private GameObject secObj;
    private GameObject engObj;
    private GameObject flightObj; 

    private void Start()
    {
        m_commsPanel = GetComponentInChildren<CommsPanel>();
        m_securityPanel = GetComponentInChildren<SecurityPanel>();
        m_engineeringPanel = GetComponentInChildren<EngineeringPanel>();
        m_flightPanel = transform.GetChild(2).GetComponentInChildren<MapLogic>();

        comsObj = transform.GetChild(1).gameObject;
        secObj = transform.GetChild(3).gameObject;
        engObj = transform.GetChild(0).gameObject;
        flightObj = transform.GetChild(2).gameObject; 
    }

    public void OpenComs()
    {
        comsObj.SetActive(true); 
    }

    public void OpenSecurity()
    {
        secObj.SetActive(true); 
    }

    public void OpenEng()
    {
        engObj.SetActive(true); 
    }

    public void OpenFlight()
    {
        flightObj.SetActive(true); 
    }

    public void CloseComs()
    {
        comsObj.SetActive(false);
    }

    public void CloseSecurity()
    {
        secObj.SetActive(false);
    }

    public void CloseEng()
    {
        engObj.SetActive(false);
    }

    public void CloseFlight()
    {
        flightObj.SetActive(false);
    }
}
