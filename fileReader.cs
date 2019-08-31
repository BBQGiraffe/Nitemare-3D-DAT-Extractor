using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace fileReader
{

    public class FileReader
    {


        private static Entry[] GetEntries(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (var reader = new BinaryReader(stream, Encoding.Default, true))
            {
                var entries = new List<Entry>();

                while (true)
                {
                    var entry = new Entry(reader);

                    if (entry.Position + entry.Length == stream.Length)
                        break;

                    entries.Add(entry);
                }

                return entries.ToArray();
            }
        }
        /*
        private static string GetPath(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            const string directory = @"";

            var path = Path.Combine(directory, fileName);

            return path;
        }
        */




        public static void Extract(string fileName, string dir, string fuck)
        {
            //var path = GetPath(fileName);
            bool isSound;
            int fileCount = 0;
            using (var stream = File.OpenRead(fileName))
            using (var reader = new BinaryReader(stream))
            {
                var entries = GetEntries(stream);
                string outputFolder = dir;
                
                //this part is kinda stupid but my tiny 15 year old brain could not think of anything better
                if(fuck == "SND.DAT")
                {
                    isSound = true;
                }
                else
                {
                    isSound = false;
                }
                var directory = Directory.CreateDirectory(@"C:\Nitemare\UIF");
                var index = 0;

                foreach (var entry in entries)
                {
                    stream.Position = entry.Position;
                    if (isSound == false)
                    {
                        var entryPath = Path.Combine(directory.FullName, $"{index++:D3}.PCX");
                        var entryData = reader.ReadBytes(entry.Length);
                        File.WriteAllBytes(entryPath, entryData);
                    }
                    else
                    {
                        directory = Directory.CreateDirectory(@"C:\Nitemare\SND");
                        var entryPath = "";
                        //this whole bs detects what extension the thing is
                        if (fileCount == 0)//I have NO fucking idea why but the soundbank still outputs as .VOC
                        {
                            entryPath = Path.Combine(directory.FullName, $"{index++:D3}.ibk");
                        }
                        if (fileCount > 0 && fileCount <= 15)
                        {
                            entryPath = Path.Combine(directory.FullName, $"{index++:D3}.mid");
                        }
                        else
                        {
                            entryPath = Path.Combine(directory.FullName, $"{index++:D3}.voc");
                        }
                        fileCount += 1; 
                        //var entryPath = Path.Combine(directory.FullName, $"{index++:D3}.PCX");
                        var entryData = reader.ReadBytes(entry.Length);
                        File.WriteAllBytes(entryPath, entryData);
                    }
                    

                    
                }
            }
        }
    }

    public struct Entry
    {
        public readonly ushort Length;
        public readonly uint Position;

        public Entry(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            Length = reader.ReadUInt16();
            Position = reader.ReadUInt32();
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Length)}: {Length}";
        }
    }
}