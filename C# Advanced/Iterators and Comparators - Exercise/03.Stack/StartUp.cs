namespace _03.Stack
{
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] data = Console.ReadLine()
                .Split(new[] {' ',','}, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToArray();
            Stack<string> stack = new Stack<string>();

            foreach (var str in data)
            {
                stack.Push(str);
            }
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                switch (input)
                {
                    case "Pop":
                        try
                        {
                            stack.Pop();
                        }
                        catch (InvalidOperationException e)
                        {

                            Console.WriteLine(e.Message);
                        }
                        break;
                    default:

                        string element = input.Split()[1];
                        stack.Push(element);
                        break;
                }
            }
            Console.WriteLine(string.Join(Environment.NewLine,stack));
            Console.WriteLine(string.Join(Environment.NewLine,stack));
        }
    }
}
