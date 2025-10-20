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
        public bool washerCompleted;
        public bool carpetCompleted;
        public bool towelCompleted;

        public bool IsBathSinkToiletCompleted;
        public bool IsWasherCarpetTowelCompleted;

        //lvl03
        public bool kitchenCompleted;
        public bool cookerCompleted;
        public bool fridgeCompleted;
        public bool chair01Completed;
        public bool chair02Completed;
        public bool chair03Completed;
        public bool chair04Completed;
        public bool tableCompleted;
        public bool tableItemsCompleted;
        public bool vaseCompleted;
        public bool microwaveCompleted;
        public bool panCompleted;
        public bool dishesCompleted;
        public bool glass01Completed;
        public bool glass02Completed;
        public bool glass03Completed;
        public bool glass04Completed;
        public bool glass05Completed;

        public bool IsKitchenCookerFridgeCompleted;
        public bool IsChair01Chair02Chair03Completed;
        public bool IsChair04TableTableItemsCompleted;
        public bool IsVaseMicrowavePanCompleted;
        public bool IsDishesGlass01Glass02Completed;
        public bool IsGlass03Glass04Glass05Completed;
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


            SaveData saveDataPrev = LoadGame();
            // if prev save data existed
            if (saveDataPrev != null)
            {
                //lvl02
                saveData.bathCompleted = saveDataPrev.bathCompleted;
                saveData.sinkCompleted = saveDataPrev.sinkCompleted;
                saveData.toiletCompleted = saveDataPrev.toiletCompleted;

                saveData.washerCompleted = saveDataPrev.washerCompleted;
                saveData.carpetCompleted = saveDataPrev.carpetCompleted;
                saveData.towelCompleted = saveDataPrev.towelCompleted;

                saveData.IsBathSinkToiletCompleted = saveDataPrev.IsBathSinkToiletCompleted;
                saveData.IsWasherCarpetTowelCompleted = saveDataPrev.IsWasherCarpetTowelCompleted;

                //lvl03
                saveData.kitchenCompleted = saveDataPrev.kitchenCompleted;
                saveData.cookerCompleted = saveDataPrev.cookerCompleted;
                saveData.fridgeCompleted = saveDataPrev.fridgeCompleted;

                saveData.chair01Completed = saveDataPrev.chair01Completed;
                saveData.chair02Completed = saveDataPrev.chair02Completed;
                saveData.chair03Completed = saveDataPrev.chair03Completed;

                saveData.chair04Completed = saveDataPrev.chair04Completed;
                saveData.tableCompleted = saveDataPrev.tableCompleted;
                saveData.tableItemsCompleted = saveDataPrev.tableItemsCompleted;

                saveData.vaseCompleted = saveDataPrev.vaseCompleted;
                saveData.microwaveCompleted = saveDataPrev.microwaveCompleted;
                saveData.panCompleted = saveDataPrev.panCompleted;

                saveData.dishesCompleted = saveDataPrev.dishesCompleted;
                saveData.glass01Completed = saveDataPrev.glass01Completed;
                saveData.glass02Completed = saveDataPrev.glass02Completed;

                saveData.glass03Completed = saveDataPrev.glass03Completed;
                saveData.glass04Completed = saveDataPrev.glass04Completed;
                saveData.glass05Completed = saveDataPrev.glass05Completed;

                saveData.IsKitchenCookerFridgeCompleted = saveDataPrev.IsKitchenCookerFridgeCompleted;
                saveData.IsChair01Chair02Chair03Completed = saveDataPrev.IsChair01Chair02Chair03Completed;
                saveData.IsChair04TableTableItemsCompleted = saveDataPrev.IsChair04TableTableItemsCompleted;
                saveData.IsVaseMicrowavePanCompleted = saveDataPrev.IsVaseMicrowavePanCompleted;
                saveData.IsDishesGlass01Glass02Completed = saveDataPrev.IsDishesGlass01Glass02Completed;
                saveData.IsGlass03Glass04Glass05Completed = saveDataPrev.IsGlass03Glass04Glass05Completed;
            }


        }
        else if (levelName == "Level02")
        {
            GameManager_Lvl02 gameManager = Object.FindObjectOfType<GameManager_Lvl02>();
            if (gameManager == null)
            {
                Debug.LogError("SaveManager: GameManager not found!");
                return;
            }

            saveData.bathCompleted = gameManager.GetBathCompleted();
            saveData.sinkCompleted = gameManager.GetSinkCompleted();
            saveData.toiletCompleted = gameManager.GetToiletCompleted();      

            saveData.washerCompleted = gameManager.GetWasherCompleted();
            saveData.carpetCompleted = gameManager.GetCarpetCompleted();
            saveData.towelCompleted = gameManager.GetTowelCompleted();

            saveData.IsBathSinkToiletCompleted = gameManager.GetIsBathSinkToiletCompleted();
            saveData.IsWasherCarpetTowelCompleted = gameManager.GetIsWasherCarpetTowelCompleted();


            SaveData saveDataPrev = LoadGame();
            //if prev save data existed
            if (saveDataPrev != null)
            {
                //lvl01
                saveData.bedCompleted = saveDataPrev.bedCompleted;
                saveData.bookshelfCompleted = saveDataPrev.bookshelfCompleted;

                saveData.deskCompleted = saveDataPrev.deskCompleted;
                saveData.chairCompleted = saveDataPrev.chairCompleted;

                saveData.rugCompleted = saveDataPrev.rugCompleted;
                saveData.posterCompleted = saveDataPrev.posterCompleted;

                saveData.sofaCompleted = saveDataPrev.sofaCompleted;
                saveData.tvCompleted = saveDataPrev.tvCompleted;

                saveData.IsBedBookCompleted = saveDataPrev.IsBedBookCompleted;
                saveData.IsDeskChairCompleted = saveDataPrev.IsDeskChairCompleted;
                saveData.IsRugPosterCompleted = saveDataPrev.IsRugPosterCompleted;
                saveData.IsSofaTVCompleted = saveDataPrev.IsSofaTVCompleted;


                //lvl03
                saveData.kitchenCompleted = saveDataPrev.kitchenCompleted;
                saveData.cookerCompleted = saveDataPrev.cookerCompleted;
                saveData.fridgeCompleted = saveDataPrev.fridgeCompleted;

                saveData.chair01Completed = saveDataPrev.chair01Completed;
                saveData.chair02Completed = saveDataPrev.chair02Completed;
                saveData.chair03Completed = saveDataPrev.chair03Completed;

                saveData.chair04Completed = saveDataPrev.chair04Completed;
                saveData.tableCompleted = saveDataPrev.tableCompleted;
                saveData.tableItemsCompleted = saveDataPrev.tableItemsCompleted;

                saveData.vaseCompleted = saveDataPrev.vaseCompleted;
                saveData.microwaveCompleted = saveDataPrev.microwaveCompleted;
                saveData.panCompleted = saveDataPrev.panCompleted;

                saveData.dishesCompleted = saveDataPrev.dishesCompleted;
                saveData.glass01Completed = saveDataPrev.glass01Completed;
                saveData.glass02Completed = saveDataPrev.glass02Completed;

                saveData.glass03Completed = saveDataPrev.glass03Completed;
                saveData.glass04Completed = saveDataPrev.glass04Completed;
                saveData.glass05Completed = saveDataPrev.glass05Completed;

                saveData.IsKitchenCookerFridgeCompleted = saveDataPrev.IsKitchenCookerFridgeCompleted;
                saveData.IsChair01Chair02Chair03Completed = saveDataPrev.IsChair01Chair02Chair03Completed;
                saveData.IsChair04TableTableItemsCompleted = saveDataPrev.IsChair04TableTableItemsCompleted;
                saveData.IsVaseMicrowavePanCompleted = saveDataPrev.IsVaseMicrowavePanCompleted;
                saveData.IsDishesGlass01Glass02Completed = saveDataPrev.IsDishesGlass01Glass02Completed;
                saveData.IsGlass03Glass04Glass05Completed = saveDataPrev.IsGlass03Glass04Glass05Completed;
            }

        }


        else if (levelName == "Level03")
        {
            GameManager_Lvl03 gameManager = Object.FindObjectOfType<GameManager_Lvl03>();
            if (gameManager == null)
            {
                Debug.LogError("SaveManager: GameManager not found!");
                return;
            }

            saveData.kitchenCompleted = gameManager.GetKitchenCompleted();
            saveData.cookerCompleted = gameManager.GetCookerCompleted();
            saveData.fridgeCompleted = gameManager.GetFridgeCompleted();

            saveData.chair01Completed = gameManager.GetChair01Completed();
            saveData.chair02Completed = gameManager.GetChair02Completed();
            saveData.chair03Completed = gameManager.GetChair03Completed();

            saveData.chair04Completed = gameManager.GetChair04Completed();
            saveData.tableCompleted = gameManager.GetTableCompleted();
            saveData.tableItemsCompleted = gameManager.GetTableItemsCompleted();

            saveData.vaseCompleted = gameManager.GetVaseCompleted();
            saveData.microwaveCompleted = gameManager.GetMicrowaveCompleted();
            saveData.panCompleted = gameManager.GetPanCompleted();

            saveData.dishesCompleted = gameManager.GetDishesCompleted();
            saveData.glass01Completed = gameManager.GetGlass01Completed();
            saveData.glass02Completed = gameManager.GetGlass02Completed();

            saveData.glass03Completed = gameManager.GetGlass03Completed();
            saveData.glass04Completed = gameManager.GetGlass04Completed();
            saveData.glass05Completed = gameManager.GetGlass05Completed();

            saveData.IsKitchenCookerFridgeCompleted = gameManager.GetIsKitchenCookerFridgeCompleted();
            saveData.IsChair01Chair02Chair03Completed = gameManager.GetIsChair01Chair02Chair03Completed();
            saveData.IsChair04TableTableItemsCompleted = gameManager.GetIsChair04TableTableItemsCompleted();
            saveData.IsVaseMicrowavePanCompleted = gameManager.GetIsVaseMicrowavePanCompleted();
            saveData.IsDishesGlass01Glass02Completed = gameManager.GetIsDishesGlass01Glass02Completed();
            saveData.IsGlass03Glass04Glass05Completed = gameManager.GetIsGlass03Glass04Glass05Completed();




            SaveData saveDataPrev = LoadGame();
            //if prev save data existed
            if (saveDataPrev != null)
            {
                //lvl01
                saveData.bedCompleted = saveDataPrev.bedCompleted;
                saveData.bookshelfCompleted = saveDataPrev.bookshelfCompleted;

                saveData.deskCompleted = saveDataPrev.deskCompleted;
                saveData.chairCompleted = saveDataPrev.chairCompleted;

                saveData.rugCompleted = saveDataPrev.rugCompleted;
                saveData.posterCompleted = saveDataPrev.posterCompleted;

                saveData.sofaCompleted = saveDataPrev.sofaCompleted;
                saveData.tvCompleted = saveDataPrev.tvCompleted;

                saveData.IsBedBookCompleted = saveDataPrev.IsBedBookCompleted;
                saveData.IsDeskChairCompleted = saveDataPrev.IsDeskChairCompleted;
                saveData.IsRugPosterCompleted = saveDataPrev.IsRugPosterCompleted;
                saveData.IsSofaTVCompleted = saveDataPrev.IsSofaTVCompleted;


                //lvl02
                saveData.bathCompleted = saveDataPrev.bathCompleted;
                saveData.sinkCompleted = saveDataPrev.sinkCompleted;
                saveData.toiletCompleted = saveDataPrev.toiletCompleted;

                saveData.washerCompleted = saveDataPrev.washerCompleted;
                saveData.carpetCompleted = saveDataPrev.carpetCompleted;
                saveData.towelCompleted = saveDataPrev.towelCompleted;

                saveData.IsBathSinkToiletCompleted = saveDataPrev.IsBathSinkToiletCompleted;
                saveData.IsWasherCarpetTowelCompleted = saveDataPrev.IsWasherCarpetTowelCompleted;
            }

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
