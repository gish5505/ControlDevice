﻿using ControlDevice.Models;
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
            

        }


        private  void button1_Click(object sender, EventArgs e)
        {
            _isStarted = !_isStarted;
            btnStart.Text = (_isStarted) ? "Стоп" : "Старт";
            txtResult.Text = String.Empty;

            Foo();

            try
            {
                using (IListenerBoard board = GetListenerBoard(0))
                {
                    //board.InitOperation();
                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            var results = 1;//board.ReadBuffer();

                            var toShow = String.Join(",", results);
                            txtResult.Text += toShow;
                            //await Task.Delay(100).ConfigureAwait(false);
                        }
                    }
                    catch (Exception err)
                    {
                        txtResult.Text = err.Message + Environment.NewLine + err.StackTrace;
                    }
                    finally
                    {
                       // board.StopOperation();
                    }

                }
            }
            catch(Exception err)
            {
                txtResult.Text = err.Message + Environment.NewLine + err.StackTrace;
            }

            _isStarted = !_isStarted;
            btnStart.Text = (_isStarted) ? "Стоп" : "Старт";

        }


        private  IListenerBoard GetListenerBoard(int boardNo)
        {            

            IListenerBoard result;

            //if (_useMock)
            //    result = new ListenerBoardMock(boardNo);
            //else
                result = new ListenerBoard(boardNo);

            return result;
        }

        private void Foo()
        {

            foreach (var ctl in this.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).BackColor = Color.White;
                }
            }
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void ViewWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void calculationViewModelBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
