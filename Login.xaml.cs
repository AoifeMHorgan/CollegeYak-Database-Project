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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MEMBER member;
        public Login()
        {
            InitializeComponent();
           
        }

        
        

        private void btnLogInUser_Click(object sender, RoutedEventArgs e)
        {
            try { 
            using (Entities context = new Entities())
            {
               


                String college;
                String email;
                String confirmEmail;
               
                var name = txtUser.Text;
                var password = txtPassword.Password;
                var query = from b in context.MEMBERs
                            where b.USERNAME == name
                            select b;

                    foreach (var item in query)
                    {

                        member = new MEMBER();



                        var login = context.LOGGINGIN(name, password);


                        member.USERNAME = name;
                        member.PASSWORD = password;
                        college = item.COLLEGE_NAME;
                        email = item.EMAIL;
                        confirmEmail = item.CONFIRM_EMAIL;
                        member.CONFIRM_EMAIL = confirmEmail;
                        member.COLLEGE_NAME = college;
                        member.EMAIL = email;
                        member.AGE = item.AGE;
                        context.MEMBERs.Add(member);


                        MessageBox.Show("Success! You are now Logged In");
                        PostsScreen screen = new PostsScreen(member);
                        screen.Show();
                        this.Hide();
                    }
                }  
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error! Not a valid Login!: " + ex.Message);
            }
        }

      

        private void lblBack_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow screen = new MainWindow();
            this.Hide();
            screen.Show();
        }
    }
}
