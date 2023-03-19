using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Country_Code_Switch_Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // path to the txt file you want to read FROM
            var readPath = "";
            // path to the txt file you want to write TO
            var writePath = "";

            // parser with delimiters so "split" the data according to the specified delimiters
            var parser = new TextFieldParser(readPath) { Delimiters = new string[] { "\t", "/" } };

            var strBldr = new StringBuilder();

            while (!parser.EndOfData) // while the parser still has data
            {
                var readFields = parser.ReadFields();

                var dialingCode = readFields[0];

                if (dialingCode.Contains("-")) // skips weird country codes
                    continue;

                var countryCode = readFields[1];

                strBldr.Append($"case \"{countryCode}\": return \"{dialingCode}\";");
            }

            File.WriteAllText(writePath, strBldr.ToString());
        }


    }
}
