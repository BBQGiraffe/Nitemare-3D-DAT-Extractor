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

        //finds every file in the archive
        private static Entry[] GetEntries(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var reader = new BinaryReader(stream, Encoding.Default, true))
            {
                var entries = new List<Entry>();

                while (true)
                {
                    var entry = new Entry(reader);

                    if (entry.Position + entry.Length == stream.Length) break;

                    entries.Add(entry);
                }

                return entries.ToArray();
            }
        }

        public static void Extract(string fileName, string dir, string fuck)
        {
            //var path = GetPath(fileName);
            bool isSound = false;
            int fileCount = 0;
            using (var stream = File.OpenRead(fileName))
            using (var reader = new BinaryReader(stream))
            {
                var entries = GetEntries(stream);
                string outputFolder = dir;

                if (fuck == "SND.DAT" || fuck == "snd.DAT" || fuck == "SND.dat" || fuck == "snd.dat")
                {
                    isSound = true;
                }
                else
                {
                    isSound = false;
                }

                var directory = Directory.CreateDirectory(outputFolder + "\\UIF");
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
                        directory = Directory.CreateDirectory(outputFolder + "\\SND");
                        var entryPath = "";
                        //this whole bs detects what extension the thing is
                        if (fileCount == 0) 
                        {
                            entryPath = Path.Combine(directory.FullName, $"{index++:D3}.ibk");
                        }
                        if (fileCount > 0 && fileCount <= 15)
                        {
                            entryPath = Path.Combine(directory.FullName, $"{index++:D3}.mid");
                        }
                        if(fileCount > 15)
                        {
                            entryPath = Path.Combine(directory.FullName, $"{index++:D3}.voc");
                        }
                        fileCount += 1;
                        var entryData = reader.ReadBytes(entry.Length);

                        //for some reason before I added this it would output a bunch of completely empty files.
                        if(entry.Length > 0)
                        {
                            File.WriteAllBytes(entryPath, entryData);
                        }
                        
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
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            Length = reader.ReadUInt16();
            Position = reader.ReadUInt32();
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Length)}: {Length}";
        }
    }
}