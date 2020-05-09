using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checker : MonoBehaviour
{
    protected Text dialogueText;

    [SerializeField] string introText;
    [SerializeField] string validateText;

    protected Button validateButton;

    protected GameData gameData;

    public string checkedName { get; set; }

    public Sprite checkedImage { get; set; }

    public Indics indic;
    
    private bool match;

    void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;

        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
        dialogueText.text = introText;

        validateButton = GameObject.Find("Validate Button").GetComponent<Button>();
        validateButton.onClick.AddListener(delegate { SendEvent(); });
    }

    public virtual void SendEvent()
    {
        Send();
    }

    protected void Send()
    {
        GetCheckedElements();

        foreach (Indics indic in gameData.allReports.Keys)
        {
            foreach (Report report in gameData.allReports[indic])
            {
                if (Check(indic, report))
                {
                    if (report.elementDetailName == null)
                    {
                        UnlockReport(report);
                        match = true;
                    }
                }
            }
        }

        if (!match) UnlockFailedReport();

        match = false;

        ResetField();
    }

    protected void Send(List<Intel> intelList)
    {
        GetCheckedElements();

        foreach (Indics indic in gameData.allReports.Keys)
        {
            foreach (Report report in gameData.allReports[indic])
            {
                if (Check(indic, report))
                {
                    if (report.elementDetailName == null)
                    {
                        UnlockReport(report);
                        match = true;
                    }

                    else
                    {
                        foreach (Intel intel in intelList)
                        {
                            if (intel.revealed && report.elementDetailName == intel.name)
                            {
                                UnlockReport(report);
                                match = true;
                            }
                        }
                    }
                }
            }
        }

        if (!match) UnlockFailedReport();

        match = false;

        ResetField();
    }

    public virtual void GetCheckedElements() { }

    public virtual bool Check(Indics _indic, Report _report)
    {
        bool check = true;

        if (_indic != this.indic) check = false;

        if (_report.elementName != checkedName) check = false;

        if (_report.index == 0) check = false;

        return check;
    }

    void UnlockReport(Report _report)
    {
        if (!_report.unlockedData)
        {
            _report.unlockedData = true;
            gameData.reportsCollected++;
            _report.unlockOrderIndex = gameData.reportsCollected;

            gameData.newStuff = true;
        }
    }

    void UnlockFailedReport()
    {
        Report t_Report = gameData.allReports[indic][0];

        Report f_Report = new Report
        {
            failed = true,
            unlockedData = true,
            elementSprite = checkedImage,
            elementName = checkedName,
            
            index = gameData.allReports.Count,

            agentName = t_Report.agentName,
            agentSprite = t_Report.agentSprite,
            reportText = t_Report.reportText,
            signature = t_Report.signature
        };

        gameData.allReports[indic].Add(f_Report);
    }
    
    public virtual void ResetField()
    {
        dialogueText.text = validateText;
    }
} 