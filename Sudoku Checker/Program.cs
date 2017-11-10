using System;
using System.IO;

namespace Lucas_Program_2
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuChecker sCheck = new SudokuChecker(); 
            sCheck.LoadArray();
            Console.WriteLine("--------------------");
            Console.WriteLine("Sudoku Matrix");
            Console.WriteLine("--------------------");
            sCheck.PrintSudokuMatrix();            
            Console.WriteLine("--------------------");
            sCheck.CheckRows();
            Console.WriteLine("--------------------");
            sCheck.CheckColumns();
            Console.WriteLine("--------------------");
            sCheck.CheckAllBlocks();
            Console.WriteLine("--------------------");
            Console.ReadKey();
            
        }

        
    }
}
