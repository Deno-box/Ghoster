namespace GhosterUtility
{
    // 計算系のUtility
    public static class CalculationUtility
    {
        /// <summary>
        /// 指定範囲内に値が収まっているか
        /// </summary>
        /// <param name="_value">判定を取りたい値</param>
        /// <param name="_min">範囲の最小値</param>
        /// <param name="_max">範囲の最大値</param>
        /// <returns>true : 範囲内 / false : 範囲外</returns>
        public static bool IsWithinRange(float _value,float _min, float _max)
        {
            if (_min <= _value &&
                _value <= _max)
                return true;

            return false;
        }
    }
}

