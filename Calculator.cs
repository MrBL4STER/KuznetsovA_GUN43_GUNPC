class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the first number: ");
        if (!Int32.TryParse(Console.ReadLine(), out var a))
        {
            Console.WriteLine("Not a number!");
            return;
        }

        Console.Write("Enter the second number: ");
        if (!Int32.TryParse(Console.ReadLine(), out var b))
        {
            Console.WriteLine("Not a number!");
            return;
        }

        Console.Write("Enter a sign('&', '|', or '^'): ");
        var s = Console.ReadLine();
        var boolVar = true;
        if (s.Length == 0 || s.Length > 1 && !boolVar)
        {
            Console.WriteLine("Wrong sign");
            return;
        }

        switch (s[0])
        {
            case '&':
                Console.WriteLine("Result of {0} & {1} = {2} in the decimal system", a, b, a & b);
                Console.WriteLine("Result of {0} & {1} = {2} in the binary number system", a, b, Convert.ToString((a & b), 2));
                Console.WriteLine("Result of {0} & {1} = {2} in the hexadecimal system", a, b, Convert.ToString((a & b), 16));
                break;
            case '|':
                Console.WriteLine("Result of {0} | {1} = {2} in the decimal system", a, b, a | b);
                Console.WriteLine("Result of {0} | {1} = {2} in the binary number system", a, b, Convert.ToString((a | b), 2));
                Console.WriteLine("Result of {0} | {1} = {2} in the hexadecimal system", a, b, Convert.ToString((a | b), 16));
                break;
            case '^':
                Console.WriteLine("Result of {0} ^ {1} = {2} in the decimal system", a, b, a ^ b);
                Console.WriteLine("Result of {0} ^ {1} = {2} in the binary number system", a, b, Convert.ToString((a ^ b), 2));
                Console.WriteLine("Result of {0} ^ {1} = {2} in the hexadecimal system", a, b, Convert.ToString((a ^ b), 16));
                break;
            default: Console.WriteLine("Wrong sign");
                break;
        } 
    }
}