using ColumnNoDuplicates.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColumnNoDuplicates
{
    public partial class Form1 : Form
    {
        private List<ColumnModel> Columns;
        private string[] tableNames = new[] { "good table", "mytable", "lds_features" };
        public Form1()
        {
            InitializeComponent();
        }

        private void NameBox_Validating(object sender, CancelEventArgs e)
        {
            if (tableNames.Contains(NameBox.Text.ToLower()))
            {
                ErrorProvider.SetError(NameBox, $"table {NameBox.Text} existiert bereits");
            }
        }
    }
}
