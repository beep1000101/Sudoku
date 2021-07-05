using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Table {

    class SudokuMember {
        private byte value;
        private byte[] possibleValues;


        public SudokuMember() {
            this.value = 0;
            this.possibleValues = new byte[] { };
        }

        public byte PrintValue() {
            return this.value;
        }

        public void AddPossibleValues(ref byte[] possibleValues, ref byte value) {
            possibleValues = possibleValues.Concat(new byte[] { value }).ToArray();
        }

        public void SortPossibleValues(ref byte[] possibleValues) {
            Array.Sort(possibleValues);
        }
    }


    class SudokuTable {

        private byte[,] numbers;
        private bool[,] isFixed;
        private byte heightOfTable;
        private byte lengthOfTable;


        public SudokuTable() {
            //this.numbers = new byte[,] { { 3, 2, 1, 7, 0, 4, 0, 0, 0 },
            //                             { 6, 4, 0, 0, 9, 0, 0, 0, 7 },
            //                             { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            //                             { 0, 0, 0, 0, 4, 5, 9, 0, 0 },
            //                             { 0, 0, 5, 1, 8, 7, 4, 0, 0 },
            //                             { 0, 0, 4, 9, 6, 0, 0, 0, 0 },
            //                             { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            //                             { 2, 0, 0, 0, 7, 0, 0, 1, 9 },
            //                             { 0, 0, 0, 6, 0, 9, 5, 8, 2 } };


            this.numbers = new byte[,] { { 3, 2, 0, 7, 0, 4, 0, 0, 0 },
                                         { 6, 4, 0, 0, 9, 0, 0, 0, 7 },
                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                         { 0, 0, 0, 0, 4, 5, 9, 0, 0 },
                                         { 0, 0, 5, 1, 8, 7, 4, 0, 0 },
                                         { 0, 0, 4, 9, 6, 0, 0, 0, 0 },
                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                         { 2, 0, 0, 0, 7, 0, 0, 1, 9 },
                                         { 0, 0, 0, 6, 0, 9, 5, 8, 2 } };

            //this.numbers = new byte[,] { {3, 0, 6, 5, 0, 8, 4, 0, 0},
            //                             {5, 2, 0, 0, 0, 0, 0, 0, 0},
            //                             {0, 8, 7, 0, 0, 0, 0, 3, 1},
            //                             {0, 0, 3, 0, 1, 0, 0, 8, 0},
            //                             {9, 0, 0, 8, 6, 3, 0, 0, 5},
            //                             {0, 5, 0, 0, 9, 0, 6, 0, 0},
            //                             {1, 3, 0, 0, 0, 0, 2, 5, 0},
            //                             {0, 0, 0, 0, 0, 0, 0, 7, 4},
            //                             {0, 0, 5, 2, 0, 6, 3, 0, 0} };




            this.heightOfTable = (byte)this.numbers.GetLength(1);
            this.lengthOfTable = (byte)this.numbers.GetLength(0);

            this.isFixed = new bool[this.heightOfTable, this.lengthOfTable];

            for (byte row = 0; row < this.lengthOfTable; row++) {
                for (byte column = 0; column < this.heightOfTable; column++) {
                    if (PrintValue(column, row) != 0) SetIsFixed(column, row, true);
                    else SetIsFixed(column, row, false);
                }
            }
        }

        public byte PrintHeight() {
            return this.heightOfTable;
        }

        public byte PrintLength() {
            return this.lengthOfTable;
        }

        public byte PrintValue(byte column, byte row) {
            return this.numbers[column, row];
        }

        public bool PrintBoolValue(byte column, byte row) {
            return this.isFixed[column, row];
        }

        public void SetValue(byte column, byte row, byte value) {
            this.numbers[column, row] = value;
        }

        public void SetIsFixed(byte column, byte row, bool valueBool) {
                this.isFixed[column, row] = valueBool;
            }

        public void PrintTable() {
            byte heightOfTable = PrintHeight();
            byte lengthOfTable = PrintLength();

            for (byte row = 0; row < lengthOfTable; row++) {
                for (byte column = 0; column < heightOfTable; column++) {
                    Console.Write(PrintValue(row, column) + " ");
                }
                Console.Write("\n");
            }
        }

        // that was to check if the table is properly set
        public void PrintBoolTable() {
            byte heightOfTable = PrintHeight();
            byte lengthOfTable = PrintLength();

            for (byte row = 0; row < lengthOfTable; row++) {
                for (byte column = 0; column < heightOfTable; column++) {
                    Console.Write(PrintBoolValue(row, column) + " ");
                }
                Console.Write("\n");
            }
        }

        public bool CheckRows(byte column, byte row, byte candidate) {
            for (byte rowCheck = 0; rowCheck < this.lengthOfTable; rowCheck++)
                if (this.numbers[column, rowCheck] == candidate)
                    return false;
            return true;
        }

        public bool CheckColumns(byte column, byte row, byte candidate) {
            for (byte columnCheck = 0; columnCheck < this.heightOfTable; columnCheck++)
                if (this.numbers[columnCheck, row] == candidate)
                    return false;
            return true;
        }

        public bool CheckSquare(byte column, byte row, byte candidate) {
            for (byte squareColumn = 0; squareColumn < 3; squareColumn++)
                for (byte squareRow = 0; squareRow < 3; squareRow++) {
                    byte properSquareColumn = (byte)(column - column % 3 + squareColumn);
                    byte properSquareRow = (byte)(row - row % 3 + squareRow);
                    if (this.numbers[properSquareColumn, properSquareRow] == candidate)
                        return false;
                }
            return true;
        }

        public bool IsThisPossible(byte column, byte row, byte candidate) {
            bool columnBoolValue = CheckColumns(column, row, candidate);
            bool rowBoolValue = CheckRows(column, row, candidate);
            bool squareBoolValue = CheckSquare(column, row, candidate);
            return columnBoolValue && rowBoolValue && squareBoolValue;
        }

        public void NormalBacktrackingSolve() {
            for (byte column = 0; column < this.heightOfTable; column++) {
                for (byte row = 0; row < this.lengthOfTable; row++) {
                    if (this.numbers[column, row] == 0) {
                        for(byte candidate = 1; candidate < 10; candidate++) {
                            if (IsThisPossible(column, row, candidate)) {
                                SetValue(column, row, candidate);
                                NormalBacktrackingSolve();
                                SetValue(column, row, 0);
                            }
                        }
                        return;
                    }
                }
            }
            PrintTable();
            Console.WriteLine("\n\n\n");
        }
    }

        //class Program {
        //    static void Main(string[] args) {
        //        SudokuTable sudokutable = new SudokuTable();
        //
        //    sudokutable.PrintTable();
        //    Console.WriteLine("\n\n\n");
        //    sudokutable.NormalBacktrackingSolve();
        //    Console.ReadKey();
        //    }
        //}
}