namespace Jobseekers
{
    /// <summary>
    /// Hlavní vstupní třída aplikace Jobseekers
    /// </summary>
    public class Program
    {
        public static async Task Main()
        {
            try
            {
                // Inicializace komunikační služby pro interakci s uživatelem
                CommunicationService communicationService = new();

                // Spuštění hlavní asynchronní metody pro ovládání programu
                await communicationService.RunProgramAsync();
            }
            catch (Exception ex)
            {
                // Logování chyb a informování uživatele
                Console.WriteLine($"Neočekávaná chyba: {ex.Message}");
                Console.WriteLine("Něco se pokazilo. Zkuste to prosím znovu nebo kontaktujte podporu.");
            }
        }
    }
}
