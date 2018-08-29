using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MineSweeper
{
    class Bound : System.Windows.Forms.Button
    {
        private int _Flag;//0未翻开 1翻开 2标记
        private int m_status;//状态 0无炸弹 1有炸弹
        private int m_Nabar;//周围的炸弹数量
        private int _x;
        private int _y;

        public int Flag
        {
            get
            {
                return _Flag;
            }
            set
            {
                _Flag = value;
            }
        }

        public int Status
        {
            get
            {
                return m_status;
            }
            set
            {
                m_status = value;
            }
        }

        public int Nabar
        {
            get
            {
                return m_Nabar;
            }
            set
            {
                m_Nabar = value;
            }
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public Bound(int x, int y)
        {
            Flag = 0;
            Status = 0;
            Nabar = 0;
            this._x = x;
            this._y = y;
        }

        public void ShowNabar()
        {
            this.Font = new Font(this.Font, this.Font.Style | FontStyle.Bold);
            switch(Nabar)
            {
                case 1: this.ForeColor = Color.DarkBlue; break;
                case 2: this.ForeColor = Color.Green; break;
                case 3: this.ForeColor = Color.Red; break;
                case 4: this.ForeColor = Color.Blue; break;
                case 5: this.ForeColor = Color.Maroon; break;
                case 6: this.ForeColor = Color.LightSeaGreen; break;
                case 7: this.ForeColor = Color.Black; break;
                case 8: this.ForeColor = Color.Gray; break;
                default: break;
            }
            this.FlatAppearance.BorderColor = Color.Gray;
            this.Text = this.Nabar.ToString();
        }

    }
}
