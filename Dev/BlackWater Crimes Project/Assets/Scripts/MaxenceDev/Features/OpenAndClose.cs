using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndClose : MonoBehaviour
{
    private bool isClosed = true;

    public void Open(GameObject gameObject, bool childs)
    {
        if (!childs) gameObject.SetActive(true);
        else
        {
            foreach (Transform tr in gameObject.transform) tr.gameObject.SetActive(true);
        }
    }

    public void Close(GameObject gameObject, bool childs)
    {
        if (!childs) gameObject.SetActive(false);
        else
        {
            foreach (Transform tr in gameObject.transform) tr.gameObject.SetActive(false);
        }
    }

    public void OpenOrClose(GameObject _gameObject)
    {
        if (isClosed) { Open(_gameObject, false); isClosed = false; }

        else { Close(_gameObject, false); isClosed = true; }
    }

    public void OpenOrCloseWithChilds(GameObject _gameObject)
    {
        if (isClosed) { Open(_gameObject, true); isClosed = false; }

        else { Close(_gameObject, true); isClosed = true; }
    }
}
