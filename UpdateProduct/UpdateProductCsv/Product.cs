/*--------------------------------------------------------------
*				HTBLA-Leonding / Class: 1xHIF
*--------------------------------------------------------------
*              Musterlösung-HA
*--------------------------------------------------------------
* Description: UpdateProductCsv
*--------------------------------------------------------------
*/

namespace UpdateProductCsv
{
    using System.IO;
    using System.Globalization;
    using System;

    public class Product
    {
        public static string CreateCsvLine(string productCodes, string descriptions, string taxClasses, decimal retails)
        {
            return $"{productCodes};{descriptions};{taxClasses};{retails.ToString(CultureInfo.InvariantCulture)}";
        }
        public static bool TryParseLine(string line, out string code, out string descriptions, out string taxClasses, out decimal retails)
        {
            code = "";
            descriptions = "";
            taxClasses = "";
            retails = 0;

            string[] parts = line.Split(';');

            if (parts.Length == 4)
            {
                code = parts[0];
                descriptions = parts[1];
                taxClasses = parts[2];
                retails = decimal.Parse(parts[3]);

                return true;
            }

            return false;
        }
        public static int IsValidArgLength(string[] argLength)
        {
            if (argLength.Length != 2)
            {
                Console.WriteLine("Usage: UpdateProductCsv <filename.csv> <percentage>");
                return 1;
            }
            return 0;
        }
        public static int IsValidFileName(string fileName)
        {
            if (!File.Exists(fileName) || !fileName.EndsWith(".csv"))
            {
                Console.WriteLine("Error: File must be a .csv and must exist.");
                return 1;
            }
            return 0;
        }
        public static void CreateBackup(string fileName, string[] updatedList)
        {
            string tempFile = Path.ChangeExtension(fileName, ".$$$");
            File.WriteAllLines(tempFile, updatedList);

            string backup = Path.ChangeExtension(fileName, ".bak");

            if (File.Exists(backup))
            {
                File.Delete(backup);
            }

            File.Move(fileName, backup);
            File.Move(tempFile, fileName);
        }
    }
}