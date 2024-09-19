namespace Jobseekers
{
    /// <summary>
    /// Hlavní vstupní třída aplikace Jobseekers
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Inicializace komunikační služby pro interakci s uživatelem
            CommunicationService communicationService = new CommunicationService();

            // Spuštění hlavní metody pro ovládání programu
            communicationService.RunProgram();
        }
    }
}
