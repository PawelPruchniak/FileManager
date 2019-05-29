using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TotalCommanderProjectV2.DataModels
{
    public class DiscFile : DiscElement
    {
        // pola
        string path;
        string name;
        DateTime date;

        // właściwości
        public string Path { get => path; set => path = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Extension { get; internal set; }

        // konstruktor
        public DiscFile()
        {

        }
        public DiscFile(string path)
        {
            this.Path = path;
            Name = path.Substring(path.LastIndexOf(@"\") + 1);
            Date = File.GetCreationTime(path);
        }


        /// <summary>
        /// pobieramy zawartość pliku (dotyczy gównie plików tekstowych)
        /// </summary>
        /// <returns></returns> 
        internal string GetFileContet()
        {

            throw new NotImplementedException();
        }

        // metody
        public override DateTime GetCreationDate()
        {
            return Date;
        }
        public override string GetDescription()
        {
            return ("path: " + Path + ", Type(File)");
        }
        public override string GetName()
        {
            return Name;
        }
        public override string GetPath()
        {
            return path;
        }

    }
}
