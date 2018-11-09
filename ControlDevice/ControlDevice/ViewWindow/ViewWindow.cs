using ControlDevice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewWindow
{
    public partial class ViewWindow : Form
    {

        private bool _isStarted = false;
        private bool _useMock = true;
        

        public ViewWindow()
        {
            InitializeComponent();

            radioButton1.Checked = _useMock;

        }

        private  void button1_Click(object sender, EventArgs e)
        {
            _isStarted = !_isStarted;
            btnStart.Text = (_isStarted) ? "Стоп" : "Старт";
            txtResult.Text = String.Empty;

            Foo();

            try
            {
                using (IListenerBoard board = GetListenerBoard())
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var results = board.ReadBuffer();

                        var toShow = String.Join(",", results);
                        txtResult.Text += toShow;
                        //await Task.Delay(100).ConfigureAwait(false);
                    }
                }
            }
            catch(Exception err)
            {
                txtResult.Text = err.Message;
            }

            _isStarted = !_isStarted;
            btnStart.Text = (_isStarted) ? "Стоп" : "Старт";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _useMock = radioButton1.Checked;
        }


        private  IListenerBoard GetListenerBoard()
        {            

            IListenerBoard result;

            if (_useMock)
                result = new ListenerBoardMock(0);
            else
                result = new ListenerBoard(0);

            return result;
        }

        private void Foo()
        {

            foreach (var ctl in this.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).BackColor = Color.Yellow;
                }
            }
        }
    }
}
