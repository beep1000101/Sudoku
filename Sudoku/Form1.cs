using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Table;

namespace Sudoku {
    public partial class Form1 : Form {

        const byte n = 3, labelSize = 50;
        private byte[,] sudokuMember = new byte[n * n, n * n];
        private SudokuTable sudokuTable = new SudokuTable();
        private Label[,] label = new Label[n * n, n * n];

        public Form1() {
            InitializeComponent();
            GenerateMap();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }


        private void DrawLine(Pen pen, Graphics graphics, byte x0, byte y0, byte x1, byte y1) {
            graphics.DrawLine(pen, x0, y0, x1, y1);
        }


        private void UpdateMap() {
            int sleepTime = 0;
            sudokuTable.NormalBacktrackingSolve(sleepTime);
            for (byte column = 0; column < n * n; column++)
                for (byte row = 0; row < n * n; row++) {
                    sudokuMember[column, row] = sudokuTable.PrintValue(column, row);
                    if (sudokuMember[column, row]==0)
                        label[column, row].Text = String.Empty;
                    else {
                        label[column, row].Text = sudokuMember[column, row].ToString();
                        label[column, row].ForeColor = System.Drawing.Color.White;
                    }
                }
        }

              
                

                
            
        

        private void GenerateMap() {
            for (byte column = 0; column < n * n; column++)
                for (byte row = 0; row < n * n; row++)
                    sudokuMember[column, row] = sudokuTable.PrintValue(column, row);
            CreateMap();
            Paint += DrawGrid;
        }

        private void CreateMap() {
            for (byte column = 0; column < n * n; column++)
                for (byte row = 0; row < n * n; row++) {
                    label[column, row] = new Label();
                    label[column, row].Size = new Size(labelSize, labelSize);
                    if (sudokuMember[column, row] == 0)
                        label[column, row].Text = String.Empty;
                    else
                        label[column, row].Text = sudokuMember[column, row].ToString();
                    label[column, row].Location = new Point(row * labelSize, column * labelSize);
                    label[column, row].Font = new Font("Arial", 16);
                    label[column, row].ForeColor = System.Drawing.Color.White;
                    label[column, row].TextAlign = ContentAlignment.MiddleCenter;
                    label[column, row].BackColor = System.Drawing.Color.Transparent;
                    this.Controls.Add(label[column, row]);
                }
        }

      

        private void DrawGrid(object sender, PaintEventArgs pe) {
            Graphics graphics = pe.Graphics;

            Pen myPenThin  = new Pen(Brushes.White, 1);
            Pen myPenThick = new Pen(Brushes.White, 3);
            
            int lines = n*n+1;
            byte x = 0;        // starting point
            byte y = 0;        //

            //vertical lines
            for (byte i = 0; i < lines; i++) {
                if (i % n == 0 && i != 0 && i != n * n)
                    graphics.DrawLine(myPenThick, x + labelSize * i, y, x + labelSize * i, y + labelSize * (n * n));
                else
                    graphics.DrawLine(myPenThin,  x + labelSize * i, y, x + labelSize * i, y + labelSize * (n * n));
                
            }
            //horizontal lines
            for (byte i = 0; i < lines; i++) {
                if (i % n == 0 && i != 0 && i != n * n)
                    graphics.DrawLine(myPenThick, x, y + labelSize * i, labelSize * (n * n), y + labelSize * i);
                else
                    graphics.DrawLine(myPenThin,  x, y + labelSize * i, labelSize * (n * n), y + labelSize * i);
            }
        }
        private void button_solve_Click(object sender, EventArgs e) {
            UpdateMap();
        }
    }
}