using Databox;
using Databox.Dictionary;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPrefType
{
    String,
    Int,
    Float
}


[System.Serializable]
public class PlayerPrefData
{
    [InlineButton("Delete")]
    [HorizontalGroup("Split", 0.5f)]
    [SuffixLabel("PlayerPref Id", Overlay = true)]
    [HideLabel]
    public string playerPrefId;

    [InlineButton("SaveCurrent")]
    [InlineButton("Load")]
    [HorizontalGroup("Split", 0.5f)]
    [HideLabel]
    public PlayerPrefType playerPrefType;


    public bool setNoKey = true;

    [ShowIf("playerPrefType", PlayerPrefType.String)]
    [SuffixLabel("String", Overlay = true)]
    [HideLabel]
    public string stringValue = "";

    [ShowIf("playerPrefType", PlayerPrefType.Int)]
    [SuffixLabel("Int", Overlay = true)]
    [HideLabel]
    public int intValue = 0;

    [ShowIf("playerPrefType", PlayerPrefType.Float)]
    [SuffixLabel("Float", Overlay = true)]
    [HideLabel]
    public float floatValue = 0f;


    public void Init()
    {
        if ( !PlayerPrefs.HasKey(playerPrefId) ) {
            if (setNoKey)
            {
                SaveCurrentValue();
            }
            else {
                return;
            };
        };

        GetCurrentValue();
    }


    public void Load()
    {
        if (!PlayerPrefs.HasKey(playerPrefId))
        {
            if (setNoKey)
            {
                SaveCurrentValue();
            };
        };

        GetCurrentValue();
    }


    public void Delete()
    {
        PlayerPrefs.DeleteKey(playerPrefId);
        
    }

    public void SaveCurrent()
    {
        SaveCurrentValue();
    }


    public void Save(float floatValue)
    {
        PlayerPrefs.SetFloat(playerPrefId, floatValue);

        GetCurrentValue();
    }

    public void Save(int intValue)
    {
        PlayerPrefs.SetInt(playerPrefId, intValue);

        GetCurrentValue();
    }

    public void Save(string stringValue)
    {
        PlayerPrefs.SetString(playerPrefId, stringValue);

        GetCurrentValue();
    }


    public string GetStringValue()
    {
        GetCurrentValue();

        return stringValue;
    }

    public int GetIntValue()
    {
        GetCurrentValue();

        return intValue;
    }

    public float GetFloatValue()
    {
        GetCurrentValue();

        return floatValue;
    }

    public void SaveCurrentValue()
    {
        if (playerPrefType == PlayerPrefType.String)
        {
            PlayerPrefs.SetString(playerPrefId, stringValue);
        }
        else if (playerPrefType == PlayerPrefType.Int)
        {
            PlayerPrefs.SetInt(playerPrefId, intValue);
        }
        else if (playerPrefType == PlayerPrefType.Float)
        {
            PlayerPrefs.SetFloat(playerPrefId, floatValue);
        };
    }

    public void GetCurrentValue()
    {
        if (playerPrefType == PlayerPrefType.String)
        {
            stringValue = PlayerPrefs.GetString(playerPrefId);
        }
        else if (playerPrefType == PlayerPrefType.Int)
        {
            intValue = PlayerPrefs.GetInt(playerPrefId);
        }
        else if (playerPrefType == PlayerPrefType.Float)
        {
            floatValue = PlayerPrefs.GetFloat(playerPrefId);
        };
    }



    public bool CheckIfValid()
    {
        if (!PlayerPrefs.HasKey(playerPrefId))
        {
            return false;
        };

        if (playerPrefType == PlayerPrefType.String)
        {
            return (stringValue == PlayerPrefs.GetString(playerPrefId));
        }
        else if (playerPrefType == PlayerPrefType.Int)
        {
            return (intValue == PlayerPrefs.GetInt(playerPrefId));
        }
        else if (playerPrefType == PlayerPrefType.Float)
        {
            return (floatValue == PlayerPrefs.GetFloat(playerPrefId));
        };

        return false;
    }


}



[System.Serializable]
public class PlayerPrefManager
{
    [Button("Delete All PlayerPrefs")]
    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }



    public List<PlayerPrefData> dataPoints;


    [Title("Database")]
    public string databaseTableName = "";
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public DataboxObject database;


    public PlayerPrefData GetDataPoint(string dataPointId)
    {
        foreach(PlayerPrefData dataPoint in dataPoints)
        {

            if (dataPointId == dataPoint.playerPrefId)
            {
                return dataPoint;
            };
           
        };

        return null;
    }

    public void Init()
    {
        if (database)
        {
            database.LoadDatabase();
            LoadDatabaseDataPoints(databaseTableName);
        }
        else {
            Debug.Log("No PlayerPrefs Database Set.");
        };
        

        dataPoints.ForEach(dataPoint => {
            dataPoint.Init();
        });
    }


    




    public void LoadDatabaseDataPoints(string tableName)
    {

        // First we get a dictionary with all entries from a table
        var _table = database.GetEntriesFromTable(tableName);

        //Debug.Log(_table.Count + " Entries in " + tableName);

        if (_table == null)
        {
            Debug.LogError("PlayerPrefManager.LoadDatabaseDataPoints(" + tableName  + ") > No Table Found....");
            return;
        };

        // Then we iterate through all entries
        foreach (var entry in _table.Keys)
        {
            // Next we get for each entry all values inside of it
            var _values = database.GetValuesFromEntry(tableName, entry);

            Debug.Log(_values.Count + " Values in " + entry);

            var Name = database.GetData<StringType>(tableName, entry, "Name");
            var Type = database.GetData<StringType>(tableName, entry, "Type");
            var InitialValue = database.GetData<StringType>(tableName, entry, "InitialValue");


            PlayerPrefData newDataPoint = new PlayerPrefData
            {
                playerPrefId = Name.Value,
                playerPrefType = (PlayerPrefType)Enum.Parse(typeof(PlayerPrefType), Type.Value),
            };


            if (newDataPoint.playerPrefType == PlayerPrefType.String)
            {
                newDataPoint.stringValue = InitialValue.Value;
            }
            else if (newDataPoint.playerPrefType == PlayerPrefType.Int)
            {
                newDataPoint.intValue = int.Parse(InitialValue.Value);
            }
            else if (newDataPoint.playerPrefType == PlayerPrefType.Float)
            {
                newDataPoint.floatValue = float.Parse(InitialValue.Value);
            };


            dataPoints.Add(newDataPoint);

        }

    }
}
