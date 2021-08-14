using System;
using System.Collections.Generic;
class Heap<T> {
    int n;
    T[] values;
    int[] keys, indices;
    Func<int, int, int> cmp;
    public bool rev = false;
    public Heap(int m) {
        values = new T[m];
        keys = new int[m];
        indices = new int[m];
        for (int i = 0; i < m; i++) {
            keys[i] = -1;
            indices[i] = -1;
        }
        n = 0;
        cmp = (i, j) => Comparer<T>.Default.Compare(values[i], values[j]) * (rev ? -1 : 1);
    }

    public void Update(int i, T val) {
        if (indices[i] == -1) {
            keys[n] = i;
            indices[i] = n;
            ++n;
        }
        values[i] = val;
        int p = indices[i];
        while (p > 0) {
            int par = p - 1 >> 1;
            if (cmp(keys[par], i) < 0) {
                keys[p] = keys[par];
                indices[keys[p]] = p;
                p = par;
            }
            else break;
        }
        while ((p << 1 | 1) < n) {
            int ch = p << 1 | 1;
            if (ch < n - 1 && cmp(keys[ch], keys[ch + 1]) < 0) ++ch;
            if (cmp(i, keys[ch]) < 0) {
                keys[p] = keys[ch];
                indices[keys[p]] = p;
                p = ch;
            }
            else break;
        }
        keys[p] = i;
        indices[i] = p;
    }
    public int Pop() {
        --n;
        int ret = keys[0];
        indices[ret] = -1;
        int i = keys[n];
        keys[n] = -1;
        T val = values[i];
        if (n == 0) return ret;
        int p = 0;
        while ((p << 1 | 1) < n) {
            int ch = p << 1 | 1;
            if (ch < n - 1 && cmp(keys[ch], keys[ch + 1]) < 0) ++ch;
            if (cmp(i, keys[ch]) < 0) {
                keys[p] = keys[ch];
                indices[keys[p]] = p;
                p = ch;
            }
            else break;
        }
        keys[p] = i;
        indices[i] = p;
        return ret;
    }
    public int Count => n;
}
