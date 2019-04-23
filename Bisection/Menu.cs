using System;
using System.Linq;
using System.Threading;

namespace Bisection
{
    public class Menu
    {
        public void MainMenu()
        {
            Console.WriteLine("Welcome to the main menu" +
                              "Pick one of the following:\n\n\n");

            Console.WriteLine("Press 1: Human picks a number\n" +
                              "Press 2: Machine picks a number");

            var input = Console.ReadKey().Key;

            switch (input)
            {
                case ConsoleKey.D1:
                    Console.WriteLine(HumanPickMenu());
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine(MachinePickMenu());
                    break;
            }
        }

        private string HumanPickMenu()
        {
            Console.Clear();
            Console.WriteLine("Pick a number between 0 and 100:\n\n\n");

            var humanNumber = Convert.ToInt32(Console.ReadLine()); //Assuming the human will input a valid number

            var array = new int[100];
            var count = 1;

            for (var i = 0; i < array.Length; i++) array[i] = i;

            return Search(array, count, humanNumber);
        }

        //Recursive search, passing an array we work with, current count, user's input.

        private string Search(int[] array, int count, int humanNumber)
        {
            var machineNumber = (int)array.Average();
            int indexBuffer;

            if (humanNumber == machineNumber) return $"Machine guessed human's number :{machineNumber}, in {count} attempts.";

            var tempArray = new int[array.Length / 2];
            var indexZero = 0;

            if (humanNumber > machineNumber)
            {
                indexBuffer = array.Length / 2;
            }
            else
            {
                indexBuffer = indexZero;
                machineNumber = array[array.Length / 4];
            }

            for (var i = 0; i < array.Length/2; i++)
            {
                tempArray[i] = array[indexBuffer];
                indexBuffer++;
            }

            count++;

            Console.WriteLine($"current array length: {array.Length}, machineNumber: {machineNumber}, count: {count}");

            return Search(tempArray, count, humanNumber);
        }

        //Recursive method, but much simpler, since the only parameter we are passing is count and current random picked number
        private string MachinePickMenu(int count = 1, int machineNumber = 0)
        {
            //Check for initial count, to avoid reinitializing random variable
            if (count == 1)
            {
                Console.Clear();

                var random = new Random();
                machineNumber = random.Next(0, 1000);
            }

            Console.WriteLine("Enter a number between 0 and 100:\n\n\n");
            Console.WriteLine(machineNumber + "\n\n\n");
            var input = Convert.ToInt32(Console.ReadLine());

            if (input == machineNumber)
                return $"You guessed machine's number :{input}, in {count} attempts.";

            else if (input > machineNumber)
                Console.WriteLine("Too big");
            else
                Console.WriteLine("Too small");

            count++;

            return MachinePickMenu(count, machineNumber);
        }
    }
}