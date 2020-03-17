using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndClose : MonoBehaviour
{
    private bool isClosed = true;

    public void Open(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void OpenOrClose(GameObject _gameObject)
    {
        if (isClosed) { Open(_gameObject); isClosed = false; }

        else { Close(_gameObject); isClosed = true; }
    }
}
