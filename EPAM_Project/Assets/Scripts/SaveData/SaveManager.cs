﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Services;
using UnityEngine;

namespace SaveData
{
    public static class SaveManager
    {
        public static void Save()
        {
            //SaveGameData(new GameData(PlayerSaveData()));
        }

        public static void Load()
        {
            var gameData = LoadGameData();
           // PlayerLoadData(gameData.playerSaveData); 
        }

        //private static PlayerSaveData PlayerSaveData() => ServiceLocator.Instance.Get<PlayerManager>().Data.GetSaveData();
        //private static void PlayerLoadData(PlayerSaveData saveData) => ServiceLocator.Instance.Get<PlayerManager>().Data.LoadData(saveData);

        private static void SaveGameData(GameData data)
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/saveData.dat";

            using var stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, data);
        }
        
        private static GameData LoadGameData()
        {
            var path = Application.persistentDataPath + "/saveData.dat";

            if (!File.Exists(path)) 
                throw new FileNotFoundException();
            
            var formatter = new BinaryFormatter();

            using var stream = new FileStream(path, FileMode.Open);
            return formatter.Deserialize(stream) as GameData;
        }
    }
}