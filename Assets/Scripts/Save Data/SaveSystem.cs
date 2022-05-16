using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(int _slot, DataManager manager) //Para guardar datos debes pasar el slot donde lo guardaras
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata_" + _slot.ToString() + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        //Debug.Log(path);
        Data data = new Data(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void DeleteData(int _slot)
    {
        string path = Application.persistentDataPath + "/savedata_" + _slot.ToString() + ".bin";
        string path_progress = Application.persistentDataPath + "/gameprogress_" + _slot.ToString() + ".bin";
        File.Delete(path);
        File.Delete(path_progress);
    }

    public static bool CheckFileExist(int _slot)
    {
        string path = Application.persistentDataPath + "/savedata_" + _slot.ToString() + ".bin";

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CheckSettingsFileExist()
    {
        string path = Application.persistentDataPath + "/settings" + ".bin";

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Data LoadData(int _slot)
    {
        string path = Application.persistentDataPath + "/savedata_" + _slot.ToString() + ".bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveSettings(DataManager manager) //Para guardar datos debes pasar el slot donde lo guardaras
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings" + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        //Debug.Log(path);
        Settings settings = new Settings(manager);

        formatter.Serialize(stream, settings);
        stream.Close();
    }

    public static Settings LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings" + ".bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Settings settings = formatter.Deserialize(stream) as Settings;
            stream.Close();

            return settings;
        }
        else
        {
            Debug.LogError("Settings file not found");
            return null;
        }
    }
}
