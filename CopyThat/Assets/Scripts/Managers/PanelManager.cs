/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/28/2017
/// Purpose: PENLAL
/// </summary>
public class PanelManager : Singleton<PanelManager>
{
    public CommsPanel m_commsPanel;
    public SecurityPanel m_securityPanel;
    public EngineeringPanel m_engineeringPanel;
    public MapLogic m_flightPanel;

    private void Start()
    {
        m_commsPanel = GetComponentInChildren<CommsPanel>();
        m_securityPanel = GetComponentInChildren<SecurityPanel>();
        m_engineeringPanel = GetComponentInChildren<EngineeringPanel>();
        m_flightPanel = GetComponent<MapLogic>();
    }
}
