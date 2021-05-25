using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Entities
{
    public class Book : EntityObject
    {
        [ForeignKey("Publisher_Id")]
        public Publisher Publisher { get; set; }
    
        public int Publisher_Id { get; set; }

        
        public string Title { get; set; }

        public string Authors { get; set; }

        public string Isbn { get; set; }

        public Book()
        {
            
        }

        /// <summary>
        /// Eine gültige ISBN-Nummer besteht aus den Ziffern 0, ... , 9,
        /// 'x' oder 'X' (nur an der letzten Stelle)
        /// Die Gesamtlänge der ISBN beträgt 10 Zeichen.
        /// Für die Ermittlung der Prüfsumme werden die Ziffern 
        /// von LINKS nach RECHTS mit 10 bis 1 multipliziert und die 
        /// Produkte aufsummiert. Ist das rechte Zeichen ein x oder X
        /// wird dieses X als Zahlenwert 10 verwendet.
        /// Die Prüfsumme muss modulo 11 0 ergeben.
        /// 
        /// Beispiel: 3760778739 = 3*10+7*9+6*8+0*7+7*6+7*5+8*4+7*3+3*2+9*1 = 286 % 11 = 0 --> gültig
        /// </summary>
        /// <returns>Prüfergebnis</returns>
        public static bool CheckIsbn(string isbn)
        {
            if (String.IsNullOrEmpty(isbn) || isbn.Length != 10)
            {
                Debug.WriteLine($"!!! isbn {isbn} has no length of 10!");
                return false;
            }
            int weight = 10;  // Startgewicht
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                var ch = isbn[i];
                int number;
                if (char.IsDigit(ch))
                {
                    number = ch - '0';
                }
                else  // keine Ziffer  => x oder X an letzter Stelle
                {
                    if (i != 9)
                    {
                        return false;
                    }
                    else
                    {
                        if (ch == 'x' || ch == 'X')
                        {
                            number = 10;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                // zahl enthält gültigen Wert
                sum += number * weight;
                weight--;
            }
            if (sum % 11 != 0)
            {
                Debug.WriteLine($"!!! isbn {isbn} checksum is {sum % 11} (should be 0!");
            }
            return (sum % 11) == 0;
        }


        public override string ToString()
        {
            return $"{Title} {Isbn}";
        }
    }
}

