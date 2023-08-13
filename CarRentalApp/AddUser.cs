﻿using CarRentalApp;
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
    public partial class AddUser : Form
    {
        private readonly CarRentalEntities _db;
        private ManageUsers _manageUsers;
        public AddUser(ManageUsers manageUsers)
        {
            InitializeComponent();
            _db = new CarRentalEntities();
            _manageUsers = manageUsers;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            var roles = _db.Roles.ToList();
            cbxRole.DataSource = roles;
            cbxRole.ValueMember = "id";
            cbxRole.DisplayMember = "name";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var username = tbxUsername.Text;
                var RoleId = (int)cbxRole.SelectedValue;
                var password = Utils.DefaultHashedPassword();
                var user = new User
                {
                    username = username,
                    password = password,
                    isActive = true
                };
                _db.Users.Add(user);
                _db.SaveChanges();

                var userid = user.id;
                var userRole = new UserRole
                {
                    roleid = RoleId,
                    userid = userid
                };

                _db.UserRoles.Add(userRole);
                _db.SaveChanges();
                MessageBox.Show("New User Added Successfully");
                _manageUsers.PopulateGrid();
                Close();
            } 
            catch (Exception ex) 
            {
                MessageBox.Show("An Error Has Occured");
            }
        }
    }
}
