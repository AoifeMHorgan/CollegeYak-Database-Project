using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Permissions;
using System.IO;

namespace CollegeYak
{
    /// <summary>
    /// Interaction logic for UsersProfile.xaml
    /// </summary>
    public partial class UsersProfile : Window
    {
        MEMBER member;
        public UsersProfile(MEMBER member)
        {
            this.member = member;
            InitializeComponent();

            var name = this.member.USERNAME;
            var age = Convert.ToByte(member.AGE);

            var pass = this.member.PASSWORD;
            var college = this.member.COLLEGE_NAME;
            var email = this.member.EMAIL;
            var emailC = this.member.CONFIRM_EMAIL;



            txtUsername.Text = name;
            txtPassword.Text = pass;
            txtCollege.Text = college;
            txtEmail.Text = email;
            txtCEmail.Text = emailC;
            txtAge.Text = Convert.ToString(age);

        }








        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new Entities())
                {
                    context.UPDATEMEMBER(txtUsername.Text, txtEmail.Text, txtCollege.Text, txtPassword.Text, Decimal.Parse(txtAge.Text));

                    this.member.USERNAME = txtUsername.Text;
                    this.member.EMAIL = txtEmail.Text;
                    this.member.COLLEGE_NAME = txtCollege.Text;
                    this.member.PASSWORD = txtPassword.Text;
                    this.member.AGE = Byte.Parse(txtAge.Text);


                    MessageBox.Show("Success! You have Updated your Details");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Update!: " + ex.Message);
            }
        }
            
        

        public static void open(string path){
          
            string cmd = "explorer.exe";
            string arg = "/select, " + path;
           System.Diagnostics.Process.Start(cmd, arg);
           
           


        }
       

        private void btnPhoto_Click(object sender, RoutedEventArgs e)
        {
            var o = new Microsoft.Win32.OpenFileDialog();
            o.ShowDialog();
            Console.Write(o.FileName);
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
           
            bi3.UriSource = new Uri(o.FileName, UriKind.Relative);
            Console.Write(bi3);
            bi3.EndInit();
            MessageBox.Show(""+bi3);
            imgProfile.Source = bi3;


            // open("c:");

            /* BitmapImage bi3 = new BitmapImage();
             bi3.BeginInit();
             bi3.UriSource = new Uri("smiley_stackpanel.PNG", UriKind.Relative);
             bi3.EndInit();
             imgProfile.Source = bi3;*/



            /*using (var context = new Entities())
            {
                
               // member = Login.member;

                context.LOAD_PICTURE(member.USERNAME,"user-128.png");
            }*/


        }

       
      

        private void lblBack_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PostsScreen screen = new PostsScreen(member);
            this.Hide();
            screen.Show();
        }

       
    }
    }

