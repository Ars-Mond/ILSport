using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ILSport.CustomElements;

public class CustomEllipse : Shape
{
    public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(nameof(Center), typeof(Size), typeof(CustomEllipse));
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CustomEllipse));
    public static readonly DependencyProperty CountSegmentsProperty = DependencyProperty.Register(nameof(CountSegments), typeof(int), typeof(CustomEllipse));
    public static readonly DependencyProperty CountVisibleSegmentsProperty = DependencyProperty.Register(nameof(CountVisibleSegments), typeof(int), typeof(CustomEllipse));
    public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(nameof(Angle), typeof(double), typeof(CustomEllipse));

    public Size Center
    {
        get => (Size)GetValue(CenterProperty);
        set => SetValue(CenterProperty, value);
    }

    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }
    
    public int CountSegments
    {
        get => (int)GetValue(CountSegmentsProperty);
        set => SetValue(CountSegmentsProperty, value);
    }
    
    public int CountVisibleSegments
    {
        get => (int)GetValue(CountVisibleSegmentsProperty);
        set => SetValue(CountVisibleSegmentsProperty, value);
    }
    
    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    protected override Geometry DefiningGeometry
    {
        get
        {
            Vector2 center = new Vector2(Center.Width, Center.Height);
            double radius = Radius;
            double angle = Angle;
            int edges = CountSegments;
            int visibleEdges = CountVisibleSegments < CountSegments ? CountVisibleSegments : CountSegments;

            var vertexes = Shapes.Polygon(edges, radius, center, angle);

            List<PathSegment> segments = new List<PathSegment>(edges);

            for (var i = 0; i < visibleEdges; i++)
            {
                var vertex = vertexes.NextElement(i);
                segments.Add(new LineSegment(vertex.ToPoint(), true));
            }

            PathFigure figure = new PathFigure(vertexes[0].ToPoint(), segments, CountVisibleSegments >= CountSegments);
            
            return new PathGeometry(new[] { figure }, FillRule.EvenOdd, null);
        }
    }

    public CustomEllipse()
    { }
    
    
    
}

public static class Shapes
{
    public static Vector2[] Polygon(int edges, double radius, Vector2 center, double angleRotation)
    {
        var vertexes = new Vector2[edges];
        for (var i = 0; i < edges; i++)
        {
            var angle = 2f * Math.PI / edges * i + angleRotation * Math.PI / 180;
            vertexes[i] = new Vector2(
                Math.Cos(angle) * radius + center.x,
                Math.Sin(angle) * radius + center.y
            );
        }

        return vertexes;
    }
    
    public static T NextElement<T>(this T[] array, int index)
    {
        return array[++index % array.Length];
    }
}

public class Vector2
{
    public double x;
    public double y;

    public Vector2()
    {
        x = 0;
        y = 0;
    }

    public Vector2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}

public static class Vector2Extension
{
    public static Point ToPoint(this Vector2 vector2)
    {
        return new Point(vector2.x, vector2.y);
    }
}