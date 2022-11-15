namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader();
            var importedObjects = reader.ImportData("data.csv",true);
            var printer = new ConsolePrinter();
            printer.Print(importedObjects);
        }
    }
}
