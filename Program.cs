// Aaron Fraga

namespace HexConverter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                ConverterService converterService = new ConverterService(args);
                bool fileTask = await converterService.ConverterFile();

                if (fileTask)
                {
                    converterService.FinishedMessage();
                }

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("################## ERROR ##################\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex.Message);
            }
        }
    }
}