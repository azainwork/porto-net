using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public int Year { get; set; }
		public bool IsAvailable { get; set; }
		public DateTime? BorrowedDate { get; set; }
		public string? BorrowedBy { get; set; }

		public Book(int id, string title, string author, string isbn, int year)
		{
			Id = id;
			Title = title;
			Author = author;
			ISBN = isbn;
			Year = year;
			IsAvailable = true;
			BorrowedDate = null;
			BorrowedBy = null;
		}

		public bool Borrow(string borrowerName)
		{
			if(IsAvailable)
			{
				IsAvailable = false;
				BorrowedDate = DateTime.Now;
				BorrowedBy = borrowerName;
				return true;
			}
			return false;
		}

		public bool Return()
		{
			if (!IsAvailable)
			{
				IsAvailable = true;
				BorrowedDate = null;
				BorrowedBy = null;
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			string status = IsAvailable ? "Tersedia" : $"Dipinjam oleh {BorrowedBy}";
			return $"ID: {Id} | Judul: {Title} | Penulis: {Author} | ISBN: {ISBN} | Tahun: {Year} | Status: {status}";
		}

		public string ToCSV()
		{
			return $"{Id},{Title},{Author},{ISBN},{Year},{IsAvailable},{BorrowedDate},{BorrowedBy}";
		}

		public static Book FromCSV(string csvLine)
		{
			string[] values = csvLine.Split(',');

			if(values.Length >= 5)
			{
				int id = int.Parse(values[0]);
				string title = values[1];
				string author = values[2];
				string isbn = values[3];
				int year = int.Parse(values[4]);

				Book book = new(id, title, author, isbn, year);

				if (values.Length > 5 && bool.TryParse(values[5], out bool isAvailable))
				{
					book.IsAvailable = isAvailable;
				}

				if (values.Length > 6 && DateTime.TryParse(values[6], out DateTime borrowedDate))
				{
					book.BorrowedDate = borrowedDate;
				}

				if (values.Length > 7)
				{
					book.BorrowedBy = values[7];
				}

				return book;
			}

			throw new ArgumentException("Format CSV tidak valid");
		}
	}
}
