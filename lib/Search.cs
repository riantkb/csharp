using System;
using System.Collections.Generic;

static class Search {
    // x より大きいの項の index のうち最小のものを返す
    // x 以下の項の index のうち最大のもの + 1 を返す
    // そこに挿入
    public static int UpperBound<T>(this IList<T> a, T x) where T : IComparable<T> {
        int le = -1, u = a.Count;
        while (le < u - 1) {
            int m = (le + u) >> 1;
            if (a[m].CompareTo(x) > 0) u = m;
            else le = m;
        }
        return le + 1;
    }
    public static int UpperBound<T>(this IList<T> a, T x, Func<T, T, int> compare) {
        int le = -1, u = a.Count;
        while (le < u - 1) {
            int m = (le + u) >> 1;
            if (compare(a[m], x) > 0) u = m;
            else le = m;
        }
        return le + 1;
    }

    // n 未満の項の index のうち最大のもの + 1 を返す
    // n 以上の項の index のうち最小のものを返す
    // そこに挿入
    public static int LowerBound<T>(this IList<T> a, T x) where T : IComparable<T> {
        int l = -1, ue = a.Count;
        while (l < ue - 1) {
            int m = (l + ue) >> 1;
            if (a[m].CompareTo(x) < 0) l = m;
            else ue = m;
        }
        return ue;
    }
    public static int LowerBound<T>(this IList<T> a, T x, Func<T, T, int> compare) {
        int l = -1, ue = a.Count;
        while (l < ue - 1) {
            int m = (l + ue) >> 1;
            if (compare(a[m], x) < 0) l = m;
            else ue = m;
        }
        return ue;
    }
}
