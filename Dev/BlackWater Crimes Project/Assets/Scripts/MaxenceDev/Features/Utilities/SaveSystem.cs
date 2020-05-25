using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action { Save, Load }

[Serializable]
public class SaveData
{
    public object dataVariable;

    public string dataName;
}

public class SaveSystem
{
    public void WriteData<T>(T type, object variable, string name)
    {
        if (type.GetType() == typeof(int)){
            int intValue = (int) variable;
            PlayerPrefs.SetInt(name, intValue);

        } else if (type.GetType() == typeof(string)){
            string stringValue = (string) variable;
            PlayerPrefs.SetString(name, stringValue);

        } else if (type.GetType() == typeof(bool)){
            bool boolValue = (bool) variable;
            int value = boolValue == false ? 0 : 1;
            PlayerPrefs.SetInt(name, value);

        } else if (type.GetType() == typeof(float)){
            float floatValue = (float) variable;
            PlayerPrefs.SetFloat(name, floatValue);
        }
    }
    
    public T RetrieveData<T>(T type, object variable, string name) where T : Type
    {
        if (type.GetType() == typeof(int)){
            variable = PlayerPrefs.GetInt(name);
            int var = (int) variable;
            return var as T;

        } else if (type.GetType() == typeof(string)){
            variable = PlayerPrefs.GetString(name);
            string var = (string) variable;
            return var as T;

        } else if (type.GetType() == typeof(bool)){
            variable = PlayerPrefs.GetInt(name);
            int var = (int) variable;
            bool _var = var == 0 ? false : true;
            return _var as T;

        } else if (type.GetType() == typeof(float)){
            variable = PlayerPrefs.GetFloat(name);
            float var = (float) variable;
            return var as T;
        }
        else return variable as T;
    }

    public void SaveDataList(GameData _gameData)
    {
        PlayerPrefs.DeleteAll();

        foreach (SaveData saveData in _gameData.savedData)
        {
            Type dataType = saveData.dataVariable.GetType();
            WriteData(dataType, saveData.dataVariable, saveData.dataName);

            Debug.Log(saveData.dataName + " : " + saveData.dataVariable + ", was saved !");
        }
    }

    public void LoadDataList(GameData _gameData)
    {
        foreach (SaveData saveData in _gameData.savedData)
        {
            Type dataType = saveData.dataVariable.GetType();
            saveData.dataVariable = RetrieveData(dataType, saveData.dataVariable, saveData.dataName);

            Debug.Log(saveData.dataName + " : " + saveData.dataVariable + ", has been loaded !");
        }
    }
}
