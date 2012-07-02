using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Practice.Types.Annotation;

namespace Practice.Util
{
    public class CaptchaImage : IDisposable
    {
        /// <summary>
        /// The text.
        /// </summary>
        private readonly string _text;

        /// <summary>
        /// The width.
        /// </summary>
        private int _width;

        /// <summary>
        /// The height.
        /// </summary>
        private int _height;

        /// <summary>
        /// The family name.
        /// </summary>
        private string _familyName;

        /// <summary>
        /// The image.
        /// </summary>
        private Bitmap _image;


        public CaptchaImage([NotNull] string s, int width, int height)
            : this(s, height, width, string.Empty)
        {
        }

        public CaptchaImage([NotNull] string s, int width, int height, string familyName)
        {
            _text = s;
            SetDimension(width, height);
            SetFamilyName(familyName);
            GenerateImage();
        }

        /// <summary>
        /// Gets Height.
        /// </summary>
        public int Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Gets Width.
        /// </summary>
        public int Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets Image.
        /// </summary>
        public Bitmap Image
        {
            get { return _image; }
        }

        /// <summary>
        /// Gets Text.
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// The IDisposable.Dispose method.
        /// </summary>
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            _image.Dispose();
        }

        /// <summary>
        /// The generate image.
        /// </summary>
        private void GenerateImage()
        {
            var random = new Random();
            // Create a new 32-bit bitmap image.
            var bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);
            // Create a graphics object for drawing.
            var g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, _width, _height);
            var randomLineColor = random.Next(40) + 200;
            // Fill in the background.
            var hatchBrush = new HatchBrush(
                HatchStyle.SmallConfetti,
                Color.FromArgb(randomLineColor, randomLineColor, randomLineColor),
                Color.White);
            g.FillRectangle(hatchBrush, rect);
            // Set up the text font.
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            //Adjush the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(_familyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(_text, font);
            } while (size.Width > rect.Width);
            // Set up the text format.
            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            // Create a path using the text and warp it randomly.
            var path = new GraphicsPath();
            path.AddString(_text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4F;
            PointF[] points = {
                new PointF(random.Next(rect.Width)/v, random.Next(rect.Height)/v),
                new PointF(rect.Width - random.Next(rect.Width)/v, random.Next(rect.Height)/v),
                new PointF(random.Next(_width)/v, rect.Height - random.Next(rect.Height)/v),
                new PointF(rect.Width - random.Next(rect.Width)/v, rect.Height - random.Next(rect.Height)/v)
            };
            var matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
            var randomColor = Color.FromArgb(
                random.Next(100) + 100,
                random.Next(100) + 100,
                random.Next(100) + 100);
            var randomBackground = Color.FromArgb(
                20 + random.Next(100),
                20 + random.Next(100),
                20 + random.Next(100));
            // Draw the text.
            hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, randomColor, randomBackground);
            g.FillPath(hatchBrush, path);
            // Add some random noise.
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = random.Next(rect.Width);
                int y = random.Next(rect.Height);
                int w = random.Next(m / (random.Next(1000) + 50));
                int h = random.Next(m / (random.Next(1000) + 50));
                g.FillEllipse(hatchBrush, x, y, w, h);
            }
            double noise = random.Next(35) + 35;
            int maxDim = Math.Max(rect.Width, rect.Height);
            var radius = (int)(maxDim * noise / 3000);
            var maxGran = (int)(rect.Width * rect.Height / (100 - (noise >= 90 ? 90 : noise)));
            for (int i = 0; i < maxGran; i++)
            {
                g.FillEllipse(
                    hatchBrush,
                    random.Next(rect.Width),
                    random.Next(rect.Height),
                    random.Next(radius),
                    random.Next(radius)
                );
            }
            double _lines = random.Next(25) + 15;
            if (_lines > 0)
            {
                int lines = ((int)_lines / 30) + 1;
                using (var pen = new Pen(hatchBrush, 1))
                {
                    for (int i = 0; i < lines; i++)
                    {
                        var pointsLine = new PointF[lines > 2 ? lines - 1 : 2];
                        for (int j = 0; j < pointsLine.Length; j++)
                        {
                            pointsLine[j] = new PointF(random.Next(rect.Width), random.Next(rect.Height));
                        }
                        g.DrawCurve(pen, pointsLine, 1.75F);
                    }
                }
            }
            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();
            // Set the image.
            _image = bitmap;
        }

        /// <summary>
        /// The set dimensions.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private void SetDimension(int width, int height)
        {
            // Check the width and height
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException("width", width, "Argument out range, must be greater than zero.");
            }


            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height", height, "Argument out range, must be greater than zero.");
            }
            _width = width;
            _height = height;
        }

        /// <summary>
        /// The set family name.
        /// </summary>
        /// <param name="familyName">
        /// The family name.
        /// </param>
        private void SetFamilyName([NotNull] string familyName)
        {
            // If the named font is not installed, default to a system font.
            try
            {
                _familyName = familyName;
                Font font = new Font(_familyName, 14F);
                font.Dispose();
            }
            catch
            {
                _familyName = FontFamily.GenericMonospace.Name;
            }
        }
    }
}