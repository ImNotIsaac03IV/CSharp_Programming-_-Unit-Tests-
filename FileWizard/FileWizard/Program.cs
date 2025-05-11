using System;
using System.IO;
using System.Text;

namespace FileWizard
{
    /// <summary>
    /// FileWizard erledigt einige einfache Konvertieraufgaben
    /// auf beliebigen Textdateien
    /// </summary>
    class Program
    {
        static ConsoleColor defaultColor = Console.ForegroundColor;
        const string LOGO_FILE = "logo.txt";

        static void Main(string[] args)
        {
            string logo = File.ReadAllText(LOGO_FILE, Encoding.Default);
            Console.WriteLine(logo);

            string fileName = GetFileName();
            bool finished = false;
            do
            {
                finished = PerformOperation(ref fileName);
            } while (!finished);

            Console.WriteLine("Vielen Dank, dass ich Dir helfen durfte!");
            System.Threading.Thread.Sleep(3000);
        }


        /// <summary>
        /// Erfragen eines Dateinamen vom Benutzer.
        /// Gibt dieser einen ungültigen Dateinamen ein
        /// (Datei existiert nicht), wird das Einlesen
        /// solange wiederholt, bis ein gültiger Dateiname
        /// eingelesen wurde.
        /// Verwende File.Exists
        /// </summary>
        /// <returns>Gültiger Dateiname (Datei existiert)</returns>
        private static string GetFileName()
        {
            Console.Write("Welche Datei soll ich öffnen? ");
            string fileName = Console.ReadLine()!;

            while (!File.Exists(fileName) || !fileName.EndsWith(".txt"))
            {
                Console.WriteLine("Diese Datei existiert nicht!");

                Console.Write("Welche Datei soll ich öffnen? ");
                fileName = Console.ReadLine()!;
            }

            CreateBackup(fileName);

            return fileName;
        }

        /// <summary>
        /// Präsentiert eine Auswahl an Datei-Funktionen
        /// und führt die vom Benutzer gewählte Funktion
        /// aus.
        /// Der Dateiname muss als ref-Parameter übergeben
        /// werden, da er sich durch das Einlesen einer
        /// neuen Datei auch ändern kann.
        /// Wenn "Ende" ausgewählt wurde, wird true 
        /// zurückgegeben
        /// </summary>
        /// <param name="fileName">Dateiname als Referenz</param>
        /// <returns>finished</returns>
        private static bool PerformOperation(ref string fileName)
        {
            Console.WriteLine("Was kann ich für dich tun? ");
            Console.WriteLine($"{fileName}");

            Console.WriteLine("(1) Datei am Bildschirm anzeigen");
            Console.WriteLine("(2) Zeilennummern hinzufügen");
            Console.WriteLine("(3) Zeilen reversieren");
            Console.WriteLine("(4) Zeichenkette ersetzen");
            Console.WriteLine("(5) Neue Datei einlesen");
            Console.WriteLine("(0) Ende");

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    PrintFile(fileName);
                break;
                case "2":
                    AddLineNumbers(fileName);
                break;
                case "3":
                    ReverseLines(fileName);
                break;
                case "4":
                    ReplaceCharacters(fileName);
                break;
                case "5":
                    GetFileName();
                break;
                case "0":
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Erstellen einer Sicherungskopie einer Datei.
        /// Es wird an den Dateinamen '.bak' angehängt.
        /// Gibt es schon eine Sicherung mit diesem Namen,
        /// wird '.bak1' angehängt (bzw. '.bak2', '.bak3', usw.)
        /// Verwende File.Exists und File.Copy
        /// </summary>
        /// <param name="fileName">Zu sichernde Datei</param>
        private static void CreateBackup(string fileName)
        {
            string backupFile = Path.ChangeExtension(fileName, ".bak");

            int count = 1;

            while (File.Exists(backupFile))
            {
                backupFile = Path.ChangeExtension(fileName, $".bak{count}");

                count++;
            }

            Console.WriteLine($"Datei {backupFile} wurde erstellt");

            File.Copy(fileName, backupFile);
        }

        /// <summary>
        /// Alle Zeilen der Datei werden eingelesen und
        /// in umgekehrter Reihenfolge wieder auf die
        /// Datei geschrieben.
        /// Verwende File.ReadAllLines und File.WriteAllLines
        /// </summary>
        /// <param name="fileName"></param>
        private static void ReverseLines(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            string[] reversedLines = new string[lines.Length];

            int counter = 0;

            for (int i = lines.Length - 1; i >= 0; i--)
            {
                reversedLines[counter] = lines[i];

                counter++;
            }

            File.WriteAllLines(fileName, reversedLines);
        }

        /// <summary>
        /// Alle Zeilen der Datei werden eingelesen und
        /// mit einer Zeilennummer versehen wieder auf die
        /// gleiche Datei geschrieben.
        /// Verwende File.ReadAllLines und File.WriteAllLines
        /// </summary>
        /// <param name="fileName"></param>
        private static void AddLineNumbers(string fileName)
        {
            string[] tempFile = File.ReadAllLines(fileName);

            string[] modifiedFile = new string[tempFile.Length];

            for (int i = 0; i < tempFile.Length; i++)
            {
                modifiedFile[i] = $"{i + 1}: {tempFile[i]}";
            }

            File.WriteAllLines(fileName, modifiedFile);
        }

        /// <summary>
        /// Der Inhalt der Datei wird in eine string-Variable gelesen.
        /// Der Benutzer wird gefragt, welche Zeichenkette er durch
        /// welche andere Zeichenkette ersetzen will.
        /// Die Ersetzung wird in der string-Variable durchgeführt
        /// und das Ergebnis wieder auf die Datei geschrieben.
        /// Verwende File.ReadAllText und File.WriteAllText.
        /// </summary>
        /// <param name="fileName"></param>
        private static void ReplaceCharacters(string fileName)
        {
            string content = File.ReadAllText(fileName);

            Console.Write("Welche(s) Zeichen soll(en) ersetzt werden? ");

            string currentWord = Console.ReadLine()!;

            Console.Write("Wodurch? ");

            string newWord = Console.ReadLine()!;

            content = content.Replace(currentWord, newWord);

            File.WriteAllText(fileName, content);
        }

        /// <summary>
        /// Ausgabe der Textdatei auf die Konsole
        /// Verwende ReadAllText
        /// </summary>
        /// <param name="fileName"></param>
        private static void PrintFile(string fileName)
        {
            string lines = File.ReadAllText(fileName);

            Console.WriteLine("------- Ausgabe Start -------");

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(lines);

            Console.ResetColor();

            Console.WriteLine("------- Ausgabe Ende -------");
        }
    }
}
