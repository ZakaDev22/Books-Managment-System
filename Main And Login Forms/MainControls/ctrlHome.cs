using IslamIsLife.People_Forms;
using IslamIsLife.Teachers_Forms;
using IslamIsLife_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslamIsLife.MainControls
{
    public partial class ctrlHome : UserControl
    {
        public static int PeopleNumber =0;
        public static int UsersNumber = 0;
        public static int TeachersNumber = 0;
        public static int BooksNumber = 0;
        public static int TestsNumber = 0;

        public ctrlHome()
        {
            InitializeComponent();
        }

        private void ctrlHome_Load(object sender, EventArgs e)
        {
            _RefreshHomePage();
        }

        public void _RefreshHomePage()
        {
            PeopleNumber = clsPeople.GetAllPeopleCount();
            UsersNumber = clsUsers.GetAllUsersCount();
            TeachersNumber = clsTeachers.GetAllTeachersCount();
            BooksNumber = clsBooks.GetAllBooksCount();
            TestsNumber = clsTests.GetAllTestsCount();
   
            cpbPeople.Value= PeopleNumber;       
            cpbTests.Value = TestsNumber;
            cpbUsers.Value = UsersNumber;
            cpbTeachers.Value = TeachersNumber;
            cpbBooks.Value = BooksNumber;
        }

        private void btnSerchForPerson_Click(object sender, EventArgs e)
        {
            ShowSearchForPersonForm  frm = new ShowSearchForPersonForm();
            frm.ShowDialog();
        }

        private void SearchForTeacher_Click(object sender, EventArgs e)
        {
            ShowSearchTeacherForm frm = new ShowSearchTeacherForm();
            frm.ShowDialog();
        }
    }
}
