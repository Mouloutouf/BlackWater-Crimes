using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavingData
{
    public object dataVariable;

    public string dataName;
}

public class SaveSystem : MonoBehaviour
{
    public GameData gameData;

    private Emotions myEmotion;

    public Dictionary<string, object> savedData = new Dictionary<string, object>();

    public void WriteData<T>(T type, object variable, string name)
    {
        if (type.GetType() == typeof(int)){
            int intValue = (int) variable;
            PlayerPrefs.SetInt(name, intValue);

        } else if (type.GetType() == typeof(string)){
            string stringValue = (string) variable;
            PlayerPrefs.SetString(name, stringValue);

        } else if (type.GetType() == typeof(bool)){
            bool boolVar = (bool) variable;
            int value = boolVar == false ? 0 : 1;
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

    public void SaveData()
    {
        PlayerPrefs.DeleteAll();

        foreach (SavingData savingData in gameData.dataToStore)
        {
            Type dataType = savingData.dataVariable.GetType();
            WriteData(dataType, savingData.dataVariable, savingData.dataName);
        }
    }

    public void LoadData()
    {
        foreach (SavingData savingData in gameData.dataToStore)
        {
            Type dataType = savingData.dataVariable.GetType();
            savingData.dataVariable = RetrieveData(dataType, savingData.dataVariable, savingData.dataName);
        }
    }
}
