using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportFileObject : ObjectData<Report>
{
    public FileObject file;

    public Localisation agentComponent;
    public Localisation elementComponent;
    
    void Start()
    {
        GetGameData();

        file.codeName = data.elementName;
        file.type = typeof(Report);
    }

    public override void Protocol()
    {
        if (!file.isFileDisplayed)
        {
            agentComponent.key = data.agentKey;
            agentComponent.RefreshText();

            elementComponent.key = data.elementKey;
            elementComponent.RefreshText();
        }

        base.Protocol();
    }
}
