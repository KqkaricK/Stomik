using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using Word = Microsoft.Office.Interop.Word;

namespace Stomik
{
    public partial class Export : Form
    {
        DataSet ds;
        DataTable dt;
        public Export()
        {
            InitializeComponent();
            ds = new DataSet();
            dt = new DataTable();
            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Database=StomikBD;User Id=postgres; Password=7532;");
            con.Open();
            string cmd = ("select yslygi.\"Money\", priem.\"date\" from yslygi INNER JOIN(poset INNER JOIN(vrach INNER JOIN priem ON vrach.\"Id\" = priem.\"kod_vrach\") ON poset.\"Id\" = priem.\"kod_pos\") ON yslygi.\"Id\" = priem.\"kod_yslugi\";");

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            con.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = dataGridView1.Rows[i].Cells[1].Value.ToString().Remove(10);
            }   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal m = 0;
            string date = dateTimePicker1.Value.ToString();
            date = date.Remove(10);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Money");
            dt1.Columns.Add("data");
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(date))
                        {
                            dt1.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value);
                            dataGridView2.DataSource = dt1;
                        }
            }
            if (dataGridView2.RowCount > 1)
            {
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Money");
                dt2.Columns.Add("data");
                for (int p = 0; dataGridView2.RowCount > p; p++)
                {
                    m += Convert.ToDecimal(dataGridView2.Rows[p].Cells[0].Value.ToString());
                }
                dt2.Rows.Add(m, dataGridView2.Rows[0].Cells[1].Value);
                dataGridView2.DataSource = dt2;
            }
            change();
        }
        void change()
        {
            if (dataGridView1.Visible == true)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
            }
            else
            {
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.doc)|*.doc";

            sfd.FileName = "export" + DateTime.Now.ToString("d-M-yyyy HH-mm-ss") + ".doc";

            if (sfd.ShowDialog() == DialogResult.OK && dataGridView1.Visible == true)
            {
                if (dataGridView1.Visible == true)
                {
                    Export_Data_To_Word(dataGridView1, sfd.FileName);
                }
                else
                {
                    Export_Data_To_Word(dataGridView2, sfd.FileName);
                }
            }
        }
        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";
                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 12;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Color = Word.WdColor.wdColorBlack;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //table style 
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header 
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Доход";
                    headerRange.Font.Size = 16;
                    headerRange.Font.Color = Word.WdColor.wdColorBlack;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }
                var pText = oDoc.Paragraphs.Add();
                pText.Format.SpaceAfter = 10f;
                pText.Range.Text = String.Format("ООО <<Народная Стоматология>>");
                pText.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                pText.Range.InsertParagraphAfter();
                pText.Range.Text = String.Format("Дата: " + DateTime.Now.ToString());
                pText.Range.InsertParagraphAfter();
                pText.Range.Text = String.Format("Зам. Директор: Бараксанов С.М.");
                pText.Range.InsertParagraphAfter();
                pText.Range.Text = String.Format("Подпись: ____________");
                pText.Range.InsertParagraphAfter();
                pText.Format.SpaceAfter = 10f;
                pText.Range.Text = String.Format("Директор: Козырев А.С.");
                pText.Range.InsertParagraphAfter();
                pText.Range.Text = String.Format("Подпись: ____________");
                pText.Range.InsertParagraphAfter();
                //save
                oDoc.SaveAs2(filename);
            }
        }

    }
}
