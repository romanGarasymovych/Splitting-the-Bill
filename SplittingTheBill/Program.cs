using System;
using System.IO;

namespace SplittingTheBill
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Program pr = new Program();

            try
            {
                // Get path for input file
                Console.WriteLine("Enter file path:");
                string inputPath = Console.ReadLine();
                string outputPath = inputPath + ".out";

                // Check if output file exists. If not, create a new file.
                if (!File.Exists(outputPath))
                {
                    StreamWriter sw = File.CreateText(outputPath);
                    sw.Close();
                }

                // Analyse the input file
                using (StreamReader input = new StreamReader(inputPath))
                {
                    double[] data = { 0 };
                    string line;
                    while ((line = input.ReadLine()) != null)
                    {
                        int n = 0;
                        int.TryParse(line, out n);

                        if (line.Equals("0"))
                        {
                            pr.ProcessTrip(data, outputPath);
                        }
                        else
                        {
                            data = new double[n];
                            for (int i = 0; i < n; i++)
                            {
                                int p = 0;
                                int.TryParse(input.ReadLine(), out p);

                                for (int j = 0; j < p; j++)
                                {
                                    double amt = 0;
                                    double.TryParse(input.ReadLine(), out amt);

                                    data[i] += amt;
                                }
                            }
                            pr.ProcessTrip(data, outputPath);
                        }
                    }
                    input.Close();
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
            Console.ReadLine();
        }

        // Appends text into output file.
        public void AppendText(string text, string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(text);
                sw.Close();
            }
        }

        // Process the data from the input file, and push it to the output file.
        public void ProcessTrip(double[] data, string path)
        {
            Program pr = new Program();
            double sum = 0;

            // Get total trip sum
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }

            for (int i = 0; i < data.Length; i++)
            {
                double n = (sum / data.Length) - data[i];
                if (n < 0)
                {
                    pr.AppendText("($" + Math.Abs(Math.Round(n, 2)) + ")", path);
                }
                else
                {
                    pr.AppendText("$" + Math.Round(n, 2), path);
                }
            }
            pr.AppendText("", path);
        }
    }
}
