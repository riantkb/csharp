#load "../lib/BIT.cs"

var (n, q) = new Func<(int, int)>(() => {
   var _ = Console.ReadLine().Split().Select(int.Parse).ToArray();
   return (_[0], _[1]);
})();

var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

var bit = new BIT(n);
for (int i = 0; i < n; i++)
    bit.add(i, a[i]);

for (int i = 0; i < q; i++)
{
    var (t, l, r) = new Func<(int, int, int)>(() => {
        var _ = Console.ReadLine().Split().Select(int.Parse).ToArray();
        return (_[0], _[1], _[2]);
    })();
    if (t == 0)
        bit.add(l, r);
    else
        Console.WriteLine(bit.sum(l, r));
}
