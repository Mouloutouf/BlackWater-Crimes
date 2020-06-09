using System.Collections;
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

    public AudioClip introAudio;
    public AudioClip successAudio;
    public AudioClip failureAudio;
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

    SoundSystem soundSystem;
    
    void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;

        dialogueKey = GameObject.Find("Dialogue Text").GetComponent<Localisation>();

        soundSystem = GameObject.FindObjectOfType<SoundSystem>();
        
        foreach (IndicsText indicsText in allIndicsText)
        {
            if (indicsText.indic == gameData.currentIndic)
            {
                dialogueKey.key = indicsText.introKey;
                dialogueKey.RefreshText();
                soundSystem.PlayVoice(indicsText.introAudio);
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
        foreach (Indics indic in gameData.reports.Keys)
        {
            foreach (Report report in gameData.reports[indic].Item1)
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
        foreach (Indics indic in gameData.reports.Keys)
        {
            foreach (Report report in gameData.reports[indic].Item1)
            {
                if (Check(indic, report))
                {
                    if (report.detailKey == null)
                    {
                        UnlockReport(report);
                        match = true;
                    }

                    else
                    {
                        foreach (Intel intel in intelList)
                        {
                            if (intel.revealed && report.detailKey == intel.intelKey)
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

        if (_report.elementKey != checkedName) check = false;

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
        Report t_Report = gameData.reports[indic].Item2[0];

        Report f_Report = new Report
        {
            failed = true,
            unlockedData = true,
            elementSprite = checkedImage,
            elementKey = checkedName,
            
            index = gameData.reports[indic].Item2.Count,

            agentKey = t_Report.agentKey,
            agentSprite = t_Report.agentSprite,
            reportKey = t_Report.reportKey,
            signature = t_Report.signature
        };

        gameData.reports[indic].Item2.Add(f_Report);
    }
    
    public virtual void ResetField()
    {
        foreach (IndicsText indicText in allIndicsText)
        {
            if (indicText.indic == gameData.currentIndic)
            {
                if (match) 
                {
                    dialogueKey.key = indicText.successKey; 
                    soundSystem.PlayVoice(indicText.successAudio);
                }
                else 
                {
                    dialogueKey.key = indicText.failureKey;
                    soundSystem.PlayVoice(indicText.failureAudio);
                }
                dialogueKey.RefreshText();

                break;
            }
        }
    }
} 