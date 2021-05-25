using Books.Core.Entities;
using Books.Persistence;
using System;
using System.Collections.Generic;

namespace Books.ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Import der Bücher in die Datenbank");
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.FillDb();
                int bookCnt = unitOfWork.BookRepository.Count();

                Console.WriteLine($"  Es wurden {bookCnt} Bücher eingelesen!");
                int pubCnt = unitOfWork.PublisherRepository.Count();
                Console.WriteLine($"  Es wurden {pubCnt} Verlage eingelesen!");

                Console.Write("Beenden mit Eingabetaste ...");
                Console.ReadLine();
            }
        }
    }
}
