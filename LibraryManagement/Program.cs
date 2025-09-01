namespace LibraryManagement
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("[ SISTEM MANAJEMEN PERPUSTAKAAN ]");
			Console.WriteLine("Selamat datang di sistem manajemen perpustakaan!");

			Library library = new Library();

			while (true)
			{
				DisplayMainMenu();
				string choice = GetUserInput("Pilih menu (1-9): ");

				switch (choice)
				{
					case "1":
						AddNewBook(library);
						break;
					case "2":
						library.DisplayAllBooks();
						break;
					case "3":
						SearchBooks(library);
						break;
					case "4":
						BorrowBook(library);
						break;
					case "5":
						ReturnBook(library);
						break;
					case "6":
						RemoveBook(library);
						break;
					case "7":
						DisplayBookStatus(library);
						break;
					case "8":
						library.DisplayStatistics();
						break;
					case "9":
						Console.WriteLine("Terima kasih telah menggunakan sistem perpustakaan!");
						return;
					default:
						Console.WriteLine("Pilihan tidak valid! Silakan pilih 1-9.");
						break;
				}

				Console.WriteLine("\nTekan Enter untuk melanjutkan...");
				Console.ReadLine();
				Console.Clear();
			}
		}

		static void DisplayMainMenu()
		{
			Console.WriteLine("\n [ MENU UTAMA ]");
			Console.WriteLine("1. Tambah Buku Baru");
			Console.WriteLine("2. Tampilkan Semua Buku");
			Console.WriteLine("3. Cari Buku");
			Console.WriteLine("4. Pinjam Buku");
			Console.WriteLine("5. Kembalikan Buku");
			Console.WriteLine("6. Hapus Buku");
			Console.WriteLine("7. Status Buku");
			Console.WriteLine("8. Statistik Perpustakaan");
			Console.WriteLine("9. Keluar");
			Console.WriteLine();
		}

		static string GetUserInput(string message)
		{
			Console.Write(message);
			return Console.ReadLine();
		}

		static int GetNumber(string message)
		{
			while (true)
			{
				Console.Write(message);
				string input = Console.ReadLine();

				if (int.TryParse(input, out int number))
				{
					return number;
				}
				else
				{
					Console.WriteLine("Input tidak valid! Silakan masukkan angka.");
				}
			}
		}

		static void AddNewBook(Library library)
		{
			Console.WriteLine("\n=== TAMBAH BUKU BARU ===");

			string title = GetUserInput("Judul buku: ");
			string author = GetUserInput("Penulis: ");
			string isbn = GetUserInput("ISBN: ");

			Console.Write("Tahun terbit: ");
			int year = GetNumber("Tahun terbit: ");

			library.AddBook(title, author, isbn, year);
		}

		static void SearchBooks(Library library)
		{
			Console.WriteLine("\n=== CARI BUKU ===");
			Console.WriteLine("1. Cari berdasarkan judul");
			Console.WriteLine("2. Cari berdasarkan penulis");

			string choice = GetUserInput("Pilih jenis pencarian (1-2): ");

			switch (choice)
			{
				case "1":
					string title = GetUserInput("Masukkan judul buku: ");
					library.SearchBooksByTitle(title);
					break;
				case "2":
					string author = GetUserInput("Masukkan nama penulis: ");
					library.SearchBooksByAuthor(author);
					break;
				default:
					Console.WriteLine("Pilihan tidak valid!");
					break;
			}
		}

		static void BorrowBook(Library library)
		{
			Console.WriteLine("\n=== PINJAM BUKU ===");

			library.DisplayAvailableBooks();

			int bookId = GetNumber("Masukkan ID buku yang ingin dipinjam: ");
			string borrowerName = GetUserInput("Masukkan nama peminjam: ");

			library.BorrowBook(bookId, borrowerName);
		}

		static void ReturnBook(Library library)
		{
			Console.WriteLine("\n=== KEMBALIKAN BUKU ===");

			library.DisplayBorrowedBooks();

			int bookId = GetNumber("Masukkan ID buku yang ingin dikembalikan: ");

			library.ReturnBook(bookId);
		}

		static void RemoveBook(Library library)
		{
			Console.WriteLine("\n=== HAPUS BUKU ===");

			library.DisplayAllBooks();

			int bookId = GetNumber("Masukkan ID buku yang ingin dihapus: ");

			string confirm = GetUserInput("Apakah Anda yakin ingin menghapus buku ini? (y/n): ");

			if (confirm.ToLower() == "y" || confirm.ToLower() == "yes")
			{
				library.RemoveBook(bookId);
			}
			else
			{
				Console.WriteLine("Penghapusan dibatalkan.");
			}
		}

		static void DisplayBookStatus(Library library)
		{
			Console.WriteLine("\n=== STATUS BUKU ===");
			Console.WriteLine("1. Buku yang tersedia");
			Console.WriteLine("2. Buku yang dipinjam");

			string choice = GetUserInput("Pilih status (1-2): ");

			switch (choice)
			{
				case "1":
					library.DisplayAvailableBooks();
					break;
				case "2":
					library.DisplayBorrowedBooks();
					break;
				default:
					Console.WriteLine("Pilihan tidak valid!");
					break;
			}
		}
	}
}
