using System;

namespace ListMaker
{
    internal class DecisionResult
    {
        internal enum DecisionCode // access with - DecisionCode.Add
        {
            Add, Subtract, Exit
        }

        internal DecisionCode Ask()
        {
        bool decided = false;
            while (!decided)
	        {
                Console.WriteLine("Add (+), subtract (-), or exit (^)?");
                string answer = Console.ReadLine().ToLower();
                switch (answer)
                {
                    case "add":
                        decided = true;
                        return DecisionCode.Add;
                    case "subtract":
                        decided = true;
                        return DecisionCode.Subtract;
                    case "exit":
                        decided = true;
                        return DecisionCode.Exit;
                    case "+":
                        decided = true;
                        return DecisionCode.Add;
                    case "-":
                        decided = true;
                        return DecisionCode.Subtract;
                    case "^":
                        decided = true;
                        return DecisionCode.Exit;
                    default:
                        Console.WriteLine("Please make a choice from the list:");
                        break;
                }
	        }
            return DecisionCode.Add; // This line prevents a compile error, though it should never be reached.               
        }
    }
}