using UnityEngine;

public class Constant
{
    public static int MaxLevel = 70; // Maximum level a character can reach
    public static int StartMoney = 100000;
    public static int InventorySize = 50;
    public static int MaxAcceptedQuest = 10;
    public static int MaxExploreTeam = 3;
    public static Color32[] itemRarityColor = {
            new Color32(0,0,0,255),
            new Color32(72,209,55,255),
            new Color32(0,168,255,255),
            new Color32(142,68,173,255),
            new Color32(230,126,34,255)
        };
    
    public static string[] itemRarityName = {
        "Common",
        "Uncommon",
        "Rare",
        "Epic",
        "Legendary"
    };

    public static string[] equipmentRarityName = {
        "",
        "Good",
        "Great",
        "Epic",
        "Legendary"
    };

    public static int[] ExpForEachLevel = {
            100,
            150,
            200,
            250,
            300,
            350,
            400,
            500,
            600,
            1000,//Lv.10
            1500,
            2000,
            2500,
            3000,
            3500,
            4000,
            4500,
            5000,
            5500,
            10000,//Lv.20
            12500,
            15000,
            17500,
            20000,
            25000,
            30000,
            35000,
            40000,
            45000,
            60000,//Lv.30
            65000,
            70000,
            75000,
            80000,
            85000,
            90000,
            95000,
            100000,
            110000,
            130000,//Lv.40
            150000,
            175000,
            200000,
            225000,
            250000,
            275000,
            300000,
            325000,
            400000,
            500000,//Lv.50
            550000,
            600000,
            650000,
            700000,
            750000,
            800000,
            850000,
            900000,
            950000,
            1000000,//Lv.60
            1100000,
            1250000,
            1500000,
            1750000,
            2000000,
            3000000,
            4000000,
            6000000,
            8000000,
            10000000,//Lv. 70
        };

    public static int[] CraftSkillRequiredExp = {
        10,
        20,
        30,
        40,
        50,
        60,
        70,
        80,
        90,
        100,
    };

    public static int[] AlchemyExpForEachLevel = {
        100,
        200,
        500,
        800,
        1000,
    };
}