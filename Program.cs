using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp47
{
    class unisystem
    {
        public static List<Student> students = new List<Student>();
        public static List<Teacher> teachers = new List<Teacher>();
        public static List<Course> courses = new List<Course>();
        public static List<erae> eraes = new List<erae>();
        public static List<Akhz> akhzs = new List<Akhz>();
        public static void addcourse(Course course)
        {
            var ac = from item in courses
                     where item.courseid == course.courseid
                     select item;
            if (ac.Count() == 0)
            {
                courses.Add(course);
            }
            else
                throw new Exception("alredy have this course");
        }
        public static void removecourse(Course course)
        {
            courses.Remove(course);
        }
        public static void addstudent(Student student)
        {
            var ast = from item in students
                      where item.studentid == student.studentid
                      select item;
            if (ast.Count() != 0)
            {
                throw new Exception("this student exist");
            }
            else
                students.Add(student);

        }
        public static void removestudent(Student student)
        {
            students.Remove(student);
        }
        public static void addteacher(Teacher teacher)
        {
            var at = from item in teachers
                     where item.teacherid == teacher.teacherid
                     select item;
            if (at.Count() == 0)
            {
                teachers.Add(teacher);
            }
            else
                throw new Exception("already have this teacher");
        }
        public static void removeteacher(Teacher teacher)
        {
            teachers.Remove(teacher);
        }
        public static void getcoursebystudent(Student student, Course course)
        {
            var get = from item in student.studentcourses
                      where item.courseid == course.courseid
                      select item;
            if (get.Count() == 0)
            {
                student.studentcourses.Add(course);
                course.studentcourse.Add(student);
            }
            else
                throw new Exception("this student has this course");
        }
        public static void PresentaCoursebyTeacher(Teacher teacher, Course course)
        {
            var present = from item in teacher.teachercourse
                          where item.courseid == course.courseid
                          select item;
            if (present.Count() == 0)
            {
                teacher.teachercourse.Add(course);
                course.teachercourse.Add(teacher);
            }
            else
                throw new Exception();

        }
        public static void assigngrade(Course course, Student student, double score)
        {

            foreach (var item in akhzs)
            {
                if (item.akhzstudent == student && item.akhzcourse == course)
                {
                    item.score = score;
                    break;
                }
            }
        }
        public static double averagegrade(Student student)
        {
            var ave = from item in akhzs
                      where item.akhzstudent.id == student.id
                      select item;

            if (ave.Count() == 0)
            {
                throw new Exception("student doesnt exist");
            }
            else
            {
                double sum1 = 0;
                foreach (var item in ave)
                {
                    sum1 += item.akhzcourse.vahedcount;
                }
                double sum2 = 0;
                foreach (var item in ave)
                {
                    sum2 += item.score * item.akhzcourse.vahedcount;

                }
                return sum2 / sum1;

            }
        }
        public static List<Student> studentcourse(Course course)
        {
            var list = from item in akhzs
                       where item.akhzcourse.courseid == course.courseid
                       select item.akhzstudent;

            if (list.Count() == 0)

            {
                return null;
            }
            else
                return list.ToList();
        }
        public static List<Course> teachercourse(Teacher teacher)
        {
            var listt = from item in eraes
                        where item.eraeteacher.id == teacher.id
                        select item.eraecourse;

            if (listt.Count() == 0)
            {
                return null;
            }
            else
                return listt.ToList();
        }
        public static double averagegradecourse(Course course)
        {
            var d1 = from item in akhzs
                     where item.akhzcourse.courseid == course.courseid
                     select item.score;
            return (d1.Average());
        }
        public static List<Course> studentcourselist(Student student)
        {
            var list = from item in akhzs
                       where item.akhzstudent.id == student.id
                       select item.akhzcourse;

            if (list.Count() == 0)
            {
                return null;
            }
            else
                return list.ToList();
        }
        public static int totalunit(Student student)
        {
            var tu = from item in akhzs
                     where item.akhzstudent.id == student.id
                     select item.akhzcourse.vahedcount;
            var td = from item in akhzs
                     where item.akhzstudent.id == student.id
                     select item.akhzcourse;
            if (tu.Count() == 0)
            {
                throw new Exception("this student doesnt exist ");
            }
            else
                return tu.Sum() * td.Count();
        }
        public static Student searchStudent(int id)
        {
            var stu = from item in students
                      where item.studentid == id
                      select item;
            return stu.First();

        }
        public static Course searchCours(int id)
        {
            var crs =
                from item in courses
                where item.courseid == id
                select item;
            return crs.First();
        }
        public static Teacher searchTeacher(int id)
        {
            var tch =
                from item in teachers
                where item.teacherid == id
                select item;
            return tch.First();
        }

    }
    class Person
    {
        public string firstname;
        public string lastname;
        public string jensiat;
        public int birthdate;
        public int Birthdate
        {
            get { return birthdate; }
            set
            {
                if (value > 0)
                {
                    value = birthdate;
                }
                else
                    throw new Exception("birthdate must be positive");
            }
        }
        public int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value > 0)
                {
                    value = id;
                }
                else
                    throw new Exception("id must be positive");
            }
        }
        public Person(string firstname, string lastname, string jensiat, int birthdate, int id)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.jensiat = jensiat;
            this.birthdate = birthdate;
            this.id = id;
        }
    }
    class Student : Person
    {
        public int studentid;
        public int Studentid
        {
            get { return studentid; }
            set
            {
                if (value > 0)
                {
                    value = studentid;
                }
                else
                    throw new Exception("student id must be > 0");
            }
        }
        public List<Course> studentcourses = new List<Course>();
        public Student(int studentid, string firstname, string lastname, string jensiat, int birthdate, int id) : base(firstname, lastname, jensiat, birthdate, id)
        {
            this.studentid = studentid;

        }
    }
    class Teacher : Person
    {
        public List<Course> teachercourse = new List<Course>();
        public int teacherid;
        public int Teacherid
        {
            get { return teacherid; }
            set
            {
                if (value > 0)
                {
                    value = teacherid;
                }
                else
                    throw new Exception("teacher id must be > 0");
            }
        }
        public Teacher(int teacherid, string firstname, string lastname, string jensiat, int birthdate, int id) : base(firstname, lastname, jensiat, birthdate, id)
        {
            this.teacherid = teacherid;
        }
    }
    class Course
    {
        public List<Student> studentcourse = new List<Student>();
        public List<Teacher> teachercourse = new List<Teacher>();
        public string coursename;
        public int vahedcount;
        public int courseid;
        public int Courseid
        {
            get { return courseid; }
            set
            {
                if (value > 0)
                {
                    value = courseid;
                }
                else
                    throw new Exception("id must be > 0");
            }

        }
        public Course(string coursename, int vahedcount, int courseid)
        {
            this.coursename = coursename;
            this.vahedcount = vahedcount;
            this.courseid = courseid;

        }
    }
    class Akhz
    {
        public Student akhzstudent;
        public Course akhzcourse;
        public double score = -1;
        public double Score
        {
            get { return score; }
            set
            {
                if (value > 0 && value < 20)
                    value = score;
                else
                    throw new Exception("score must be between 0 and 20");
            }
        }
        public int akhzdate;
        public Akhz(Student akhzstudent, Course akhzcourse, int akhzdate)
        {
            this.akhzstudent = akhzstudent;
            this.akhzcourse = akhzcourse;
            this.akhzdate = akhzdate;

        }
    }
    class erae
    {
        public Teacher eraeteacher;
        public Course eraecourse;
        public int year;
        public int Year
        {
            get { return year; }

            set
            {
                if (value > 0)
                {
                    value = year;
                }
                else
                    throw new Exception("year must be positive");
            }

        }
        public erae(Teacher eraeteacher, Course eraecourse, int year)
        {
            this.eraeteacher = eraeteacher;
            this.eraecourse = eraecourse;
            this.year = year;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {


            int a = 0;
            while (a != 16)
            {

                Console.WriteLine("Please enter a number to execute the corresponding command: ");
                Console.WriteLine("1: Add a Course \t 2: Remove a Course");
                Console.WriteLine("3: Add a Student\t 4: Remove a Student");
                Console.WriteLine("5: Add a Teacher\t 6: Remove a Teacher");
                Console.WriteLine("7: Get a Course by a Student");
                Console.WriteLine("8: Present a Course by a Teacher");
                Console.WriteLine("9: Assign a grade for a Student");
                Console.WriteLine("10: Calculate and display the average grades of a student");
                Console.WriteLine("11: Get student list of a course");
                Console.WriteLine("12: Get course list of a teacher");
                Console.WriteLine("13: Get average grades of the course");
                Console.WriteLine("14: Get course list of a student");
                Console.WriteLine("15: Get the total units of a specific student that taken");
                Console.WriteLine("16: Quit");
                // try catch for exceptions
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                    if (a > 16 && a < 1)
                    {
                        //throw new Exception("must be between 1 and 16");
                        throw new Exception("must be between 1 and 16");
                    }
                    else
                    {
                        switch (a)
                        {
                            case 1:
                                // add new course
                                Console.Clear();
                                Console.Write("Please enter course name: ");
                                string courseName = Console.ReadLine();
                                Console.Write("enter the numbe of units: ");
                                int units = Convert.ToInt32(Console.ReadLine());
                                Console.Write("enter course id: ");
                                int ci = Convert.ToInt32(Console.ReadLine());
                                unisystem.addcourse(new Course(courseName, units, ci));
                                break;
                            case 2:
                                //remove a course
                                Console.Clear();
                                Console.Write("Please enter course id you want to remove: ");
                                int CourseId = Convert.ToInt32(Console.ReadLine());
                                unisystem.removecourse(unisystem.searchCours(CourseId));
                                break;
                            case 3:
                                //add new student
                                Console.Clear();
                                Console.Write("Please enter student first name: ");
                                string name = Console.ReadLine();
                                Console.Write("Please enter student last name: ");
                                string lastName = Console.ReadLine();
                                Console.Write("Please enter student id: ");
                                int stuId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter id: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter student birth date: ");
                                int birth = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter student gender: ");
                                string jensiat = Console.ReadLine();
                                unisystem.addstudent(new Student(stuId, name, lastName, jensiat, birth, id));
                                break;
                            case 4:
                                //remove a student
                                Console.Clear();
                                Console.Write("Please enter student id you want to remove: ");
                                int remove = Convert.ToInt32(Console.ReadLine());
                                unisystem.removestudent(unisystem.searchStudent(remove));
                                break;
                            case 5:
                                //add new teacher
                                Console.Clear();
                                Console.Write("Please enter teacher first name: ");
                                string tname = Console.ReadLine();
                                Console.Write("Please enter teacher last name: ");
                                string tlastName = Console.ReadLine();
                                Console.Write("Please enter teacher id: ");
                                int teacherId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter id: ");
                                int tid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter teacher birth date: ");
                                int tbirth = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter teacher gender: ");
                                string tjensiat = Console.ReadLine();
                                unisystem.addteacher(new Teacher(teacherId, tname, tlastName, tjensiat, tbirth, tid));
                                break;
                            case 6:
                                //remove a teacher
                                Console.Clear();
                                Console.Write("Please enter teacher id you want to remove: ");
                                int removeT = Convert.ToInt32(Console.ReadLine());
                                unisystem.removeteacher(unisystem.searchTeacher(removeT));
                                break;
                            case 7:
                                //get a course by student
                                Console.Clear();
                                Console.Write("Please enter student id: ");
                                int idd = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter course id: ");
                                int cId = Convert.ToInt32(Console.ReadLine());
                                unisystem.getcoursebystudent(unisystem.searchStudent(idd), unisystem.searchCours(cId));
                                Console.WriteLine("enter akhz date : ");
                                int ad = Convert.ToInt32(Console.ReadLine());
                                Akhz a1 = new Akhz(unisystem.searchStudent(idd), unisystem.searchCours(cId), ad);
                                unisystem.akhzs.Add(a1);

                                break;
                            case 8:
                                //Present a Course by Teacher
                                Console.Clear();
                                Console.Write("Please enter teacher id: ");
                                int Tidd = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter course id: ");
                                int TcId = Convert.ToInt32(Console.ReadLine());
                                unisystem.PresentaCoursebyTeacher(unisystem.searchTeacher(Tidd), unisystem.searchCours(TcId));
                                Console.WriteLine("enter year :");
                                int yearr = Convert.ToInt32(Console.ReadLine());
                                erae e1 = new erae(unisystem.searchTeacher(Tidd), unisystem.searchCours(TcId), yearr);
                                unisystem.eraes.Add(e1);
                                break;

                            case 9:
                                //assign a grade to a student
                                Console.Clear();
                                Console.Write("enter student id: ");
                                int sid = Convert.ToInt32(Console.ReadLine());
                                Console.Write("enter grade: ");
                                double g = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Please enter course id: ");
                                int cIdd = Convert.ToInt32(Console.ReadLine());
                                unisystem.assigngrade(unisystem.searchCours(cIdd), unisystem.searchStudent(sid), g);
                                break;
                            case 10:
                                //show average score of a student
                                Console.Clear();
                                Console.Write("Please enter student id you want to see average score: ");
                                int sAve = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(unisystem.averagegrade(unisystem.searchStudent(sAve)));
                                break;
                            case 11:
                                //show student list of a course
                                Console.Clear();
                                Console.Write("Please enter course id you want to see students: ");
                                int iddd = Convert.ToInt32(Console.ReadLine());
                                List<Student> c1 = new List<Student>();
                                c1 = unisystem.studentcourse(unisystem.searchCours(iddd));
                                /*foreach (var item in c1)
								{
									Console.WriteLine(item);
								}*/
                                break;
                            case 12:
                                //get course list of a teacher
                                Console.Clear();
                                Console.Write("Please enter teacher id to see courses: ");
                                int idddd = Convert.ToInt32(Console.ReadLine());

                                List<Course> cl1 = new List<Course>();
                                cl1 = unisystem.teachercourse(unisystem.searchTeacher(idddd));
                                /*foreach (var item in cl1)
								{
									Console.WriteLine(item);
								}*/
                                break;
                            case 13:
                                //get average grade of a course
                                Console.Clear();
                                Console.Write("Please enter course id you want to see average: ");
                                int aveId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(unisystem.averagegradecourse(unisystem.searchCours(aveId)));
                                break;
                            case 14:
                                //get course list of student
                                Console.Clear();
                                Console.Write("Please enter student id you want to see list of courses: ");
                                int stuCourses = Convert.ToInt32(Console.ReadLine());
                                List<Course> cs1 = new List<Course>();
                                cs1 = unisystem.studentcourselist(unisystem.searchStudent(stuCourses));
                                /*foreach (var item in cs1)
								{
									Console.WriteLine(item);
								}*/

                                break;
                            case 15:
                                //get total units of a student
                                Console.Clear();
                                Console.Write("Please enter student id you want to see list of courses: ");
                                int stuNumberOfCorses = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(unisystem.totalunit(unisystem.searchStudent(stuNumberOfCorses)));
                                break;
                            case 16:
                                break;
                        }


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();

                Console.Clear();

            }



        }
    }
}
