using System;
using System.Collections.Generic;

static class MyMath {
    public static long Mod = util.M;
    // public static long Mod = 1000000007;
    public static bool isprime(long a) {
        if (a < 2) return false;
        for (long i = 2; i * i <= a; i++) if (a % i == 0) return false;
        return true;
    }
    public static bool[] sieve(int n) {
        var p = new bool[n + 1];
        for (int i = 2; i <= n; i++) p[i] = true;
        for (int i = 2; i * i <= n; i++) if (p[i]) for (int j = i * i; j <= n; j += i) p[j] = false;
        return p;
    }
    public static int[] sieve2(int n) {
        var p = new int[n + 1];
        for (int i = 2; i <= n; i++) p[i] = i;
        for (int i = 2; i * i <= n; i++) {
            if (p[i] == i)
                for (int j = i * i; j <= n; j += i)
                    p[j] = Math.Min(p[j], i);
        }
        return p;
    }
    public static bool[] segmentSieve(long l, long r) {
        int sqn = (int)Math.Sqrt(r + 9);
        var ps = getprimes(sqn);
        return segmentSieve(l, r,ps);
    }
    public static bool[] segmentSieve(long l, long r, List<int> ps) {
        var sieve = new bool[r - l + 1];
        for (long i = l; i <= r; i++) sieve[i - l] = true;
        foreach (long p in ps) {
            if (p * p > r) break;
            for (long i = p >= l ? p * p : (l + p - 1) / p * p; i <= r; i += p) sieve[i - l] = false;
        }
        return sieve;
    }
    public static List<int> getprimes(int n) {
        var prs = new List<int>();
        var p = sieve(n);
        for (int i = 2; i <= n; i++) if (p[i]) prs.Add(i);
        return prs;
    }
    public static long pow(long a, long b, long mod) {
        a %= mod;
        if (b < 0) Console.Error.WriteLine($"power number is negative ({a}^{b}).");
        if (b <= 0) return 1;
        var t = pow(a, b / 2, mod);
        if ((b & 1) == 0) return t * t % mod;
        return t * t % mod * a % mod;
    }
    public static long pow(long a, long b) => pow(a, b, Mod);
    public static long inv(long a) => pow(a, Mod - 2);
    public static long gcd(long a, long b) {
        while (b > 0) {
            var t = a % b;
            a = b;
            b = t;
        }
        return a;
    }
    // a x + b y = gcd(a, b)
    public static long extgcd(long a, long b, out long x, out long y) {
        long g = a; x = 1; y = 0;
        if (b > 0) { g = extgcd(b, a % b, out y, out x); y -= a / b * x; }
        return g;
    }

    // return (r, m): x = r (mod. m)
    // return (0, -1) if no answer
    public static pair<long, long> chineserem(IList<long> b, IList<long> m) {
        long r = 0, M = 1;
        for (int i = 0; i < b.Count; ++i) {
            long p, q;
            long d = extgcd(M, m[i], out p, out q); // p is inv of M/d (mod. m[i]/d)
            if ((b[i] - r) % d != 0) return new pair<long, long>(0, -1);
            long tmp = (b[i] - r) / d * p % (m[i]/d);
            r += M * tmp;
            M *= m[i]/d;
        }
        return new pair<long, long>((r % M + M) % M, M);
    }

    // return k: x^k = y (mod. mod) O(sqrt(mod))
    public static long modlog(long x, long y, long mod) {
        if (y == 1) return 0;
        long H = (long)Math.Sqrt(mod) + 1;
        var baby = new Dictionary<long, long>();
        for (long b = 0, xby = y; b < H; b++, xby = (xby * x) % mod) {
            if (!baby.ContainsKey(xby))
                baby.Add(xby, b);
            else
                baby[xby] = b;
        }

        long xH = 1;
        for (int i = 0; i < H; ++i) xH = xH * x % mod;
        for (long a = 1, xaH = xH; a <= H; a++, xaH = (xaH * xH) % mod)
            if (baby.ContainsKey(xaH))
                return a * H - baby[xaH];

        return -1;
    }
    public static long lcm(long a, long b) => a / gcd(a, b) * b;

