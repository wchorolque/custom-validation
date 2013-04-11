using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Cookbook.Recipes.Core.CustomValidation;
using Cookbook.Recipes.Core.Data.Repository;

namespace CustomValidationApp
{
    public class Register : Form
    {
        Label label;
        TextBox txtUsername;
        Button btnOK;
        ComboBox cmbLocale;

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
            label = new Label ();
            label.Text = "User Name";
            label.AutoSize = true;
            label.Location = new Point (20, 20);

            txtUsername = new TextBox ();
            txtUsername.Size = new Size (100, 25);
            txtUsername.Location = new Point (120, 20);

            cmbLocale = new ComboBox ();
            cmbLocale.Location = new Point (100, 50);
            cmbLocale.Items.Add ("es-ES");
            cmbLocale.Items.Add ("es-BO");
            cmbLocale.Items.Add ("en-US");
            cmbLocale.Items.Add ("fr-FR");
            cmbLocale.SelectedIndex = 0;

            btnOK = new Button ();
            btnOK.Text = "OK";
            btnOK.Location = new Point (20, 80);

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

