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
    using System.Globalization;

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
    }
}