using ColumnNoDuplicates.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColumnNoDuplicates
{
    public partial class Form1 : Form
    {
        private List<ColumnModel> Columns = new List<ColumnModel>();
        private string[] tableNames = new[] { "good table", "mytable", "lds_features" };
        public Form1()
        {
            InitializeComponent();
            columnModelBindingSource.DataSource = Columns;
        }

        private void NameBox_Validating(object sender, CancelEventArgs e)
        {
            if (tableNames.Contains(NameBox.Text.ToLower()))
            {
                ErrorProvider.SetError(NameBox, $"table {NameBox.Text} existiert bereits");
            }
            else ErrorProvider.SetError(NameBox, "");
        }

        private void columnModelDataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == columnModelDataGridView.Rows.Count - 1) return;

            string name = Columns[e.RowIndex].Name;
            var columnNames = Columns
                .Select(row => row.Name?.ToLower());
            var allValuesAreUnique = columnNames.Count() == columnNames.Distinct().Count();

            if (string.IsNullOrEmpty(name))
            {
                ErrorProvider.SetError(columnModelDataGridView, "Ungültige Zeichen in Spaltennamen");
            }
            else if (name.Contains("[") || name.Contains("]"))
            {
                ErrorProvider.SetError(columnModelDataGridView, "Ungültige Zeichen in Spaltennamen");
            }
            else if (!allValuesAreUnique)
            {
                ErrorProvider.SetError(columnModelDataGridView, "not unique");
            }
            else ErrorProvider.SetError(columnModelDataGridView, "");
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (ErrorProvider.GetError(control) != "")
                {
                    MessageBox.Show("Ungültige Eingabe");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
