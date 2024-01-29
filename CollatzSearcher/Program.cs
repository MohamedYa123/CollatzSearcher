using System.Diagnostics;

namespace CollatzSearcher
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            CollatzSearcher collatzSearcher = new CollatzSearcher();
            Task.Run(()=> { collatzSearcher.Search(1, collatzSearcher.fastpower(2,60)); });
            Stopwatch sp=new Stopwatch();
            sp.Start();
            while (true)
            {
                ulong spp = collatzSearcher.Reached();
                Console.Write($"\rReached : {spp} , Covered numbers:{collatzSearcher.numsfound.Count},Log:{Math.Log10(spp):###0.00000} , Longest:{collatzSearcher.longest}, Longest number: {collatzSearcher.longestnum}, Speed:{spp*1000.0/(sp.ElapsedMilliseconds)/1000000:######0.00000}*10^6 Number/second     ");
                Thread.Sleep(30);
            }
            sp.Stop();
        }
    }
}