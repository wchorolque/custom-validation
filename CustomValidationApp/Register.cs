using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cookbook.Recipes.Core.CustomValidation;
using Cookbook.Recipes.Core.Data.Repository;

namespace CustomValidationApp
{
    public class Register : Form
    {
        Label label;
        TextBox txtUsername;
        Button btnOK;

        public Register ()
        {
            System.Reflection.Assembly app = System.Reflection.Assembly.GetExecutingAssembly ();
            Bitmap logo = new Bitmap (app.GetManifestResourceStream ("Logo"));
            this.Icon = Icon.FromHandle (logo.GetHicon ());

            InitializeComponent ();
            //Bitmap bmp = WindowsFormsApplication1.Properties.Resources.AppLogo;
            //this.Icon = Icon.FromHandle(bmp.GetHicon());
            this.StartPosition = FormStartPosition.CenterScreen;
            this.btnOK.Click += new EventHandler (btnOK_Click);
        }

        public void InitializeComponent ()
        {
            label = new Label ();
            label.Text = "User Name";
            label.AutoSize = true;
            label.Location = new Point (20, 20);

            txtUsername = new TextBox ();
            txtUsername.Size = new Size (100, 25);
            txtUsername.Location = new Point (120, 20);

            btnOK = new Button ();
            btnOK.Text = "OK";
            btnOK.Location = new Point (20, 50);

            this.Controls.Add (label);
            this.Controls.Add (txtUsername);
            this.Controls.Add (btnOK);
        }

        void btnOK_Click (object sender, EventArgs e)
        {
            User user = new User ()
            {
                UserName = txtUsername.Text,
                //DateOfBirth = dtpDob.Value;
            };
            ValidationContext context = new ValidationContext (user, null, null);
            IList<ValidationResult> errors = new List<ValidationResult> ();

            if (!Validator.TryValidateObject (user, context, errors, true)) {
                foreach (ValidationResult result in errors) {
                    MessageBox.Show (result.ErrorMessage);
                }
            } else {
                IRepository repository = new MockRepository ();
                repository.AddUser (user);
                MessageBox.Show ("New user added");
            }
        }
    }
}

