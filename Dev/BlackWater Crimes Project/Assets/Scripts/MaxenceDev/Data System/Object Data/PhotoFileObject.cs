using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoFileObject : ObjectData<Evidence>
{
    public FileObject file;

    public Image imageComponent;
    
    public Localisation textComponent;
    
    void Start()
    {
        GetGameData();

        file.codeKey = data.nameKey;
        file.type = typeof(Evidence);
    }

    public override void Protocol()
    {
        if (!file.isFileDisplayed)
        {
            data.photo = EvidenceInteraction.CreateSprite(data.photoPath);
            imageComponent.sprite = data.photo;

            textComponent.key = data.nameKey;
            textComponent.RefreshText();
        }

        base.Protocol();
    }
}
