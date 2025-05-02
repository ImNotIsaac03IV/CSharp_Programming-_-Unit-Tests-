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
    using System;
    using System.Globalization;
    using System.IO;

    public class UpdateProductCsv
    {
        public static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: UpdateProductCsv <filename.csv> <percentage>");
                return 1;
            }

            string fileName = args[0];

            if (!File.Exists(fileName) || !fileName.EndsWith(".csv"))
            {
                Console.WriteLine("Error: File must be a .csv and must exist.");
                return 1;
            }

            if (!decimal.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal percent))
            {
                Console.WriteLine("Error: Invalid percentage. Use a dot for decimal (e.g., 10.5).");
                return 1;
            }

            string[] lines = File.ReadAllLines(fileName);

            string[] productCodes = new string[lines.Length];
            string[] descriptions = new string[lines.Length];
            string[] taxClasses = new string[lines.Length];
            decimal[] retails = new decimal[lines.Length];

            int count = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];

                if (Product.TryParseLine(line, out string parsedCodes, out string parsedDescrptions, out string parsedTaxClasses, out decimal parsedRetails))
                {
                    productCodes[count] = parsedCodes;
                    descriptions[count] = parsedDescrptions;
                    taxClasses[count] = parsedTaxClasses;
                    retails[count] = parsedRetails;

                    count++;
                }
                else
                {
                    Console.WriteLine($"Invalid Format detected: {line}");
                }
            }


            for (int i = 0; i < retails.Length; i++)
            {
                retails[i] = Math.Round(retails[i] * (1 + percent / 100), 2);
            }

            string[] updatedLines = new string[count + 1];

            updatedLines[0] = lines[0];

            for (int i = 0; i < count; i++)
            {
                updatedLines[i + 1] = Product.CreateCsvLine(productCodes[i], descriptions[i], taxClasses[i], retails[i]);
            }

            string tempFile = Path.ChangeExtension(fileName, ".$$$");
            File.WriteAllLines(tempFile, updatedLines);

            string backupFile = Path.ChangeExtension(fileName, ".bak");

            if (File.Exists(backupFile))
            {
                File.Delete(backupFile);
            }

            File.Move(fileName, backupFile);
            File.Move(tempFile, fileName);

            Console.WriteLine("Prices updated and file saved.");

            return 0;
        }
    }
}