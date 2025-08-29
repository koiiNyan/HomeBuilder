using UnityEngine;
using System.IO;

public static class SaveManager
{
    private const string SAVE_FILE_NAME = "gamesave.json";

    [System.Serializable]
    public class SaveData
    {
        public string levelName;
        //lvl01
        public bool bedCompleted;
        public bool bookshelfCompleted;
        public bool deskCompleted;
        public bool chairCompleted;
        public bool rugCompleted;
        public bool posterCompleted;
        public bool sofaCompleted;
        public bool tvCompleted;
        public bool IsBedBookCompleted;
        public bool IsDeskChairCompleted;
        public bool IsRugPosterCompleted;
        public bool IsSofaTVCompleted;
        //lvl02
        public bool bathCompleted;
        public bool sinkCompleted;
        public bool toiletCompleted;
        public bool IsBathSinkToiletCompleted;

        public bool washerCompleted;
        public bool carpetCompleted;
        public bool towelCompleted;
        public bool IsWasherCarpetTowelCompleted;
    }

    public static bool HasSaveFile()
    {
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        return File.Exists(path);
    }

    public static void SaveGame(string levelName)
    {
        SaveData saveData = new SaveData();
        saveData.levelName = levelName;

        Debug.Log($"SaveManager: Saving game for level {levelName}");

        if (levelName == "Level01")
        {

            GameManager gameManager = Object.FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("SaveManager: GameManager not found!");
                return;
            }


            saveData.bedCompleted = gameManager.GetBedCompleted();
            saveData.bookshelfCompleted = gameManager.GetBookshelfCompleted();
            saveData.deskCompleted = gameManager.GetDeskCompleted();
            saveData.chairCompleted = gameManager.GetChairCompleted();
            saveData.rugCompleted = gameManager.GetRugCompleted();
            saveData.posterCompleted = gameManager.GetPosterCompleted();
            saveData.sofaCompleted = gameManager.GetSofaCompleted();
            saveData.tvCompleted = gameManager.GetTVCompleted();
            saveData.IsBedBookCompleted = gameManager.GetIsBedBookCompleted();
            saveData.IsDeskChairCompleted = gameManager.GetIsDeskChairCompleted();
            saveData.IsRugPosterCompleted = gameManager.GetIsRugPosterCompleted();
            saveData.IsSofaTVCompleted = gameManager.GetIsSofaTVCompleted();
        }
        else if (levelName == "Level02")
        {
            GameManager_Lvl02 gameManager = Object.FindObjectOfType<GameManager_Lvl02>();
            if (gameManager == null)
            {
                Debug.LogError("SaveManager: GameManager not found!");
                return;
            }

            /// lvl01 all true



            saveData.bathCompleted = gameManager.GetBathCompleted();
            saveData.sinkCompleted = gameManager.GetSinkCompleted();
            saveData.toiletCompleted = gameManager.GetToiletCompleted();      
            saveData.IsBathSinkToiletCompleted = gameManager.GetIsBathSinkToiletCompleted();

            saveData.washerCompleted = gameManager.GetWasherCompleted();
            saveData.carpetCompleted = gameManager.GetCarpetCompleted();
            saveData.towelCompleted = gameManager.GetTowelCompleted();
            saveData.IsWasherCarpetTowelCompleted = gameManager.GetIsWasherCarpetTowelCompleted();

        }


        else if (levelName == "Level03")
        {
            GameManager_Lvl02 gameManager = Object.FindObjectOfType<GameManager_Lvl02>();
            if (gameManager == null)
            {
                Debug.LogError("SaveManager: GameManager not found!");
                return;
            }

            /// lvl01 all true

            /// lvl02 all true

            saveData.bathCompleted = gameManager.GetBathCompleted();
            saveData.sinkCompleted = gameManager.GetSinkCompleted();
            saveData.toiletCompleted = gameManager.GetToiletCompleted();
            saveData.IsBathSinkToiletCompleted = gameManager.GetIsBathSinkToiletCompleted();

            saveData.washerCompleted = gameManager.GetWasherCompleted();
            saveData.carpetCompleted = gameManager.GetCarpetCompleted();
            saveData.towelCompleted = gameManager.GetTowelCompleted();
            saveData.IsWasherCarpetTowelCompleted = gameManager.GetIsWasherCarpetTowelCompleted();

        }



        string json = JsonUtility.ToJson(saveData, true);
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        File.WriteAllText(path, json);

        Debug.Log("Save file created at: " + path);
        Debug.Log("Save data JSON:\n" + json);
    }

    public static SaveData LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        Debug.Log($"Attempting to load save file from: {path}");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log($"Save file contents: {json}");
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"Save data loaded. Level: {data.levelName}");
            return data;
        }
        Debug.LogWarning("No save file found");
        return null;
    }
}
