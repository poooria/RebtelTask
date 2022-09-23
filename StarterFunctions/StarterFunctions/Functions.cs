namespace StarterFunctions;

public class Functions
{
    public bool Is2Power(int input)
    {
        return (input & (input - 1)) == 0 && input > 0;
    }

    public string Reverse(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    public string Replicate(string input,int count)
    {
        return string.Concat(Enumerable.Repeat(input, count));
    }
    public IEnumerable<int> PrintOddNumbers()
    {
        for (int n = 0; n <= 100; n++)
        {
            if (n % 2 != 0)
            {
                yield return n;
            }
        }
    }

}