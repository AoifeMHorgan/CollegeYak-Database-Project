using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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
    /// Interaction logic for PostsScreen.xaml
    /// </summary>
    public partial class PostsScreen : Window
    {
        
       
        MEMBER member;
      

        public PostsScreen(MEMBER member)
        {
            this.member = member;
            InitializeComponent();
            screenLoads();

        }
       

        public void screenLoads()
        {

            using (var context = new Entities())
            {

                var query = from b in context.POST_VIEW
                            orderby b.POST_TIME
                            select b;

                foreach (var item in query)
                {
                    Decimal id;
                    int down = 0;
                    int up = 0;


                    id = item.POST_ID;

                    var queryUp = from c in context.VOTEUP_VIEW
                                  where c.POST_ID == id
                                  orderby c.POST_ID
                                  select c;
                    foreach (var item2 in queryUp)
                    {
                        up++;


                    }

                    var queryDown = from d in context.VOTEDOWN_VIEW
                                    where d.POST_ID == id
                                    orderby d.POST_ID
                                    select d;
                    foreach (var item3 in queryDown)
                    {
                        down++;
                    }





                    var q = item.POST_TIME + "\n" + item.Username + "\n" + item.DETAILS;
                    Label lblPost = new Label();
                    lblPost.Name = "post";
                    lblPost.Content = q;

                    Label lblUp = new Label();
                    lblUp.Name = "up";
                    lblUp.Content = "Up Votes: " + up;

                    Label lblDown = new Label();
                    lblDown.Name = "down";
                    lblDown.Content = "Down Votes: " + down;

                    Button upB = new Button();
                    upB.Name = "buttonUp";
                    upB.Content = "Vote Up";
                    upB.Background = Brushes.Green;

                    Button downB = new Button();
                    downB.Name = "buttonDown";
                    downB.Content = "Vote Down";
                    downB.Background = Brushes.Red;

                    listViewPosts.Items.Add(lblPost);
                    listViewPosts.Items.Add(lblUp);
                    listViewPosts.Items.Add(lblDown);
                    listViewPosts.Items.Add(upB);
                    listViewPosts.Items.Add(downB);
                    listViewPosts.Items.Add("\n");

                    var name = member.USERNAME;
                    var itemName = item.Username;
                    var postId = item.POST_ID;
                    upB.Click += (sender, e) => { upButton_Click(sender, e, "U", name,itemName,postId); };
                    downB.Click += (sender, e) => { downButton_Click(sender, e, "D", name, itemName, postId); };


                }


                    }


                    

            }

        public void upVote(string v, string name, string itemName, decimal postId)
        {
            using (var context = new Entities())
            {
                context.CHECKVOTE(v, name, itemName, postId);
            }
            MessageBox.Show("Success!You Up Voted This Post");
            
        }

        public void downVote(string v, string name, string itemName, decimal postId)
        {
            using (var context = new Entities())
            {
                context.CHECKVOTE(v, name, itemName, postId);
            }
            MessageBox.Show("Success!You Down Voted This Post");
        }

        private void downButton_Click(object sender, RoutedEventArgs e, string v, string name, string itemName, decimal postId)
        {
            try
            {
                downVote(v, name, itemName, postId);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            
        }

        private void upButton_Click(object sender, RoutedEventArgs e, string v, string name, string itemName, decimal postId)
        {
            try
            {
                upVote(v, name, itemName, postId);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }


        private void btnPost_Click_1(object sender, RoutedEventArgs e)
        {
            Entities context = new Entities();

            var college = this.member.COLLEGE_NAME;
            var username = this.member.USERNAME;
            var password = this.member.PASSWORD;
            var age = this.member.AGE;
            var email = this.member.EMAIL;
            var emailC = this.member.CONFIRM_EMAIL;
            context.INSERTPOST(college, username, txtPosts.Text);
            MessageBox.Show("Success " + username +"! Your new Post has been Posted");
            txtPosts.Clear();
            btnRefresh.Visibility = System.Windows.Visibility.Visible;



        }
       
        private void lblLogOut_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow screen = new MainWindow();
            this.Hide();
            screen.Show();
        }

        private void lblProfile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UsersProfile screen = new UsersProfile(member);
           
            this.Hide();
            screen.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            
            screenLoads();
          
        }
    }

            }
        

        

