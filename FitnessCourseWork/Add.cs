
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FitnessCourseWork
{
	public partial class Add : Form
	{
		MainForm mainForm;
		
		public Add(MainForm form)
		{
			InitializeComponent();
			mainForm = form;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBox1.Text))
			{
				MessageBox.Show("Въведи номер на зала!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			
			if (string.IsNullOrEmpty(comboBox1.Text))
			{
				MessageBox.Show("Избери тип зала!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			
			if (string.IsNullOrEmpty(textBox2.Text))
			{
				MessageBox.Show("Въведи брой места!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			
			int places = 0;
			if (!int.TryParse(textBox2.Text, out places))
			{
				MessageBox.Show("Не е число " + textBox2.Text, "Грешни входни данни", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			
			if (string.IsNullOrEmpty(textBox3.Text))
			{
				MessageBox.Show("Въведи цена!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			
			double price = 0;
			if (!double.TryParse(textBox3.Text, out price))
			{
				MessageBox.Show("Не е число " + textBox3.Text, "Грешни входни данни", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			
			Fitness fitness = new Fitness();
			fitness.Id = textBox1.Text;
			fitness.Type = comboBox1.Text;
			fitness.Places = places;
			fitness.Price = price;
			
			mainForm.gyms.Add(fitness);
			mainForm.RefreshData();
			
			Close();
		}
	}
}
