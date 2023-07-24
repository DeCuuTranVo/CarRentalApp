using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    
    public partial class Form1 : Form
    {
        // Represent database
        private readonly CarRentalEntities carRentalEntities;
        public Form1()
        {
            InitializeComponent();
            carRentalEntities = new CarRentalEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show is like Console.WriteLine
            // MessageBox.Show($"Thank you for renting: {tbCustomerName.Text}");
            try
            {
                string customerName = tbCustomerName.Text;
                var dateOut = dtRented.Value;
                var dateIn = dtReturned.Value;
                double cost = Convert.ToDouble(tbCost.Text);

                var carType = cbTypeOfCar.SelectedItem.ToString();
                // var carType = cbTypeOfCar.Text;
                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    isValid = false;
                    errorMessage += "Error: Please enter missing data.\n\r";
                    // MessageBox.Show("Please enter missing data.");
                }

                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal Date Selection\n\r";
                    // MessageBox.Show("Illegal Date Selection");
                }

                if (isValid == true)
                {
                    // object represent table
                    var rentalRecord = new CarRentalRecord();
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal) cost;
                    rentalRecord.TypeOfCarId = (int) cbTypeOfCar.SelectedValue;

                    MessageBox.Show($"Customer Name: {customerName}\n\r" +
                        $"Date Rented: {dateOut}\n\r" +
                        $"Date Returned: {dateIn}\n\r" +
                        $"Cost: {cost}\n\r" +
                        $"Car Type: {carType}\n\r" +
                        $"THANK YOU FOR YOUR BUSINESS");
                } else
                {
                    MessageBox.Show(errorMessage);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Select * from TypesOfCars
            var cars = carRentalEntities.TypesOfCars.ToList();
            cbTypeOfCar.DisplayMember = "Name";
            cbTypeOfCar.ValueMember = "id";
            cbTypeOfCar.DataSource = cars;
        }
    }
}
