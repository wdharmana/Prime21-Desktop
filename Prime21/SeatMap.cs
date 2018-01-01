using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prime21
{
    public partial class SeatMap : Form
    {
        public SeatMap()
        {
            InitializeComponent();
        }

        private void SeatMap_Load(object sender, EventArgs e)
        {
            draw();
        }

        private void draw()
        {
            var alpha = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            int width = 40; int top = 0; int len = 12, row = 10, screenWidth = 0;
            List<Button> buttons = new List<Button>();

            for (int r = 0; r < row; r++)
            {

               screenWidth = 0;

                string alp = alpha[r];

                top += width + 5;
                int space = panel1.Width + 20;
               
                for (int i = 0; i < len; i++)
                {
                    Button newButton = new Button();
                    newButton.Width = width;

                    if (i > 0)
                    {
                        space += (width + 5);
                    }
                    else
                    {
                        space += width / 2;
                    }

                    if (i == len / 2)
                    {

                        space += (width);

                    }

                    screenWidth += width + 10;
                  
                    newButton.Left = space;
                    newButton.Top = top;
                    newButton.Text = alp + (i + 1);
                    newButton.Height = width;

                    newButton.Click += (source, e) =>
                    {
                        if (newButton.BackColor == Color.Red)
                        {
                            MessageBox.Show("Already booked!");
                        }
                        else
                        {
                            newButton.BackColor = Color.Yellow;
                        }
                    };

                    if (i % 2 == 0)
                    {
                        newButton.BackColor = Color.Red;
                    }

                    buttons.Add(newButton);
                    this.Controls.Add(newButton);
                }

            }

            panelScreen.Top = top + 60;
            panelScreen.Width = screenWidth;
            panelScreen.Height = 10;
            panelScreen.Left = panel1.Width + 20;

        }
    }
}