    static long[] facts, invs;
    public static void setfacts(int n) {
        facts = new long[n + 1];
        facts[0] = 1;
        for (int i = 1; i <= n; i++) facts[i] = facts[i - 1] * i % Mod;
        invs = new long[n + 1];
        invs[n] = inv(facts[n]);
        for (int i = n; i > 0 ; i--) invs[i - 1] = invs[i] * i % Mod;
    }
    public static long fact(long n) {
        if (n < 0) return 0;
        if (facts != null && facts.Length > n) return facts[n];
        long numer = 1;
        for (long i = 1; i <= n; i++) numer = numer * (i % Mod) % Mod;
        return numer;
    }
    public static long perm(long n, long r) {
        if (n < 0 || r < 0 || r > n) return 0;
        if (facts != null && facts.Length > n) return facts[n] * invs[n - r] % Mod;
        long numer = 1;
        for (long i = 0; i < r; i++) numer = numer * ((n - i) % Mod) % Mod;
        return numer;
    }
    public static long comb(long n, long r) {
        if (n < 0 || r < 0 || r > n) return 0;
        if (facts != null && facts.Length > n) return facts[n] * invs[r] % Mod * invs[n - r] % Mod;
        if (n - r < r) r = n - r;
        long numer = 1, denom = 1;
        for (long i = 0; i < r; i++) {
            numer = numer * ((n - i) % Mod) % Mod;
            denom = denom * ((i + 1) % Mod) % Mod;
        }
        return numer * inv(denom) % Mod;
    }
    public static long multi_choose(long n, long r) => comb(n + r - 1, r);
    public static long[][] getcombs(int n) {
        var ret = new long[n + 1][];
        for (int i = 0; i <= n; i++) {
            ret[i] = new long[i + 1];
            ret[i][0] = ret[i][i] = 1;
            for (int j = 1; j < i; j++) ret[i][j] = (ret[i - 1][j - 1] + ret[i - 1][j]) % Mod;
        }
        return ret;
    }
    // nC0, nC2, ..., nCn
    public static long[] getcomb(int n) {
        var ret = new long[n + 1];
        ret[0] = 1;
        for (int i = 0; i < n; i++) ret[i + 1] = ret[i] * (n - i) % Mod * inv(i + 1) % Mod;
        return ret;
    }

    public static class ModMatrix {
        public static long[][] E(int n) {
            var ret = new long[n][];
            for (int i = 0; i < n; i++) { ret[i] = new long[n]; ret[i][i] = 1; }
            return ret;
        }
        public static long[][] pow(long[][] A, long n) {
            if (n == 0) return E(A.Length);
            var t = pow(A, n / 2);
            if ((n & 1) == 0) return mul(t, t);
            return mul(mul(t, t), A);
        }
        public static long dot(long[] x, long[] y) {
            int n = x.Length;
            long ret = 0;
            for (int i = 0; i < n; i++) ret = (ret + x[i] * y[i]) % Mod;
            return ret;
        }
        public static long[][] trans(long[][] A) {
            int n = A[0].Length, m = A.Length;
            var ret = new long[n][];
            for (int i = 0; i < n; i++) { ret[i] = new long[m]; for (int j = 0; j < m; j++) ret[i][j] = A[j][i]; }
            return ret;
        }
        public static long[] mul(long a, long[] x) {
            int n = x.Length;
            var ret = new long[n];
            for (int i = 0; i < n; i++) ret[i] = a * x[i] % Mod;
            return ret;
        }
        public static long[] mul(long[][] A, long[] x) {
            int n = A.Length;
            var ret = new long[n];
            for (int i = 0; i < n; i++) ret[i] = dot(x, A[i]);
            return ret;
        }
        public static long[][] mul(long a, long[][] A) {
            int n = A.Length;
            var ret = new long[n][];
            for (int i = 0; i < n; i++) ret[i] = mul(a, A[i]);
            return ret;
        }
        public static long[][] mul(long[][] A, long[][] B) {
            int n = A.Length;
            var Bt = trans(B);
            var ret = new long[n][];
            for (int i = 0; i < n; i++) ret[i] = mul(Bt, A[i]);
            return ret;
        }
    }
}
