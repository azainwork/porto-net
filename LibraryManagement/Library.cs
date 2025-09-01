using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
	public class Library
	{
		public List<Book> books;

		private readonly string dataFilePath = "books.csv";

		private int nextId = 1;

		public Library()
		{
			books = new List<Book>();
			LoadBooksFromFile();
		}

		public void AddBook(string title, string author, string isbn, int year)
		{
			try
			{
				if(string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
				{
					throw new ArgumentException("Judul dan penulis tidak boleh kosong!");
				}

				if  (year < 1900 || year > DateTime.Now.Year)
				{
					throw new ArgumentException($"Tahun harus antara 1900 dan {DateTime.Now.Year}");
				}

				if (books.Any(b => b.ISBN == isbn) )
				{
					throw new ArgumentException("ISBN sudah terdaftar!");
				}

				Book newBook = new Book(nextId, title, author, isbn, year);
				books.Add(newBook);
				nextId++;

				SaveBooksToFile();

                Console.WriteLine($"Buku '{title} berhasil ditambahkan!");
            }
			catch (Exception ex)
			{
                Console.WriteLine($"Error: {ex.Message}");
            }
		}

		public void DisplayAllBooks()
		{
			if (books.Count == 0)
			{
                Console.WriteLine("Tidak ada buku dalam perpustakaan.");
				return;
            }

            Console.WriteLine("\n [ DAFTAR BUKU ]");

			foreach (var book in books)
			{
                Console.WriteLine(book.ToString());
            }
            Console.WriteLine($"Total buku: {books.Count}");
        }

		public void SearchBooksByTitle(string title)
		{
			var foundBooks = books.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();

			if (foundBooks.Count == 0)
			{
                Console.WriteLine($"Tidak ada buku dengan judul yang mengandung '{title}'");
				return;
            }

            Console.WriteLine($"\n [ HASIL PENCARIAN: {title}");
			foreach (var book in foundBooks)
			{
                Console.WriteLine(book.ToString());
            }
        }

		public void SearchBooksByAuthor(string author)
		{
			var foundBooks = books.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();

			if (foundBooks.Count == 0 )
			{
                Console.WriteLine($"Tidak ada buku dari penulis yang mengandung '{author}'");
				return;
            }

			Console.WriteLine($"\n [ BUKU OLEH PENULIS: '{author}' ]");
			foreach (var book in foundBooks)
			{
				Console.WriteLine(book.ToString());
			}
		}

		public void BorrowBook(int bookId, string borrowerName)
		{
			try
			{
				var book = books.FirstOrDefault(b => b.Id == bookId);

				if (book == null)
				{
                    Console.WriteLine($"Buku dengan ID {bookId} tidak ditemukan!");
					return;
                }

				if (book.Borrow(borrowerName))
				{
					SaveBooksToFile();
                    Console.WriteLine($"Buku '{book.Title}' berhasil dipinjam oleh {borrowerName}");
                } else
				{
                    Console.WriteLine($"Buku '{book.Title}' sedang tidak tersedia");
                }
			} catch (Exception ex)
			{
                Console.WriteLine($"Error: {ex.Message}");
            }
		}

		public void ReturnBook(int bookId)
		{
			try
			{
				var book = books.FirstOrDefault(b => b.Id == bookId);

				if(book == null)
				{
                    Console.WriteLine($"Buku dengan ID {bookId} tidak ditemukan!");
					return;
                }

				if(book.Return())
				{
					SaveBooksToFile();
					Console.WriteLine($"Buku '{book.Title}' berhasil dikembalikan!");
				} else
				{
					Console.WriteLine($"Buku '{book.Title}' sudah tersedia!");
				}
			} catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public void RemoveBook(int bookId)
		{
			try
			{
				var book = books.FirstOrDefault(b => b.Id == bookId);

				if (book == null)
				{
					Console.WriteLine($"Buku dengan ID {bookId} tidak ditemukan!");
					return;
				}

				if (!book.IsAvailable)
				{
					Console.WriteLine($"Tidak dapat menghapus buku yang sedang dipinjam!");
					return;
				}

				books.Remove(book);
				SaveBooksToFile();
				Console.WriteLine($"Buku '{book.Title}' berhasil dihapus!");
			} catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public void DisplayAvailableBooks()
		{
			var availableBooks = books.Where(b => b.IsAvailable).ToList();

			if (availableBooks.Count == 0)
			{
				Console.WriteLine("Tidak ada buku yang tersedia saat ini.");
				return;
			}

			Console.WriteLine("\n [ BUKU YANG TERSEDIA ] ");
			foreach (var book in availableBooks)
			{
				Console.WriteLine(book.ToString());
			}
		}

		public void DisplayBorrowedBooks()
		{
			var borrowedBooks = books.Where(b => !b.IsAvailable).ToList();

			if (borrowedBooks.Count == 0)
			{
				Console.WriteLine("Tidak ada buku yang sedang dipinjam.");
				return;
			}

			Console.WriteLine("\n [ BUKU YANG DIPINJAM ] ");
			foreach (var book in borrowedBooks)
			{
				Console.WriteLine(book.ToString());
			}
		}

		private void SaveBooksToFile()
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(dataFilePath))
				{
					foreach (var book in books)
					{
                        Console.WriteLine(book.ToCSV());
                    }
				}
			} catch (Exception ex)
			{
				Console.WriteLine($"Error menyimpan file: {ex.Message}");
			}
		}

		private void LoadBooksFromFile()
		{
			try
			{
				if (File.Exists(dataFilePath))
				{
					string[] lines = File.ReadAllLines(dataFilePath);

					foreach (string line in lines)
					{
						if(!string.IsNullOrWhiteSpace(line))
						{
							Book book = Book.FromCSV(line);
							books.Add(book);

							if (book.Id >= nextId)
							{
								nextId = book.Id + 1;
							}
						}
					}
				}
			} catch (Exception ex)
			{
				Console.WriteLine($"Error memuat file: {ex.Message}");
			}
		}

		public void DisplayStatistics()
		{
			int totalBooks = books.Count;
			int availableBooks = books.Count(b => b.IsAvailable);
			int borrowedBooks = books.Count(b => !b.IsAvailable);

			Console.WriteLine("\n [ STATISTIK PERPUSTAKAAN ]");
			Console.WriteLine($"Total buku: {totalBooks}");
			Console.WriteLine($"Buku tersedia: {availableBooks}");
			Console.WriteLine($"Buku dipinjam: {borrowedBooks}");

			if (totalBooks > 0)
			{
				double availabilityRate = (double)availableBooks / totalBooks * 100;
				Console.WriteLine($"Tingkat ketersediaan: {availabilityRate:F1}%");
			}
		}

	}
}
