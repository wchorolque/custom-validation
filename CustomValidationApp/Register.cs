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
        System.Windows.Forms.Label lblUsername;
        System.Windows.Forms.TextBox txtUsername;
        System.Windows.Forms.Label lblDateOfBirth;
        System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        System.Windows.Forms.Label lblPassword;
        System.Windows.Forms.TextBox txtPassword;
        System.Windows.Forms.Label lblRetypePassword;
        System.Windows.Forms.TextBox txtRetypePassword;
        System.Windows.Forms.Label lblLocale;
        System.Windows.Forms.ComboBox cmbLocale;
        System.Windows.Forms.Button btnOK;
        System.Windows.Forms.Button btnCancel;

        public Register ()
        {
            // Set the APP logo
            //System.Reflection.Assembly app = System.Reflection.Assembly.GetExecutingAssembly ();
            //Bitmap logo = new Bitmap (app.GetManifestResourceStream ("CustomValidationApp.logo"));
            //this.Icon = Icon.FromHandle (logo.GetHicon ());

            InitializeComponent ();

            //Bitmap bmp = WindowsFormsApplication1.Properties.Resources.AppLogo;
            //this.Icon = Icon.FromHandle(bmp.GetHicon());
            this.StartPosition = FormStartPosition.CenterScreen;
            this.btnOK.Click += new EventHandler (btnOK_Click);
            this.btnCancel.Click += new EventHandler (btnCancel_Click);
            this.cmbLocale.SelectedIndexChanged += new EventHandler (cmbLocale_SelectedIndexChanged);
            this.Text = "Formulario de Ingreso de Datos";
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        ~Register ()
        {
        }

        public void InitializeComponent ()
        {
            this.Size = new System.Drawing.Size (300, 250);

            lblUsername = new System.Windows.Forms.Label ();
            lblUsername.Text = "User Name:";
            lblUsername.AutoSize = false;
            lblUsername.TextAlign = ContentAlignment.MiddleRight;
            lblUsername.Size = new System.Drawing.Size (100, 25);
            lblUsername.Location = new System.Drawing.Point (20, 20);

            txtUsername = new System.Windows.Forms.TextBox ();
            txtUsername.Size = new  System.Drawing.Size (130, 25);
            txtUsername.Location = new System.Drawing.Point (130, 20);

            lblDateOfBirth = new System.Windows.Forms.Label ();
            lblDateOfBirth.Text = "Date Of Birth:";
            lblDateOfBirth.AutoSize = false;
            lblDateOfBirth.TextAlign = ContentAlignment.MiddleRight;
            lblDateOfBirth.Size = new System.Drawing.Size (100, 25);
            lblDateOfBirth.Location = new System.Drawing.Point (20, 50);

            dtpDateOfBirth = new System.Windows.Forms.DateTimePicker ();
            dtpDateOfBirth.Size = new System.Drawing.Size (130, 25);
            dtpDateOfBirth.Location = new System.Drawing.Point (130, 50);

            lblPassword = new System.Windows.Forms.Label ();
            lblPassword.Text = "Password:";
            lblPassword.AutoSize = false;
            lblPassword.TextAlign = ContentAlignment.MiddleRight;
            lblPassword.Size = new System.Drawing.Size (100, 25);
            lblPassword.Location = new System.Drawing.Point (20, 80);

            txtPassword = new System.Windows.Forms.TextBox ();
            txtPassword.Size = new System.Drawing.Size (130, 25);
            txtPassword.Location = new System.Drawing.Point (130, 80);
            txtPassword.PasswordChar = '*';

            lblRetypePassword = new System.Windows.Forms.Label ();
            lblRetypePassword.Text = "Retype Password:";
            lblRetypePassword.AutoSize = false;
            lblRetypePassword.TextAlign = ContentAlignment.MiddleRight;
            lblRetypePassword.Size = new System.Drawing.Size (100, 25);
            lblRetypePassword.Location = new System.Drawing.Point (20, 110);

            txtRetypePassword = new System.Windows.Forms.TextBox ();
            txtRetypePassword.Size = new System.Drawing.Size (130, 25);
            txtRetypePassword.Location = new System.Drawing.Point (130, 110);
            txtRetypePassword.PasswordChar = '*';

            lblLocale = new System.Windows.Forms.Label ();
            lblLocale.Text = "Locale:";
            lblLocale.AutoSize = false;
            lblLocale.TextAlign = ContentAlignment.MiddleRight;
            lblLocale.Size = new System.Drawing.Size (100, 25);
            lblLocale.Location = new System.Drawing.Point (20, 140);

            cmbLocale = new System.Windows.Forms.ComboBox ();
            cmbLocale.Size = new System.Drawing.Size (100, 30);
            cmbLocale.Location = new System.Drawing.Point (130, 140);
            cmbLocale.Items.Add ("es-ES");
            cmbLocale.Items.Add ("es-BO");
            cmbLocale.Items.Add ("en-US");
            cmbLocale.Items.Add ("fr-FR");
            cmbLocale.SelectedIndex = -1;

            btnOK = new System.Windows.Forms.Button ();
            btnOK.Text = "OK";
            btnOK.Size = new System.Drawing.Size (50, 25);
            btnOK.Location = new System.Drawing.Point (70, 170);

            btnCancel = new System.Windows.Forms.Button ();
            btnCancel.Text = "Cancel";
            btnCancel.Size = new System.Drawing.Size (50, 25);
            btnCancel.Location = new System.Drawing.Point (130, 170);

            this.Controls.Add (lblUsername);
            this.Controls.Add (txtUsername);
            this.Controls.Add (lblDateOfBirth);
            this.Controls.Add (dtpDateOfBirth);
            this.Controls.Add (lblPassword);
            this.Controls.Add (txtPassword);
            this.Controls.Add (lblRetypePassword);
            this.Controls.Add (txtRetypePassword);
            this.Controls.Add (lblLocale);
            this.Controls.Add (cmbLocale);
            this.Controls.Add (btnOK);
            this.Controls.Add (btnCancel);
        }

        void btnOK_Click (object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim ().Length > 0) {
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
            } else {
                MessageBox.Show (this, 
                                 "Ingrese el nombre del usuario", 
                                 "Error", 
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error
                );
            }
        }
        void btnCancel_Click (object sernder, EventArgs e)
        {
            Application.Exit ();
        }

        void cmbLocale_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (cmbLocale.SelectedIndex != -1) {
                string locale = cmbLocale.SelectedItem.ToString ();
                Thread.CurrentThread.CurrentCulture = new CultureInfo (locale);

                string iso_locale = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                MessageBox.Show (locale + ":" + iso_locale);
            }
        }
    }
}

