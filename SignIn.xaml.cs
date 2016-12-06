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

namespace CollegeYak
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public static MEMBER member;
        public SignIn()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSignInUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new Entities())
                {
                    member = new CollegeYak.MEMBER();
                    var name = txtUsername.Text;
                    member.USERNAME = name;
                    var email = txtEmail.Text;
                    member.EMAIL = email;
                    var college = txtCollege.Text;
                    member.COLLEGE_NAME = college;
                    var pass = passwordBox.Password;
                    member.PASSWORD = pass;
                    var age = Convert.ToByte(txtAge.Text);
                    member.AGE = age;
                    member.CONFIRM_EMAIL = "N";


                    context.SIGNIN(name, email, college, pass, age);
                    MessageBox.Show("Success! You are now a Member");
                    PostsScreen screen = new PostsScreen(member);
                    screen.Show();
                    this.Hide();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error Signing Up! " + ex.InnerException);
            }
        }

       

        private void lblBack_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow screen = new MainWindow();
            screen.Show();
            this.Hide();

        }
    }
}
