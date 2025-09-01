namespace ConsoleCalculator
{
	internal class Program
	{
		static void Main(string[] args)
		{
            Console.WriteLine("[ KALKULATOR SEDERHANA ]");
            Console.WriteLine();

			while (true)
			{
				DisplayMenu();

				string choice = GetUserInput("Pilih operasi (1-5): ");


				switch (choice)
				{
					case "1":
						PerformAddition();
						break;
					case "2":
						PerformSubstraction();
						break;
					case "3":
						PerformMultiplication();
						break;
					case "4":
						PerformDivision();
						break;
					case "5":
                        Console.WriteLine("Penggunaan Kalkulator Selesai");
						return;
					default:
                        Console.WriteLine("Pilihan tidak valid! Silahkan pilih 1-5.");
						break;
                }

				Console.WriteLine("\n Tekan Enter untuk melanjutkan...");
				Console.ReadLine();
				Console.Clear();
            }
        }

		static void DisplayMenu()
		{
            Console.WriteLine("\n [ MENU OPERASI ]");
            Console.WriteLine("1. Penjumlahan (+)");
			Console.WriteLine("2. Pengurangan (-)");
			Console.WriteLine("3. Perkalian (*)");
			Console.WriteLine("4. Pembagian (/)");
			Console.WriteLine("5. Keluar");
            Console.WriteLine();
        }

		static string GetUserInput(string message)
		{
			Console.Write(message);
			return Console.ReadLine();
		}

		static double GetNumber(string message)
		{
			while(true)
			{
                Console.WriteLine(message);
				string input = Console.ReadLine();

				if (double.TryParse(input, out double number))
				{
					return number;
				} else
				{
                    Console.WriteLine("Input tidak valid. Silahkan masukkan angka");
                }
            }
		}

		static void PerformAddition()
		{
			Console.WriteLine("\n [ PENJUMLAHAN ]");
			double num1 = GetNumber("Masukkan angka pertama: ");
			double num2 = GetNumber("Masukkan angka kedua: ");

			double result = num1 + num2;

			Console.WriteLine($"Hasil: {num1} + {num2} = {result}");
		}

		static void PerformSubstraction()
		{
            Console.WriteLine("\n [ PENGURANGAN ]");

			double num1 = GetNumber("Masukkan angka pertama: ");
			double num2 = GetNumber("Masukkan angka kedua: ");

			double result = num1 - num2;

			Console.WriteLine($"Hasil: {num1} - {num2} = {result}");
		}

		static void PerformMultiplication()
		{
			Console.WriteLine("\n=== PERKALIAN ===");

			double num1 = GetNumber("Masukkan angka pertama: ");
			double num2 = GetNumber("Masukkan angka kedua: ");

			double result = num1 * num2;

			Console.WriteLine($"Hasil: {num1} * {num2} = {result}");
		}

		static void PerformDivision()
		{
			Console.WriteLine("\n=== PEMBAGIAN ===");

			double num1 = GetNumber("Masukkan angka pertama: ");
			double num2;

			while (true)
			{
				num2 = GetNumber("Masukkan angka kedua: ");

				if (num2 != 0)
				{
					break;
				}
				else
				{
					Console.WriteLine("Error: Pembagi tidak boleh nol! Silakan coba lagi.");
				}
			}

			double result = num1 / num2;

			Console.WriteLine($"Hasil: {num1} / {num2} = {result:F2}");
		}

	}
}
