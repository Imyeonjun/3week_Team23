using System;
using Newtonsoft.Json;
using TextRPG_Team23;

public class SaveData
{
    public string PlayerName { get; set; }
    public string JobName { get; set; }
    public int PlayerLevel { get; set; }
    public int PlayerExp { get; set; }
    public int PlayerHp { get; set; }
    public int PlayerMp { get; set; }
    public int PlayerGld { get; set; }
    public int PlayerBuffAtk { get; set; }
    public int PlayerBuffDef { get; set; }
    public List<ItemStack> InventoryItems { get; set; }

    public bool d1 { get; set; }
    public bool d2 { get; set; }
    public bool d3 { get; set; }
    public int TempleGold { get; set; }
}

public static class SaveSystem
{
    private static string savePath = "Mysave.json";

    public static void SaveGame(Player status, Inventory inventory, Temple temple)
    {
        var saveData = new SaveData
        {
            PlayerName = status.Name,
            PlayerLevel = status.Level,
            PlayerExp = status.Exp,
            PlayerHp = status.CurrentHp,
            PlayerMp = status.CurrentMp,
            PlayerGld = status.Gold,
            PlayerBuffAtk = status.BuffAtk,
            PlayerBuffDef = status.BuffDef,
            JobName = status.job.JobName,
            InventoryItems = inventory.GetAllItems(),
            d1 = DungeonMaganer.isClearStep1,
            d2 = DungeonMaganer.isClearStep2,
            d3 = DungeonMaganer.isClearStep3,
            TempleGold = temple.Gold
            
        };

        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        File.WriteAllText(savePath, json);

        Console.WriteLine("게임 저장 완료!");
    }

    public static bool LoadGame(out Player status, out Inventory inventory, out int tmpGld)
    {
        if (!File.Exists(savePath))
        {
            status = null;
            inventory = null;
            tmpGld = -1;
            Console.WriteLine("저장된 데이터가 없습니다.");
            return false;
        }

        string json = File.ReadAllText(savePath);
        var saveData = JsonConvert.DeserializeObject<SaveData>(json, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        Job j;
        switch (saveData.JobName)
        {
            case "전사":
                j = new Warrior();
                break;
            case "마법사":
                j = new Magician();
                break;
            default:
                j = new Warrior();
                break;
        }
        status = new Player(saveData.PlayerName, j, saveData.PlayerLevel, saveData.PlayerExp, saveData.PlayerHp, saveData.PlayerMp, saveData.PlayerGld);
        status.BuffAtk = saveData.PlayerBuffAtk;
        status.BuffDef = saveData.PlayerBuffDef;
        inventory = new Inventory(saveData.InventoryItems);
        DungeonMaganer.isClearStep1 = saveData.d1;
        DungeonMaganer.isClearStep2 = saveData.d2;
        DungeonMaganer.isClearStep3 = saveData.d3;
        tmpGld = saveData.TempleGold;
        Console.WriteLine("게임 불러오기 완료!");
        return true;
    }

}
