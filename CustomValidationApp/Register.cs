using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using Cookbook.Recipes.Core.CustomValidation;
using Cookbook.Recipes.Core.Data.Repository;

namespace CustomValidationApp
{
    public class Register : Form
    {
        System.Windows.Forms.Label label;
        System.Windows.Forms.TextBox txtUsername;
        System.Windows.Forms.Button btnOK;
        System.Windows.Forms.ComboBox cmbLocale;

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
            this.cmbLocale.Click += new EventHandler (cmbLocale_SelectedIndexChanged);
        }

        ~Register ()
        {
        }

        public void InitializeComponent ()
        {
            label = new System.Windows.Forms.Label ();
            label.Text = "User Name";
            label.AutoSize = false;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Size = new System.Drawing.Size (100, 25);
            label.Location = new System.Drawing.Point (20, 20);

            txtUsername = new System.Windows.Forms.TextBox ();
            txtUsername.Size = new  System.Drawing.Size (130, 25);
            txtUsername.Location = new System.Drawing.Point (130, 20);

            cmbLocale = new System.Windows.Forms.ComboBox ();
            cmbLocale.Location = new System.Drawing.Point (130, 50);
            cmbLocale.Size = new System.Drawing.Size (100, 30);
            cmbLocale.Items.Add ("es-ES");
            cmbLocale.Items.Add ("es-BO");
            cmbLocale.Items.Add ("en-US");
            cmbLocale.Items.Add ("fr-FR");
            cmbLocale.SelectedIndex = 0;

            btnOK = new System.Windows.Forms.Button ();
            btnOK.Text = "OK";
            btnOK.Location = new System.Drawing.Point (130, 80);

            this.Controls.Add (label);
            this.Controls.Add (txtUsername);
            this.Controls.Add (cmbLocale);
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

        void cmbLocale_SelectedIndexChanged (object sender, EventArgs e)
        {
            string locale = cmbLocale.SelectedItem.ToString ();
            Thread.CurrentThread.CurrentCulture = new CultureInfo (locale);

            string iso_locale = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            MessageBox.Show (locale + ":" + iso_locale);
        }
    }
}

