using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action { Save, Load }

[Serializable]
public class SaveData
{
    public Type dataType;

    public object dataVariable;

    public string dataName;
}

public class SaveSystem
{
    public void WriteData(Type type, object variable, string name)
    {
        if (type == typeof(int)){
            int intValue = (int) variable;
            PlayerPrefs.SetInt(name, intValue);

        } else if (type == typeof(string)){
            string stringValue = (string) variable;
            PlayerPrefs.SetString(name, stringValue);

        } else if (type == typeof(bool)){
            bool boolValue = (bool) variable;
            int value = boolValue == false ? 0 : 1;
            PlayerPrefs.SetInt(name, value);

        } else if (type == typeof(float)){
            float floatValue = (float) variable;
            PlayerPrefs.SetFloat(name, floatValue);

        } else if (type == typeof(Languages)){
            Languages languageValue = (Languages) variable;
            int value = languageValue == Languages.English ? 0 : 1;
            PlayerPrefs.SetInt(name, value);

        } else Debug.Log("The saved variable Type did not match with any of the supported Types, the variable was not saved");
    }
    
    public object RetrieveData(Type type, object variable, string name)
    {
        if (type == typeof(int)){
            int var = PlayerPrefs.GetInt(name);
            return var;

        } else if (type == typeof(string)){
            string var = PlayerPrefs.GetString(name);
            return var;

        } else if (type == typeof(bool)){
            int var = PlayerPrefs.GetInt(name);
            bool _var = var == 0 ? false : true;
            return _var;

        } else if (type == typeof(float)){
            float var = PlayerPrefs.GetFloat(name);
            return var;

        } else if (type == typeof(Languages)){
            int var = PlayerPrefs.GetInt(name);
            Languages _var = var == 0 ? Languages.English : Languages.French;
            return _var;

        } else { Debug.Log("The loaded variable Type did not match with any of the supported Types, the variable was returned unchanged"); return variable; }
    }

    public void SaveDataInPrefs(GameData _gameData)
    {
        PlayerPrefs.DeleteAll();

        foreach (SaveData saveData in _gameData.savedData)
        {
            WriteData(saveData.dataType, saveData.dataVariable, saveData.dataName);

            //Debug.Log(saveData.dataName + " : " + saveData.dataVariable + ", of type : " + saveData.dataType + ", was saved !");
        }
    }

    public void LoadDataFromPrefs(GameData _gameData)
    {
        foreach (SaveData saveData in _gameData.savedData)
        {
            saveData.dataVariable = RetrieveData(saveData.dataType, saveData.dataVariable, saveData.dataName);

            //Debug.Log(saveData.dataName + " : " + saveData.dataVariable + ", of type : " + saveData.dataType + ", has been loaded !");
        }
    }
}
