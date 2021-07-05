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
        public byte[,] sudokuMember = new byte[n*n,n*n];
        SudokuTable sudokuTable = new SudokuTable();

        public Form1() {
            InitializeComponent();
            GenerateMap();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }


        public void DrawLine(Pen pen, byte x0, byte y0, byte x1, byte y1) {
            Pen whitePen = new Pen(Color.White, 1);
        }

        public void GenerateMap() {
            for (byte column = 0; column < n * n; column++)
                for (byte row = 0; row < n * n; row++)
                    //sudokuMember[column, row] = (byte)((column * n + column / n + row) % (n * n) + 1);
                    sudokuMember[column, row] = sudokuTable.PrintValue(column, row);
                    
                    //sudokuMember[column, row] = 0;

            CreateMap();
        }

        public void CreateMap() {
            Pen whitePen = new Pen(Color.White, 1f);            // THIS SHIT DOESN'T WORK!!
            Graphics horizontalLines = this.CreateGraphics();   // 
            for (byte column = 0; column < n * n; column++)
                for (byte row = 0; row < n * n; row++) {
                    Label label = new Label();
                    label.Size = new Size(labelSize, labelSize);
                    if (sudokuMember[column, row] == 0)
                        label.Text = String.Empty;
                    else
                        label.Text = sudokuMember[column, row].ToString();
                    label.Location = new Point(row * labelSize, column * labelSize);
                    label.Font = new Font("Arial", 16);
                    label.ForeColor = System.Drawing.Color.White;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    
                    this.Controls.Add(label);
                    PointF startingPoint = new PointF(column * labelSize, 0);    /// THIS SHIT DOES'T WORK!!
                    PointF endingPoint = new PointF(column * labelSize, 460);    ///
                    horizontalLines.DrawLine(whitePen, startingPoint, endingPoint);
                }

        }
    }
}
