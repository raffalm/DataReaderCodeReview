using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IPrinter
    {
       void Print(IEnumerable<ImportedObject> objects);
    }
}