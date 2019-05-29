using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace TotalCommanderProjectV2.DataModels
{
    public class DiscDirectory : DiscElement
    {
        // pola
        string path;
        string name;
        DateTime date;

        // właściwości
        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Date { get => date; set => date = value; }
        public int NumberOfSubDirectories { get; }

        // konstruktor
        public DiscDirectory(string path)
        {
            this.Path = path;
            Date = Directory.GetCreationTime(path);
            Name = path.Substring(path.LastIndexOf(@"\") + 1);
        }

        // metody
        public override DateTime GetCreationDate()
        {
            return Date;
        }
        public override string GetDescription()
        {
            return ("path: " + Path + ", Type(Directory)");
        }
        public override string GetName()
        {
            return Name;
        }
        public int GetSubDirectories()
        {
            return NumberOfSubDirectories;
        }
        public override string GetPath()
        {
            return path;
        }
        public List<DiscElement> GetAllFiles()
        {
            string[] directoriesPaths = Directory.EnumerateDirectories(Path).ToArray();
            string[] filesPaths = Directory.EnumerateFiles(Path).ToArray();
            List<DiscElement> elements = new List<DiscElement>();

            foreach (string directoryPath in directoriesPaths)
            {
                elements.Add(new DiscDirectory(directoryPath));
            }

            foreach (string filePath in filesPaths)
            {
                elements.Add(new DiscFile(filePath));
            }

            return elements;
        }

    }
}
