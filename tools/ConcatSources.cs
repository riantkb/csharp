using System;
using System.Linq;
using System.IO;

namespace ConcatSources {
    class Program {
        static void Main(string[] args) {
            var codes = args.Select(File.ReadAllLines);
            var usings = codes.Select(lines => lines.Where(line => line.StartsWith("using "))).SelectMany(x => x).Distinct();
            var mains = codes.Select(lines => lines.Where(line => !line.StartsWith("using "))).SelectMany(x => x);
            foreach (var item in usings) Console.WriteLine(item);
            foreach (var item in mains) Console.WriteLine(item);
        }
    }
}
