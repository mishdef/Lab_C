
using Microsoft.VisualBasic.FileIO;
using MyFunctions;
using static MyFunctions.Tools;
using Lab.Class;
using Lab.Interface;

namespace Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MessagesHandler.SetInfoMessage(InfoMessage);
            MessagesHandler.SetWarningMessage(WarningMessage);
            MessagesHandler.SetErrorMessage(ErrorMessage);

            ProjectBoard.WarningToMuchTasksInToDoEvent += ShowwarningMessageBox;
            ProjectBoard.WarningToMuchToMuchTasksInToDoEvent += ShowwarningMessageBox;

            bool exit = false;
            bool changeUser = false;
            string name = InputString("Company name: ");
            string CEOName = InputString("CEO name: ");

            Menu.SelectedItemColor = ConsoleColor.DarkCyan;

            User user = new CEO(CEOName);
            Company company = new Company(user, name);

            CompanyProjectsAppSession companyProjectsApp = new CompanyProjectsAppSession(company, user);
            companyProjectsApp.DisplayMenu();
        }

        static public void ShowwarningMessageBox(string message)
        {
            MessageBox.Show(message, "Warning", MessageBox.Buttons.Ok);
        }

        static public void InfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            MessageBox.BoxItem(message);
            Console.ResetColor();
        }

        static public void WarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            MessageBox.BoxItem(message);
            Console.ResetColor();
        }

        static public void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            MessageBox.BoxItem(message);
            Console.ResetColor();
        }
    }
}
