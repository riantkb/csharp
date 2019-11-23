using System;
using System.Collections.Generic;

using Number = System.Int64;

static class ShortestPath {
    public static void WarshallFloyd(Number[][] g) {
        int n = g.Length;
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                for (int k = 0; k < n; ++k)
                    g[j][k] = Math.Min(g[j][k], g[j][i] + g[i][k]);
    }

    class Heap {
        int n;
        Number[] values;
        int[] keys, indices;

        void Update(int i, Number val) {
            if (indices[i] == -1) {
                keys[n] = i;
                indices[i] = n;
                ++n;
            }
            values[i] = val;
            int p = indices[i];
            while (p > 0) {
                int par = p - 1 >> 1;
                if (values[keys[par]] > val) {
                    keys[p] = keys[par];
                    indices[keys[p]] = p;
                    p = par;
                }
                else break;
            }
            keys[p] = i;
            indices[i] = p;
        }
        int Pop() {
            --n;
            int ret = keys[0];
            indices[ret] = -1;
            int i = keys[n];
            keys[n] = -1;
            Number val = values[i];
            if (n == 0) return ret;
            int p = 0;
            while ((p << 1 | 1) < n) {
                int ch = p << 1 | 1;
                if (ch < n - 1 && values[keys[ch]] > values[keys[ch + 1]]) ++ch;
                if (val > values[keys[ch]]) {
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

        void init(int m, int s, Number inf) {
            values = new Number[m];
            keys = new int[m];
            indices = new int[m];
            for (int i = 0; i < m; i++) {
                values[i] = inf;
                keys[i] = -1;
                indices[i] = -1;
            }
            values[s] = 0;
            keys[0] = s;
            indices[s] = 0;
        }

        public Number[] Run(List<pair<Number, int>>[] edges, int s, Number inf) {
            init(edges.Length, s, inf);

            n = 1;
            while (n > 0) {
                int p = Pop();
                foreach (var e in edges[p])
                    if (values[e.v2] > values[p] + e.v1)
                        Update(e.v2, values[p] + e.v1);
            }
            return values;
        }
        public pair<Number[], long[]> Run(List<pair<Number, int>>[] edges, int s, long Mod, Number inf) {
            init(edges.Length, s, inf);
            var cnts = new long[edges.Length];
            cnts[s] = 1;

            n = 1;
            while (n > 0) {
                int p = Pop();
                foreach (var e in edges[p])
                    if (values[e.v2] > values[p] + e.v1) {
                        cnts[e.v2] = cnts[p];
                        Update(e.v2, values[p] + e.v1);
                    }
                    else if (values[e.v2] == values[p] + e.v1)
                        cnts[e.v2] = (cnts[e.v2] + cnts[p]) % Mod;
            }
            return new pair<Number[], long[]>(values, cnts);
        }
    }

    public static Number[] Dijkstra(List<pair<Number, int>>[] edges, int s, Number inf)
        => new Heap().Run(edges, s, inf);

    public static pair<Number[], long[]> Dijkstra(List<pair<Number, int>>[] edges, int s, long Mod, Number inf)
        => new Heap().Run(edges, s, Mod, inf);
}
