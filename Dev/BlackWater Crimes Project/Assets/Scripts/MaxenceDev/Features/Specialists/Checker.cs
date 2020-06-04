﻿using System.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class IndicsText
{
    public Indics indic;

    public string introKey;

    public string successKey;
    public string failureKey;
}

public class Checker : MonoBehaviour
{
    protected Localisation dialogueKey;
    
    protected Button validateButton;

    protected GameData gameData;

    public string checkedName { get; set; }

    [HideInInspector] public Sprite checkedImage; // get; set; autoproperty maybe

    public Indics indic { get { return gameData.currentIndic; } }
    
    private bool match;
    
    [SerializeField] private List<IndicsText> allIndicsText;
    
    void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;

        dialogueKey = GameObject.Find("Dialogue Text").GetComponent<Localisation>();
        
        foreach (IndicsText indicsText in allIndicsText)
        {
            if (indicsText.indic == gameData.currentIndic)
            {
                dialogueKey.key = indicsText.introKey;
                dialogueKey.RefreshText();
                break;
            }
        }

        validateButton = GameObject.Find("Validate Button").GetComponent<Button>();
        validateButton.onClick.AddListener(delegate { SendEvent(); });
    }

    public virtual void SendEvent()
    {
        GetCheckedElements();

        Send();
    }

    protected void Send()
    {
        foreach (Indics indic in gameData.megaReports.Keys)
        {
            foreach (Report report in gameData.megaReports[indic].Item1)
            {
                if (Check(indic, report))
                {
                    UnlockReport(report);
                    match = true;
                }
            }
        }

        if (!match) UnlockFailedReport();

        ResetField();

        Debug.Log(match);

        match = false;
    }

    protected void Send(List<Intel> intelList)
    {
        foreach (Indics indic in gameData.megaReports.Keys)
        {
            foreach (Report report in gameData.megaReports[indic].Item1)
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

        ResetField();

        match = false;
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

    public virtual void UnlockReport(Report _report)
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
        Report t_Report = gameData.megaReports[indic].Item2[0];

        Report f_Report = new Report
        {
            failed = true,
            unlockedData = true,
            elementSprite = checkedImage,
            elementName = checkedName,
            
            index = gameData.megaReports[indic].Item2.Count,

            agentName = t_Report.agentName,
            agentSprite = t_Report.agentSprite,
            reportText = t_Report.reportText,
            signature = t_Report.signature,

            elementKey = ""
        };

        gameData.megaReports[indic].Item2.Add(f_Report);
    }
    
    public virtual void ResetField()
    {
        foreach (IndicsText indicText in allIndicsText)
        {
            if (indicText.indic == gameData.currentIndic)
            {
                if (match) dialogueKey.key = indicText.successKey;
                else dialogueKey.key = indicText.failureKey;
                dialogueKey.RefreshText();

                break;
            }
        }
    }
} 