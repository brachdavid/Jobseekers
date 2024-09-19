namespace Jobseekers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommunicationService communicationService = new CommunicationService();
            communicationService.RunProgram();
        }
    }
}
