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
        private BindingList<ColumnModel> Columns = new BindingList<ColumnModel>();
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
            else if (string.IsNullOrEmpty(NameBox.Text))
            {
                ErrorProvider.SetError(NameBox, "Tabelle muss einen Namen haben");
            }
            else ErrorProvider.SetError(NameBox, "");
        }

        private void columnModelDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex == columnModelDataGridView.Rows.Count - 1) return;

            string name = e.FormattedValue.ToString();

            if (string.IsNullOrEmpty(name))
            {
                columnModelDataGridView[0, e.RowIndex].ErrorText = "Name darf nicht leer sein";
            }
            else if (name.Contains("[") || name.Contains("]"))
            {
                columnModelDataGridView[0, e.RowIndex].ErrorText = "Ungülitges Zeichen";
            }
            else if (Columns.Select(col => col.Name.ToLower()).Contains(name.ToLower()))
            {
                columnModelDataGridView[0, e.RowIndex].ErrorText = "Doppelte Spaltennamen!";
            }
            else columnModelDataGridView[0, e.RowIndex].ErrorText = "";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (ErrorProvider.GetError(control) != "")
                {
                    MessageBox.Show("Ungültige Eingabe");
                    return;
                }
            }

            foreach (DataGridViewRow row in columnModelDataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ErrorText != "")
                    {
                        MessageBox.Show("Ungültige Eingabe");
                        return;
                    }
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
