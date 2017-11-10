using System;
using System.IO;

namespace Lucas_Program_2
{
    public class SudokuChecker
    {
        const int ARRAY_ROW = 9;//total number of rows
        const int ARRAY_COLUMN = 9;//total number of columns
        const int TOTAL_BLOCKS = 9;//total number of blocks in sudokuMatrix[], used in checkAllBlocks();
        const int BLOCK_HEIGHT = 3;//total number of blocks
        const int BLOCK_WIDTH = 3;//total number of blocks

        private string[,] sudokuMatrix = new string[ARRAY_ROW, ARRAY_COLUMN];//matrix used to check row and column validity

        private int row { get; set; }//tracks rows while iterating through sudokuMatrix[]
        private int column { get; set; }//tracks columns while iterating through sudokuMatrix[]
        private int blockHeightSetter { get; set; }
        private int blockWidthMultiplier { get; set; }

        private int numRowErrors = 0;//tracks of number of invalid row entries
        private int numColumnErrors = 0;//tracks of number of invalid column entries
        private int numBlockErrors = 0;//tracks of number of invalid block entries

        /// <summary>
        /// loads a comma separated text file into a 2D array that mirrors the .txt file
        /// </summary>
        public void LoadArray()
        {

            FileStream fs = new FileStream("sudoku-bad-1.txt", FileMode.Open, FileAccess.Read);
            StreamReader input = new StreamReader(fs);
            row = 0;

            while (row < ARRAY_ROW)
            {
                column = 0;
                string line = input.ReadLine();
                string[] fields = line.Split(",");
                for (column = 0; column < ARRAY_COLUMN; column++)
                {
                    sudokuMatrix[row, column] = fields[column];

                }
                row++;
            }
            fs.Close();
        }
 
        /// <summary>
        /// Prints sudokuMatrix[] to the console 
        /// </summary>
        public void PrintSudokuMatrix()
        {
            int lineCount = 0;
            foreach (string num in sudokuMatrix)
            {
                Console.Write("-{0}", num);
                lineCount++;
                if (lineCount % ARRAY_COLUMN == 0)
                {
                    Console.Write("-\n");
                }
            }

        }
        
        /// <summary>
        /// Checks sudokuMatrix[] rows for repeated numbers, if one is found an error message is printed and numRowErrors is incrememnted. 
        /// If no errors are found a message is also printed to notify user that all rows are valid
        /// </summary>
        public void CheckRows()
        {
            for (row = 0; row < ARRAY_ROW; row++) {
                for (column = 0; column < ARRAY_COLUMN;column++) {
                    string checkValue = sudokuMatrix[row,column];
                    for (int col = column + 1; col < ARRAY_COLUMN ;col++) {
                        if (checkValue.CompareTo(sudokuMatrix[row, col]) == 0) {
                            Console.WriteLine("Error in Row: {0}",row);
                            numRowErrors++;
                        }
                    }
                }
            }
            if (numRowErrors == 0)
            {
                Console.WriteLine("All Rows Valid!");
            }
        }
        /// <summary>
        /// Checks sudokuMatrix[] columns for repeated numbers, if one is found an error message is printed and numColumnErrors is incrememnted. 
        /// If no errors are found a message is also printed to notify user that all columns are valid
        /// </summary>
        public void CheckColumns() {            
            for (column = 0; column < ARRAY_COLUMN; column++) {
                for (row = 0; row < ARRAY_ROW; row++) {
                    string checkValue = sudokuMatrix[row, column];
                    for (int rw = row + 1; rw < ARRAY_ROW; rw++) {
                        if (checkValue.CompareTo(sudokuMatrix[rw, column]) == 0){
                            Console.WriteLine("Error in Column: {0}", column);
                            numColumnErrors++;
                        }
                    }
                }
                
            }
            if (numColumnErrors == 0)
            {
                Console.WriteLine("All Columns Valid!");
            }
        }
        /// <summary>
        /// Uses blockNumber to extrapolate correct block beginning positions and end postions.  Iterates through each block checking 
        /// to see if any numbers are repeated within the block.  If a repeated number is found, method prints a message to the user 
        /// telling them whick block the error was in.
        /// <paramref name="blockNumber"/> used to calculate iterations through 2d array and to output block errors to console
        /// </summary>
        public void CheckBlock(int blockNumber) {

            if (blockNumber >= 0 && blockNumber < 9) {//sets blockWidthMultiplier based on blockNumber, this helps determine which columns in the 2d array the method needs to check for each block 
                blockWidthMultiplier = 0;
                if (blockNumber >= 3 && blockNumber < 9) {
                    blockWidthMultiplier = 1;
                    if (blockNumber >= 6 && blockNumber < 9) {
                        blockWidthMultiplier = 2;
                    }
                }
            }
            else { Console.WriteLine("Invalid Block Number!"); }
            blockHeightSetter = 3 * blockWidthMultiplier;//sets blockHeightSetter based on blockWidthMultiplier, this helps to determine which Rows in the 2d array the method needs to check for each block
            Boolean errorFound = false;//without this the method will alert the user twice when there is a duplicate, this ensures that only one alert is displayed
            for (row = blockHeightSetter; row < BLOCK_HEIGHT + blockHeightSetter; row++) {//iterate through all rows of the block
                for (column = (blockNumber - (blockWidthMultiplier * BLOCK_WIDTH)) * BLOCK_WIDTH; column < ((blockNumber - (BLOCK_WIDTH * blockWidthMultiplier)) + 1) * BLOCK_WIDTH; column++) {//iterate through all columns in the block, each pass through the checkvalue is set to the current row
                    string checkValue = sudokuMatrix[row, column];//set the value that is to be checked against the entire sudokuMatrix[] block
                    for (int rw = blockHeightSetter; rw < BLOCK_HEIGHT + blockHeightSetter; rw++) {//iterate through all rows in block
                        for (int col = (blockNumber - (blockWidthMultiplier * BLOCK_WIDTH)) * BLOCK_WIDTH; col < ((blockNumber - (BLOCK_WIDTH * blockWidthMultiplier)) + 1) * BLOCK_WIDTH; col++) {//iterate through all columns comparing the checkValue to the current cells value
                            if ((row != rw || column != col)) {//don't compare a cell to itself
                                if (checkValue.CompareTo(sudokuMatrix[rw, col]) == 0) {//if cell contents match
                                    if (!errorFound) {//if we haven't already found a mistake in the block
                                        Console.WriteLine("Error in Block: {0}", blockNumber);//print message to user that displays which block the error was found in
                                        numBlockErrors++;
                                        errorFound = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// iterates through the total number of blocks, uses CheckBlock() for each block and TOTAL_BLOCK to stop iteration 
        /// </summary>
        public void CheckAllBlocks() {
            for (int block = 0; block < TOTAL_BLOCKS ;block++) {
                CheckBlock(block);
            }
            if (numBlockErrors == 0) {
                Console.WriteLine("All Blocks Valid!");
            }
        }
    }   
}
