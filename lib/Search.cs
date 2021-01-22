using System;
using System.Collections.Generic;

static class Search {
    public static int MyBinarySearch(Predicate<int> is_r, int l, int r) {
        while (l < r - 1) {
            var m = l + ((r - l) >> 1);
            if (is_r(m)) r = m;
            else l = m;
        }
        return r;
    }

    public static long MyBinarySearch(Predicate<long> is_r, long l, long r) {
        while (l < r - 1) {
            var m = l + ((r - l) >> 1);
            if (is_r(m)) r = m;
            else l = m;
        }
        return r;
    }

    // x より大きいの項の index のうち最小のものを返す
    // x 以下の項の index のうち最大のもの + 1 を返す
    // そこに挿入
    public static int UpperBound<T>(this IList<T> a, T x) where T : IComparable<T>
        => MyBinarySearch(i => a[i].CompareTo(x) > 0, -1, a.Count);

    // n 未満の項の index のうち最大のもの + 1 を返す
    // n 以上の項の index のうち最小のものを返す
    // そこに挿入
    public static int LowerBound<T>(this IList<T> a, T x) where T : IComparable<T>
        => MyBinarySearch(i => a[i].CompareTo(x) >= 0, -1, a.Count);
}
