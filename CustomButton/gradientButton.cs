using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CustomButton
{
    public class gradientButton : Button
    {
        public Color baseColor1 { get; set; } = Color.FromArgb(17, 153, 142);
        public Color baseColor2 { get; set; } = Color.FromArgb(56, 239, 125);
        public Color hoverColor1 { get; set; } = Color.FromArgb(238, 9, 121);
        public Color hoverColor2 { get; set; } = Color.FromArgb(255, 106, 0);
        public Color clickColor1 { get; set; } = Color.FromArgb(255, 0, 204);
        public Color clickColor2 { get; set; } = Color.FromArgb(51, 51, 153);
        public Color disabledColor1 { get; set; } = Color.FromArgb(50, 50, 50);
        public Color disabledColor2 { get; set; } = Color.FromArgb(100, 100, 100);

        private Color currentColor1;
        private Color currentColor2;

        public gradientButton()
        {
            currentColor1 = baseColor1;
            currentColor2 = baseColor2;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            LinearGradientBrush linGrBrush;

            if (!Enabled)
                linGrBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(Width, Height), disabledColor1, disabledColor2);
            else
                linGrBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(Width, Height), currentColor1, currentColor2);

            pevent.Graphics.FillRectangle(linGrBrush, rect);

            TextRenderer.DrawText(pevent.Graphics, Text, new Font("Impact", 20, FontStyle.Regular, GraphicsUnit.Pixel), 
                new Point(Width, Height / 2), ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            currentColor1 = hoverColor1;
            currentColor2 = hoverColor2;

            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            currentColor1 = baseColor1;
            currentColor2 = baseColor2;

            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            currentColor1 = clickColor1;
            currentColor2 = clickColor2;

            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            currentColor1 = hoverColor1;
            currentColor2 = hoverColor2;

            Refresh();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
    }
}
