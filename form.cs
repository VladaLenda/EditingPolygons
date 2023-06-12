using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class PolygonEditorForm : Form
{
    private List<Point> points;
    private Button clearButton;
    private Button calculateButton;

    public PolygonEditorForm()
    {
        points = new List<Point>();

        clearButton = new Button();
        clearButton.Text = "Очистити";
        clearButton.Location = new Point(20, 50);
        clearButton.Click += ClearButton_Click;
        Controls.Add(clearButton);

        calculateButton = new Button();
        calculateButton.Text = "Обчислити";
        calculateButton.Location = new Point(100, 50);
        calculateButton.Click += CalculateButton_Click;
        Controls.Add(calculateButton);

        MouseClick += PolygonEditorForm_MouseClick;
    }

    private void PolygonEditorForm_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            Point point = e.Location;
            points.Add(point);
            Invalidate();
        }
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        points.Clear();
        Invalidate();
    }

    private void CalculateButton_Click(object sender, EventArgs e)
    {
        if (points.Count < 3)
        {
            MessageBox.Show("Для формування багатокутника потрібно додати щонайменше 3 точки.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Polygon polygon = new Polygon(points);
        double perimeter = polygon.Perimeter();
        double area = polygon.Area();

        MessageBox.Show($"Багатокутник має {points.Count} сторони.\nПериметр багатокутника: {perimeter}\nПлоща багатокутника: {area}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;

        if (points.Count >= 3)
        {
            g.DrawPolygon(Pens.Black, points.ToArray());
        }

        foreach (Point point in points)
        {
            g.FillEllipse(Brushes.Blue, point.X - 2, point.Y - 2, 4, 4);
        }
    }
}

class Polygon
{
    private List<Point> points;

    public Polygon(List<Point> points)
    {
        this.points = points;
    }

    public double Perimeter()
    {
        double perimeter = 0;

        for (int i = 0; i < points.Count - 1; i++)
        {
            perimeter += Distance(points[i], points[i + 1]);
        }

        perimeter += Distance(points[points.Count - 1], points[0]);

        return perimeter;
    }

    public double Area()
    {
        double area = 0;

        for (int i = 0; i < points.Count - 1; i++)
        {
            area += points[i].X * points[i + 1].Y - points[i + 1].X * points[i].Y;
        }

        area += points[points.Count - 1].X * points[0].Y - points[0].X * points[points.Count - 1].Y;

        area = Math.Abs(area) / 2;

        return area;
    }

    private double Distance(Point p1, Point p2)
    {
        int dx = p2.X - p1.X;
        int dy = p2.Y - p1.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }
}

class Program
{
    static void Main()
    {
        Application.Run(new PolygonEditorForm());
    }
}
class Program
{
    static void Main()
    {
        Application.Run(new PolygonEditorForm());
    }
}