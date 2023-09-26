using Microsoft.Maui.Controls.Shapes;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace DotNetSevenPath
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            AddPath();
        }

        void AddPath()
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;
            grid.VerticalOptions = LayoutOptions.Center;
            grid.HorizontalOptions = LayoutOptions.Center;
            Path squareFillandStrokePath = GetPath("M 10,100 L 100,100 100,50Z");
            squareFillandStrokePath.Fill = new SolidColorBrush(Colors.Yellow);
            squareFillandStrokePath.Stroke = new SolidColorBrush(Colors.Black);
            CalculateSize(squareFillandStrokePath.Data, ref minX, ref minY, ref maxX, ref maxY);
            float width = maxX;
            float height = maxY;
            grid.WidthRequest = width;
            grid.HeightRequest = height;;
            grid.Children.Add(squareFillandStrokePath);
        }

        Path GetPath(string pathData)
        {
            Path path = new Path();
            var data = new PathGeometryConverter().ConvertFromString(pathData);
            path.Data = data as Geometry;
            return path;
        }

        private void CalculateSize(Geometry geometry, ref float minX, ref float minY, ref float maxX, ref float maxY)
        {
            if (geometry is PathGeometry pathGeometry)
            {
                foreach (var figure in pathGeometry.Figures)
                {
                    foreach (var segment in figure.Segments)
                    {
                        if (segment is LineSegment lineSegment)
                        {
                            minX = Math.Min(minX, (float)lineSegment.Point.X);
                            minY = Math.Min(minY, (float)lineSegment.Point.Y);
                            maxX = Math.Max(maxX, (float)lineSegment.Point.X);
                            maxY = Math.Max(maxY, (float)lineSegment.Point.Y);
                        }
                    }
                }
            }
        }

    }
}