using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileObject : MonoBehaviour
{
    [HideInInspector] public string codeKey;
    [HideInInspector] public FileType fileType;

    [HideInInspector] public bool isFileDisplayed = false;
}
