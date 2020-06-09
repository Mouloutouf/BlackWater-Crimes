using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateReportElements : InstantiateElements<Report>
{
    [Title("Report References", horizontalLine: false)]

    public NotificationSystem notificationSystem;

    protected override List<List<Report>> GetAllElements()
    {
        List<List<Report>> mainList = new List<List<Report>>();

        List<Report> allReports = new List<Report>();

        foreach ((List<Report>, List<Report>) reportsList in gameData.reports.Values)
        {
            foreach (Report report in reportsList.Item1)
            {
                allReports.Add(report);
            }
        }

        mainList.Add(allReports);

        return mainList;
    }

    protected override void OrderElements()
    {
        allData = allData.OrderBy(w => w.unlockOrderIndex).ToList();
        allData.Reverse();
    }
    
    protected override void AdditionalSettings(GameObject __prefab)
    {
        __prefab.GetComponent<RectTransform>().offsetMin = new Vector2(15, __prefab.GetComponent<RectTransform>().offsetMin.y);
        __prefab.GetComponent<RectTransform>().offsetMax = new Vector2(-15, __prefab.GetComponent<RectTransform>().offsetMax.y);

        __prefab.GetComponent<NotificationReport>().notificationSystem = notificationSystem;
    }
    
    protected override bool Compare(Indics _indic, Report data)
    {
        Indic indic = gameData.indics[_indic];

        bool check = data.agentKey == indic.nameKey;

        return check;
    }

    protected override string GetDataName(Report data)
    {
        return data.elementKey;
    }
    
    protected override void SetLayout()
    {
        float sizeX = contents[0].GetComponent<RectTransform>().rect.width;
        float sizeY = contents[0].GetComponent<RectTransform>().rect.height / amountInEachColumn;

        float posX;
        float posY;
        
        for (int w = 0; w < amountInEachColumn; w++)
        {
            posY = (sizeY / 2) + sizeY * w;
            posX = (sizeX / 2);

            spawnPoints.Add(new Vector2(posX, -posY));
        }
    }
}
