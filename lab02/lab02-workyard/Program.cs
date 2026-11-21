using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;
using WordModules;

class Program {
  public static void ProcessAll(IWordReader reader, IWordProcessor processor) {
    string? word;
    while((word = reader.ReadWord()) != null) processor.ProcessWord(word);
    processor.ProcessEnd();        
  }

  public static void Main(string[] args) {
    if (args.Length == 1) { 
      try { 
        IWordReader reader = new TextWordReader(File.OpenText(args[0] + ""));
        IWordProcessor processor = new FrequencyCounter();
        ProcessAll(reader, processor);
      } catch(UnauthorizedAccessException) {
        Console.WriteLine("File Error");
      } catch(FileNotFoundException) {
        Console.WriteLine("File Error");
      } catch(IOException) {
        Console.WriteLine("File Error");
      }
    } else {
      Console.WriteLine("Argument Error");
    }
  }
}
