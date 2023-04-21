using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace Country_Code_Switch_Converter
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var files = ChooseFiles();


            // parser with delimiters to "split" the data according to the specified delimiters
            var parser = new TextFieldParser(files[0]) { Delimiters = new string[] { "\t", "/" } };

            var strBldr = new StringBuilder();

            while (!parser.EndOfData) // while the parser still has data
            {
                var readFields = parser.ReadFields();

                // might have to switch around the index of dialingCode and CountryCode depending on format of read file
                var dialingCode = readFields[0];

                if (dialingCode.Contains("-")) // skips weird country codes
                    continue;

                var countryCode = readFields[1];

                strBldr.Append($"case \"{countryCode}\": return \"{dialingCode}\";");
            }

            File.WriteAllText(files[1], strBldr.ToString());
        }

        private static string[] ChooseFiles()
        {
            var fileArray = new string[2];


            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Text files (*.txt)|*.txt"
            };
            Console.WriteLine("Please choose a file to read from. PRESS ANY KEY TO CONTINUE");
            Console.ReadKey();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileArray[0] = openFileDialog.FileName;
            }
            Console.WriteLine("Please choose a file to write to. PRESS ANY KEY TO CONTINUE");
            Console.ReadKey();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileArray[1] = openFileDialog.FileName;
            }
            Console.Write($"First choice {fileArray[0]}");
            Console.Write($"Second choice {fileArray[1]}");
            Console.ReadKey();

            return fileArray;
        }

    }
}
