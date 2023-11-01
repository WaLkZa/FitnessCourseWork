
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FitnessCourseWork
{
	public partial class Search : Form
	{
		MainForm mainForm;
		
		public Search(MainForm form)
		{
			InitializeComponent();
			mainForm = form;
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			//dataGridView1.Hide();
			dataGridView1.Rows.Clear();
			bool isFound = false;
			
			for (int row = 0; row < mainForm.gyms.Count; row++)
			{
				if (mainForm.gyms[row].Type.ToLower() == comboBox1.Text.ToLower())
				{
					
					dataGridView1.Rows.Add(mainForm.gyms[row].Id, 
						mainForm.gyms[row].Type, 
						mainForm.gyms[row].Places, 
						mainForm.gyms[row].Price);
					
					isFound = true;
				}
			}
			
			if (isFound)
			{
				dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
				dataGridView1.Show();
				dataGridView1[0, 0].Selected = false;
			}
			else
			{
				dataGridView1.Hide();
				MessageBox.Show("Няма зали от тип " + comboBox1.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
