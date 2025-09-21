public class Util {
    public static int GetExpForLevel(int level) {
        if (level < 1 || level > Constant.ExpForEachLevel.Length) {
            throw new System.ArgumentOutOfRangeException("level", "Level must be between 1 and " + Constant.ExpForEachLevel.Length);
        }
        return Constant.ExpForEachLevel[level - 1];
    }

    public static string NumberToRoman(int number) {
        if (number == 1) return "I";
        if (number == 2) return "II";
        if (number == 3) return "III";
        if (number == 4) return "IV";
        if (number == 5) return "V";
        if (number == 6) return "VI";
        if (number == 7) return "VII";
        if (number == 8) return "VIII";
        if (number == 9) return "IX";
        if (number == 10) return "X";
        return "";
    }
}