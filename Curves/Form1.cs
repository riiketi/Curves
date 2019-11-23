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
            GraphPane pane = zedGraphControl1.GraphPane;
            {
                pane.XAxis.IsVisible = true;
                GridSet(pane.XAxis.MajorGrid, true, 5, 10);
                GridSet(pane.XAxis.MinorGrid, true, 1, 2);
            }
        }

        public static MinorGrid GridSet(MinorGrid dest, bool isVisible, int dashOn, int dashOff)
        {
            dest.IsVisible = true;
            dest.DashOn = dashOn;
            dest.DashOff = dashOff;
            return dest;
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
                pane.YAxisList[axis[j]].MajorGrid.IsZeroLine = false;
            }

            pane.Legend.IsVisible = false;
            pane.Title.IsVisible = false;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            CurveNum_tb.Enabled = true;
            DotCount_tb.Enabled = true;
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

        private void zedGraphControl1_Click(object sender, EventArgs e)
        {

        }

        double x0 = -1;
        double y0 = -1;
        int curveIndex = -1;    // Номер кривой, ближайшей к точке клика
        int pointIndex = -1;    // Номер точки кривой, ближайшей к точке клика
        double ymin_limit = -1;
        double ymax_limit = -1;
        double xmin_limit = -1;
        double xmax_limit = -1;

        double ymax_limit0 = -1;
        double ymin_limit0 = -1;

        bool mouseDown = false;


        private bool zedGraphControl1_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            // Сюда будет сохранена кривая, рядом с которой был произведен клик
            CurveItem curve;
            

            GraphPane pane = zedGraphControl1.GraphPane;

            // Максимальное расстояние от точки клика до кривой в пикселях,
            // при котором еще считается, что клик попал в окрестность кривой.
            GraphPane.Default.NearestTol = 10;
            Point eventPoint = new Point(e.X, e.Y);
            bool result = pane.FindNearestPoint(e.Location, out curve, out pointIndex);
            if (result)
            {
                pane.ReverseTransform(new PointF(e.X, e.Y), out x0, out y0);
                mouseDown = true;
                int n = Convert.ToInt32(N_tb.Text);
                for (int i = 0; i < n; ++i)
                {
                    if (curve == zedGraphControl1.GraphPane.CurveList[i])
                    {
                        curveIndex = i;
                        ymin_limit = zedGraphControl1.GraphPane.YAxisList[i].Scale.Min;
                        ymax_limit = zedGraphControl1.GraphPane.YAxisList[i].Scale.Max;

                        xmin_limit = zedGraphControl1.GraphPane.XAxis.Scale.Min;
                        xmax_limit = zedGraphControl1.GraphPane.XAxis.Scale.Max;
                        ymin_limit0 = zedGraphControl1.GraphPane.YAxisList[0].Scale.Min;
                        ymax_limit0 = zedGraphControl1.GraphPane.YAxisList[0].Scale.Max;
                        break;
                    }
                }
                
            }
            return true;
        }

        private bool zedGraphControl1_MouseMoveEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            
            return true;
        }

        private bool zedGraphControl1_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                GraphPane pane = zedGraphControl1.GraphPane;
                double x;
                double y;
                pane.ReverseTransform(new PointF(e.X, e.Y), out x, out y);
                double x1 = x - x0;
                double y1 = y - y0;
                int m = Convert.ToInt32(M_tb.Text);
                double k = (ymax_limit - ymin_limit) / (ymax_limit0 - ymin_limit0);
                zedGraphControl1.GraphPane.YAxisList[curveIndex].Scale.Min -= y1 * k;
                zedGraphControl1.GraphPane.YAxisList[curveIndex].Scale.Max -= y1 * k;

                /*
                for (int i = 0; i < m; ++i)
                {
                    zedGraphControl1.GraphPane.CurveList[curveIndex].Points[i].X += x1;
                    zedGraphControl1.GraphPane.CurveList[curveIndex].Points[i].Y += y1 * k;   // k - множитель для сопоставления осей y
                }
                
                zedGraphControl1.GraphPane.XAxis.Scale.Min = xmin_limit;
                zedGraphControl1.GraphPane.XAxis.Scale.Max = xmax_limit;
                */
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
            mouseDown = false;
            return true;
        }

        private void Smoothing_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = Convert.ToInt32(CurveNum_tb.Text) - 1;    // Индекс кривой
                int k = Convert.ToInt32(DotCount_tb.Text);      // Размер окна
                if (k % 2 == 1)
                {
                    int a = k / 2;
                    int b = Convert.ToInt32(M_tb.Text) - k / 2;
                    for (int i = a; i < b; ++i)
                    {
                        double sumX = 0;
                        double sumY = 0;
                        for (int j = i - (k / 2); j <= i + (k / 2); ++j)
                        {
                            sumX += zedGraphControl1.GraphPane.CurveList[ind].Points[j].X;
                            sumY += zedGraphControl1.GraphPane.CurveList[ind].Points[j].Y;
                        }
                        zedGraphControl1.GraphPane.CurveList[ind].Points[i].X = sumX / k;
                        zedGraphControl1.GraphPane.CurveList[ind].Points[i].Y = sumY / k;
                    }
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                }
                else
                {
                    MessageBox.Show("Число k должно быть нечётным!", "Неверное число k!", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("Убедитесь, что введённые значения верны", "Ошибка!", MessageBoxButtons.OK);
            }
        }

        private void CurveNum_tb_TextChanged(object sender, EventArgs e)
        {
            if (DotCount_tb.Text.Trim() == "")
            {
                Smoothing_btn.Enabled = false;
            }
            else
            {
                Smoothing_btn.Enabled = true;
            }
        }

        private void DotCount_tb_TextChanged(object sender, EventArgs e)
        {
            if (CurveNum_tb.Text.Trim() == "")
            {
                Smoothing_btn.Enabled = false;
            }
            else
            {
                Smoothing_btn.Enabled = true;
            }
        }
    }
}
