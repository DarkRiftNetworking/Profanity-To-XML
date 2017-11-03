using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProfanityToXml
{
    //Tool to convert a profanity list into Xml suitable for use with DarkRift's profanity filter

    //Profanity list available here:
    //https://github.com/LDNOOBW/List-of-Dirty-Naughty-Obscene-and-Otherwise-Bad-Words

    //Usage: ProfanityToXml <sourcefolder> <destinationfile>

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: ProfanityToXml <sourcefolder> <destinationfile>");
                return;
            }

            XElement root = new XElement("profanity");

            //Loop through profanity files
            foreach (string file in Directory.GetFiles(args[0]))
            {
                //Ignore Readme/License etc
                if (Path.HasExtension(file) || Path.GetFileNameWithoutExtension(file) == "LICENSE")
                    continue;

                string languageCode = Path.GetFileNameWithoutExtension(file);

                foreach (string word in File.ReadAllLines(file))
                {
                    XElement element = new XElement("word", word);
                    element.SetAttributeValue("lang", languageCode);

                    root.Add(element);
                }
            }

            root.Save(args[1]);
        }
    }
}
