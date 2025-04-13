public class Util {
    public static int GetExpForLevel(int level) {
        if (level < 1 || level > Constant.ExpForEachLevel.Length) {
            throw new System.ArgumentOutOfRangeException("level", "Level must be between 1 and " + Constant.ExpForEachLevel.Length);
        }
        return Constant.ExpForEachLevel[level - 1];
    }
}