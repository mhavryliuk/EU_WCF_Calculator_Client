using System;
using System.ServiceModel;

namespace _20180412_WCF_Calculator_Client
{
    internal class Client
    {
        private static void Main()
        {
            Console.Title = "Calculator (Client)";

            var client = new RemoteCalculator.CalculatorClient();

            try
            {
                // Connection test
                Console.Write("Checking connections to the service... ");
                if (!string.Equals(client.TestConnection(), "OK", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("Connection check failed!");
                }

                Console.WriteLine(client.TestConnection());
                Console.WriteLine();


                // Getting two numbers from a user
                double firstNumber, secondNumber;
                do
                {
                    Console.Write("Input the first number: ");
                } while (!double.TryParse(Console.ReadLine(), out firstNumber));

                do
                {
                    Console.Write("Input the second number: ");
                } while (!double.TryParse(Console.ReadLine(), out secondNumber));

                Console.WriteLine();

                // Addition
                Console.WriteLine($"{firstNumber} + {secondNumber} = {client.AddNumber(firstNumber, secondNumber):n} ");

                // Subtraction
                Console.WriteLine($"{firstNumber} - {secondNumber} = {client.SubNumber(firstNumber, secondNumber):n} ");

                // Multiplication
                Console.WriteLine(
                    $"{firstNumber} * {secondNumber} = {client.MultNumber(firstNumber, secondNumber):n} ");

                // Division
                Console.WriteLine($"{firstNumber} / {secondNumber} = {client.DivNumber(firstNumber, secondNumber):n} ");

                try
                {
                    client.Close();
                }
                catch (CommunicationException ex)
                {
                    client.Abort();
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (TimeoutException ex)
                {
                    client.Abort();
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    client.Abort();
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}