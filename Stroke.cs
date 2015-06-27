﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jesture
{
   class Segment
   {
      public Point Start { get; set; }
      public Point End { get; set; }

      public Segment(Point start, Point end)
      {
         Start = start;
         End = end;

         var vector = new Point(Start.X - End.X, Start.Y - End.Y);

         var i = Math.Abs(vector.X);
         var j = Math.Abs(vector.Y);

         IsAxisAligned = i > j * 3 || j > i * 3;
      }

      public bool IsAxisAligned { get; private set; }
   }

   class Stroke : Drawable
   {
      public List<Segment> Segments = new List<Segment>();

      public void Add(Segment segment)
      {
         Segments.Add(segment);
      }

      public void Draw(Graphics gfx, Pen pen)
      {
         foreach (var segment in Segments)
         {
            gfx.DrawLine(pen, segment.Start, segment.End);
         }
      }

      internal bool IsBoxShaped()
      {
         int matchCount = 0;

         foreach (var segment in Segments)
         {
            if(segment.IsAxisAligned)
            {
               matchCount++;
            }
         }
         return matchCount > Segments.Count * 0.8;
      }

      public Point Location()
      {
         List<Point> points = new List<Point>();

         foreach (var segment in Segments)
         {
            points.Add(segment.Start);
            points.Add(segment.End);
         }

         var min = new Point((int)points.Min(p => p.X), (int)points.Min(p => p.Y));
         var max = new Point((int)points.Max(p => p.X), (int)points.Max(p => p.Y));

         return new Point(min.X, min.Y);
      }

      public Size Size()
      {
         List<Point> points = new List<Point>();

         foreach (var segment in Segments)
         {
            points.Add(segment.Start);
            points.Add(segment.End);
         }

         return new Size(
            (int)(points.Max(p => p.X) - points.Min(p => p.X)),
            (int)(points.Max(p => p.Y) - points.Min(p => p.Y)));
      }
   }
}
