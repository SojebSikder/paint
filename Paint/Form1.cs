using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        private RadioButton rdoLine;
        private RadioButton rdoRect;

        public Form1()
        {
            InitializeComponent();

            clsLineRectangle im = new clsLineRectangle();
            this.MouseDown += new MouseEventHandler(im.clsLineRectangle_OnMouseDown);
            this.MouseMove += new MouseEventHandler(im.clsLineRectangle_OnMouseMove);
            this.MouseUp += new MouseEventHandler(im.clsLineRectangle_OnMouseUp);

            rdoLine = new RadioButton();
            rdoLine.Name = "rdoLine";
            rdoLine.Location = new Point(10, 10);
            rdoLine.Text = "Line";

            rdoRect = new RadioButton();
            rdoRect.Name = "rdoRect";
            rdoRect.Location = new Point(10, 40);
            rdoRect.Text = "Rectangle";

            this.Controls.AddRange(new Control[] { rdoLine, rdoRect });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void rdo_OnClick(Object sender, EventArgs e)
        {
            if (rdoLine.Checked)
            {
                rdoRect.Checked = true;
                rdoLine.Checked = false;
            }
            else
            {
                rdoRect.Checked = false;
                rdoLine.Checked = true;
            }
        }
    }

    public class clsLineRectangle
    {
        Rectangle SelectRect = new Rectangle();
        Point ps = new Point();
        Point pe = new Point();

        public clsLineRectangle()
        {

        }

        public void clsLineRectangle_OnMouseDown(Object sender, MouseEventArgs e)
        {
            SelectRect.Width = 0;
            SelectRect.Height = 0;
            SelectRect.X = e.X;
            SelectRect.Y = e.Y;

            ps.X = e.X;
            ps.Y = e.Y;
            pe = ps;
        }

        public void clsLineRectangle_OnMouseMove(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Form thisform = (Form)sender;

                // First DrawReversible to toggle to the background color
                // Second DrawReversible to toggle to the specified color
                if (((RadioButton)thisform.Controls[0]).Checked)
                {
                    ControlPaint.DrawReversibleLine(thisform.PointToScreen(ps), thisform.PointToScreen(pe), Color.Black);
                    pe = new Point(e.X, e.Y);
                    ControlPaint.DrawReversibleLine(thisform.PointToScreen(ps), thisform.PointToScreen(pe), Color.Black);
                }
                else
                {
                    ControlPaint.DrawReversibleFrame(thisform.RectangleToScreen(SelectRect), Color.Black, FrameStyle.Dashed);
                    SelectRect.Width = e.X - SelectRect.X;
                    SelectRect.Height = e.Y - SelectRect.Y;
                    ControlPaint.DrawReversibleFrame(thisform.RectangleToScreen(SelectRect), Color.Black, FrameStyle.Dashed);
                }
            }
        }

        public void clsLineRectangle_OnMouseUp(Object sender, MouseEventArgs e)
        {
            Form thisform = (Form)sender;
            Graphics g = thisform.CreateGraphics();
            Pen p = new Pen(Color.Blue, 2);
            if (((RadioButton)thisform.Controls[0]).Checked)
            {
                ControlPaint.DrawReversibleLine(thisform.PointToScreen(ps), thisform.PointToScreen(pe), Color.Black);
                g.DrawLine(p, ps, pe);
            }
            else
            {
                ControlPaint.DrawReversibleFrame(thisform.RectangleToScreen(SelectRect), Color.Black, FrameStyle.Dashed);
                g.DrawRectangle(p, SelectRect);
            }
            g.Dispose();
        }
    }

}
