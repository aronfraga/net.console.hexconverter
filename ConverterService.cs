using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexConverter
{
    public class ConverterService
    {

        private readonly string[] _args;
        private string _inPath;
        private string _outPath;
        private bool _read;
        private bool _continue = true;

        public ConverterService(string[] args)
        {
            _args = args;

            if (_args.Length > 0)
            {
                _read = ReadArguments();
            }
            else
            {
                _continue = HelpMessage();
            }
        }

        public async Task<bool> ConverterFile()
        {
            if (_continue)
            {
                if (!_read || string.IsNullOrEmpty(_inPath) || string.IsNullOrEmpty(_outPath))
                {
                    throw new MissingArgumentsException();
                }

                string inContext = await File.ReadAllTextAsync(_inPath);
                byte[] bytes = Encoding.UTF8.GetBytes(inContext);
                string outContent = BitConverter.ToString(bytes).Replace("-", "");
                string outContentWith0x = $"0x{outContent}";

                await File.WriteAllTextAsync(_outPath, outContentWith0x);
                await Task.CompletedTask;

                return true;
            }
            return false;
        }

        public void FinishedMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("################## DONE ##################\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"File In: {_inPath}");
            Console.WriteLine($"File Out: {_outPath}\n");

            Console.ReadKey();
        }

        public bool HelpMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("################## HELP ##################\n");
            Console.WriteLine("Flags:\n");
            Console.WriteLine("-i : + Input file path to convert to hex");
            Console.WriteLine(@"    EXAMPLE: -i C:\MyFolder\MyInFile.xml");
            Console.WriteLine("-o : + Output file path to create the txt with the hex inside");
            Console.WriteLine(@"    EXAMPLE: -o C:\MyFolder\MyOutFile.txt");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();

            return false;
        }

        private bool ReadArguments()
        {
            int count = 0;

            foreach (string arg in _args)
            {
                if (arg.ToLower() == "-i" || arg.ToLower() == "-o")
                {
                    count++;
                }

                if (count == 1 && arg is not null)
                {
                    _inPath = arg;
                }

                if (count == 2 && arg is not null)
                {
                    _outPath = arg;
                }
            }

            if (count == 2)
            {
                return true;
            }

            return false;
        }

    }
}
