using System.Text;

namespace lab02_claude;

// Reads a file character by character and reports each word it finds.
abstract class WordProcessor
{
    static bool IsWhiteSpace(int c) => c == ' ' || c == '\t' || c == '\n';

    protected abstract void ProcessWord(StringBuilder word);

    public void ProcessReader(TextReader reader)
    {
        var word = new StringBuilder();
        int c;
        while ((c = reader.Read()) != -1)
        {
            if (!IsWhiteSpace(c))
            {
                word.Append((char)c);
            }
            else if (word.Length > 0)
            {
                ProcessWord(word);
                word.Clear();
            }
        }
        if (word.Length > 0)
            ProcessWord(word);
    }

    public abstract void WriteResult(TextWriter writer);
}

class WordCounter : WordProcessor
{
    int count;

    protected override void ProcessWord(StringBuilder word) => count++;

    public override void WriteResult(TextWriter writer) => writer.WriteLine(count);
}

class FrequencyCounter : WordProcessor
{
    readonly Dictionary<string, int> counts = new();

    protected override void ProcessWord(StringBuilder word)
    {
        var key = word.ToString();
        counts[key] = counts.GetValueOrDefault(key) + 1;
    }

    public override void WriteResult(TextWriter writer)
    {
        var words = counts.Keys.ToList();
        words.Sort();
        foreach (var word in words)
            writer.WriteLine($"{word}: {counts[word]}");
    }
}

class Program
{
    static void Run(WordProcessor processor, string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Argument Error");
            return;
        }

        try
        {
            using var reader = new StreamReader(args[0]);
            processor.ProcessReader(reader);
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException
            or ArgumentException or NotSupportedException or System.Security.SecurityException)
        {
            Console.WriteLine("File Error");
            return;
        }

        processor.WriteResult(Console.Out);
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        // Swap for new WordCounter() to get the total word count instead.
        Run(new FrequencyCounter(), args);
    }
}
