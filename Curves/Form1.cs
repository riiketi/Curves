using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;


namespace Curves
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string[] ColourValues = new string[] 
        {
            "#FF0000", "#00FF00", "#0000FF", "#FFFF00", "#FF00FF", "#00FFFF", "#000000",
            "#800000", "#008000", "#000080", "#808000", "#800080", "#008080", "#808080",
            "#C00000", "#00C000", "#0000C0", "#C0C000", "#C000C0", "#00C0C0", "#C0C0C0",
            "#400000", "#004000", "#000040", "#404000", "#400040", "#004040", "#404040",
            "#200000", "#002000", "#000020", "#202000", "#200020", "#002020", "#202020",
            "#600000", "#006000", "#000060", "#606000", "#600060", "#006060", "#606060",
            "#A00000", "#00A000", "#0000A0", "#A0A000", "#A000A0", "#00A0A0", "#A0A0A0",
            "#E00000", "#00E000", "#0000E0", "#E0E000", "#E000E0", "#00E0E0", "#E0E0E0",
        };

        private void GetCoordinates(string[] lines, int rows, int cols)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            List<PointPairList> lists = new List<PointPairList>();
            List<LineItem> myCurves = new List<LineItem>();

            for (int j = 1; j < cols; ++j)    // создание Y-кривых
                lists.Add(new PointPairList());

            // заполняем список точек 
            for (int i = 0; i < rows; ++i) {
                string thisline = lines[i];
                string[] values = thisline.Split(';');
                if (values.Count() != cols)
                    throw new FileLoadException( String.Format( "Data line {0} has {1} items instead of {2}", i+1, values.Count(), cols));
                double depth = double.Parse(values[0], CultureInfo.InvariantCulture);// Глубина текущей строки
                for (int j = 1; j < cols; ++j)
                {
                    double y = double.Parse(values[j], CultureInfo.InvariantCulture);
                    lists[j-1].Add(depth, y);
                }
                
            }

            pane.YAxisList.Clear(); // Удалим существующие оси Y

            int[] axis = new int[cols-1];

            for (int j = 0; j < cols-1; ++j)    // Создадим N новых оси Y
            {
                axis[j] = pane.AddYAxis("Curve " + (j+1));
                myCurves.Add(pane.AddCurve("Curve " + (j + 1), lists[j], ColorTranslator.FromHtml(ColourValues[j]), SymbolType.None));
                myCurves[j].YAxisIndex = axis[j];
                pane.YAxisList[axis[j]].Title.FontSpec.FontColor = ColorTranslator.FromHtml(ColourValues[j]);
            }

            pane.Legend.IsVisible = false;
            pane.Title.IsVisible = false;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void Browse_btn_Click(object sender, EventArgs e)
        {
            int Rows = -1;
            int Cols = -1;
            string text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    text = File.ReadAllText(file);
                    string[] lines = text.Split('\n');
                    Rows = lines.Count();
                    if (Rows == 0) throw new FileLoadException("File is empty");
                    M_tb.Text = Convert.ToString(Rows);
                    Cols = lines[0].Split(';').Count();
                    N_tb.Text = Convert.ToString(Cols-1);
                    GetCoordinates( lines, Rows, Cols);
                }
                catch (Exception ex) {
                    MessageBox.Show( ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
