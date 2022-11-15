using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(IEnumerable<ImportedObject> importedObjects)
        {
            var databases = importedObjects.Where(x => x.Type == "DATABASE");
            foreach (var database in databases)
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                var tables = importedObjects.Where(x => x.ParentType == database.Type && x.ParentName == database.Name);
                foreach (var table in tables)
                {
                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                    var columns = importedObjects.Where(x => x.ParentType == table.Type && x.ParentName == table.Name);
                    foreach (var column in columns)
                    {
                        Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                    }
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
