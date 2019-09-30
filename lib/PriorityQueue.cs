using System;
using System.Collections.Generic;

// the greatest element pops
class PriorityQueue<T> {
    List<T> buf;
    public bool rev = false;
    Func<int, int, int> cmp;
    public PriorityQueue() {
        buf = new List<T>();
        cmp = (i, j) => Comparer<T>.Default.Compare(buf[i], buf[j]) * (rev ? -1 : 1);
    }
    public PriorityQueue(Func<T, T, int> cmp) {
        buf = new List<T>();
        this.cmp = (i, j) => cmp(buf[i], buf[j]);
    }
    void swap(int i, int j) { var t = buf[i]; buf[i] = buf[j]; buf[j] = t; }
    public void Push(T elem) {
        int n = buf.Count;
        buf.Add(elem);
        while (n > 0) {
            int i = (n - 1) >> 1;
            if (cmp(n, i) > 0) swap(i, n);
            n = i;
        }
    }
    public T Pop() {
        T ret = buf[0];
        int n = buf.Count - 1;
        buf[0] = buf[n];
        buf.RemoveAt(n);
        for (int i = 0, j; (j = (i << 1) + 1) < n; i = j) {
            if (j != n - 1 && cmp(j, j + 1) < 0) ++j;
            if (cmp(i, j) < 0) swap(i, j);
        }
        return ret;
    }
    public T Top => buf[0];
    public int Count => buf.Count;
}
