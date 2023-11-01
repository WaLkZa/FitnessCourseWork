using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FitnessCourseWork
{
	public partial class MainForm : Form
	{
		public List<Fitness> gyms = new List<Fitness>();
		public Dictionary<string, int> placesStat = new Dictionary<string, int>();
		public int placesCounter = 0;
		
		string fileName = "gyms.dat";
		FileStream fs;
		
		public MainForm()
		{
			InitializeComponent();
			
			fs = new FileStream(fileName, FileMode.OpenOrCreate);
			BinaryReader br = new BinaryReader(fs);
			
			while (fs.Position < fs.Length)
			{
				Fitness fitness = new Fitness();
				fitness.Id = br.ReadString();
				fitness.Type = br.ReadString();
				fitness.Places = br.ReadInt32();
				fitness.Price = br.ReadDouble();
				gyms.Add(fitness);
				
				dataGridView1.Rows.Add(fitness.Id, fitness.Type, fitness.Places, fitness.Price);
				
				placesCounter += fitness.Places;
				
				if (!placesStat.ContainsKey(fitness.Type))
				{
					placesStat.Add(fitness.Type, fitness.Places);
				}
				else
				{
					int currentPlaces = placesStat[fitness.Type];
					currentPlaces += fitness.Places;
					placesStat[fitness.Type] = currentPlaces;
				}
			}
			
			fs.Close();
			
			ImportStatistic();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			gyms.Clear();
			
			for (int row = 0; row < dataGridView1.RowCount - 1; row++)
			{
				Fitness fitness = new Fitness();
				fitness.Id = dataGridView1[0, row].Value.ToString();
				fitness.Type = dataGridView1[1, row].Value.ToString();
				fitness.Places = Convert.ToInt32(dataGridView1[2, row].Value);
				fitness.Price = Convert.ToDouble(dataGridView1[3, row].Value);
				gyms.Add(fitness);
			}
			
			fs = new FileStream(fileName, FileMode.Create);
			
			BinaryWriter bw = new BinaryWriter(fs);
			
			for (int i = 0; i < gyms.Count; i++)
			{
				bw.Write(gyms[i].Id);
				bw.Write(gyms[i].Type);
				bw.Write(gyms[i].Places);
				bw.Write(gyms[i].Price);
			}
			
			fs.Close();
			
			MessageBox.Show("Данните бяха успешно запазени!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			RefreshData();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			new Add(this).Show();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			new Search(this).Show();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			dataGridView1[0, 0].Selected = false;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			dataGridView1.Columns[4].Visible = true;
			
			int hours = Convert.ToInt32(comboBox1.SelectedItem.ToString());
				
			for (int row = 0; row < dataGridView1.RowCount - 1; row++)
			{
				int places = Convert.ToInt32(dataGridView1[2, row].Value);
				double price = Convert.ToDouble(dataGridView1[3, row].Value);
					
				//dataGridView1[4, row].Value = places * price * hours;
				dataGridView1.Rows[row].Cells[4].Value = places * price * hours;

            }
		}
		
		public void RefreshData()
		{
			dataGridView1.Rows.Clear();
			placesStat.Clear();
			listBox1.Items.Clear();
			placesCounter = 0;
			
			for (int row = 0; row < gyms.Count; row++)
			{
				placesCounter += gyms[row].Places;
				
				if (!placesStat.ContainsKey(gyms[row].Type))
				{
					placesStat.Add(gyms[row].Type, gyms[row].Places);
				}
				else
				{
					int currentPlaces = placesStat[gyms[row].Type];
					currentPlaces += gyms[row].Places;
					placesStat[gyms[row].Type] = currentPlaces;
				}
				
				if (dataGridView1.Columns[4].Visible)
				{
					int hours = Convert.ToInt32(comboBox1.SelectedItem.ToString());
					dataGridView1.Rows.Add(gyms[row].Id, gyms[row].Type, gyms[row].Places, gyms[row].Price, gyms[row].Places * gyms[row].Price * hours);
				}
				else
				{
					dataGridView1.Rows.Add(gyms[row].Id, gyms[row].Type, gyms[row].Places, gyms[row].Price);
				}
			}
			
			ImportStatistic();
		}
		
		private void ImportStatistic()
		{
			if (placesStat.Count == 0)
			{
				listBox1.Items.Add("Няма въведени зали!");
				return;
			}
			
			foreach (KeyValuePair<string, int> stat in placesStat)
			{
				listBox1.Items.Add(stat.Key + " = " + stat.Value + " места");
			}
			
			listBox1.Items.Add("----------------------------------------------------------------");
			listBox1.Items.Add("Общо места: " + placesCounter);
		}
	}
}
