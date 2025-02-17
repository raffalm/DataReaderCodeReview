﻿namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader
    {
        private readonly IList<ImportedObject> ImportedObjects = new List<ImportedObject>();
        private readonly int dataColumnCount = 7;

        public IEnumerable<ImportedObject> ImportData(string fileToImport,bool skipHeader)
        {
            var pathToImport = Path.GetFullPath(fileToImport);
            if (!File.Exists(pathToImport))
            {
                Console.WriteLine($"File not found in path: {pathToImport}");
                Console.ReadLine();
                return ImportedObjects;
            }
            var importedLines = new List<string>();
            using (var streamReader = new StreamReader(fileToImport))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if(String.IsNullOrEmpty(line))
                        continue;
                    importedLines.Add(line);
                }
            }
            var firstDataLineIdx=0;
            if (skipHeader)
                firstDataLineIdx++;
            for (int i = firstDataLineIdx; i < importedLines.Count; i++)
            {
                var importedLine = importedLines[i];
                var values = importedLine.Split(';');
                if (values.Length != dataColumnCount)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is incorrect data format:");
                    Console.WriteLine(String.Join(";", values));
                    Console.ResetColor();
                    continue;
                }
                var importedObject = new ImportedObject();
                importedObject.Type = values[0];
                importedObject.Name = values[1];
                importedObject.Schema = values[2];
                importedObject.ParentName = values[3];
                importedObject.ParentType = values[4];
                importedObject.DataType = values[5];
                importedObject.IsNullable = values[6];
                ImportedObjects.Add(importedObject);
            }

            // clear and correct imported data
            foreach (var importedObject in ImportedObjects)
            {
                importedObject.Type = importedObject.Type.Trim().Replace(" ", "").ToUpper();
                importedObject.Name = importedObject.Name.Trim().Replace(" ", "");
                importedObject.Schema = importedObject.Schema.Trim().Replace(" ", "");
                importedObject.ParentName = importedObject.ParentName.Trim().Replace(" ", "");
                importedObject.ParentType = importedObject.ParentType.Trim().Replace(" ", "").ToUpper();
            }

            // assign number of children
            foreach(var parent in ImportedObjects)
            {
                parent.NumberOfChildren = ImportedObjects
                    .Count(x => x.ParentType == parent.Type && x.ParentName == parent.Name);
            }
            return ImportedObjects;
        }
    }
}