using System;
using System.Collections.Generic;

// !!! not verify yet !!!
static class Strings {
    static int[] z_algo<T>(IList<T> s, Func<T, T, bool> eq) {
        int n = s.Count;
        var res = new int[n];
        res[0] = n;
        int i = 1, j = 0;
        while (i < n) {
            while (i + j < n && eq(s[j], s[i + j])) ++j;
            res[i] = j;
            if (j == 0) {
                ++i;
                continue;
            }
            int k = 1;
            while (i + k < n && k + res[k] < j) {
                res[i + k] = res[k];
                ++k;
            }
            i += k;
            j -= k;
        }
        return res;
    }
    static int[] z_algo(string s) => z_algo(s.ToCharArray(), (x, y) => x == y);

    static int[] kmp<T>(IList<T> s, Func<T, T, bool> eq) {
        int n = s.Count;
        var res = new int[n + 1];
        res[0] = -1;
        int j = -1;
        for (int i = 0; i < n; ++i) {
            while (j >= 0 && !eq(s[i], s[j])) j = res[j];
            j++;
            res[i + 1] = j;
        }
        return res;
    }
    static int[] kmp(string s) => kmp(s.ToCharArray(), (x, y) => x == y);

    static int[] manacher<T>(IList<T> s, Func<T, T, bool> eq) {
        int n = s.Count;
        int i = 0, j = 0;
        var res = new int[n];
        while (i < n) {
            while (i - j >= 0 && i + j < n && eq(s[i - j], s[i + j])) ++j;
            res[i] = j;
            int k = 1;
            while (i - k >= 0 && i + k < n && k + res[i - k] < j) {
                res[i + k] = res[i - k];
                ++k;
            }
            i += k;
            j -= k;
        }
        return res;
    }
    static int[] manacher(string s, bool extend = false, char dummy = '$') {
        if (extend) {
            var l = new char[s.Length * 2 - 1];
            for (int i = 0; i < l.Length; i++)
                l[i] = i % 2 == 0 ? s[i / 2] : dummy;

            return manacher(l, (x, y) => x == y);
        }
        else
            return manacher(s.ToCharArray(), (x, y) => x == y);
    }

}
