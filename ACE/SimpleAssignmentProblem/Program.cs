using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAssignmentProblem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //noOfteacher,noOfSubject,noOfClasses stores number of inputs for classes, subjects and teacher at the time of input from the user.
            //subjectsInClass stores number of subjects in one class at a time.
            int noOfteachers,noOfSubjects,noOfClasses,subjectsInClass;
            //teacherName stores one professor name at a time.
            //className stores one class name at a time.
            string teacherName,className;
            //teacherDictionary stores teacherName as key and List of subjects being taught by that teacher as the value.
            Dictionary<string, List<string>> teacherDictionary = new Dictionary<string, List<string>>();
            //classDictionary stores className as key and List of subjects taught in that class as the value.
            Dictionary<string, List<string>> classDictionary = new Dictionary<string, List<string>>();
            //outputDictionary stores className as key and List of teachers teaching in that class as the value.
            Dictionary<string, List<string>> outputDictionary = new Dictionary<string, List<string>>();
            //Takes input for number of teachers.
            Console.Write("Enter number of professors ");
            noOfteachers = Convert.ToInt32(Console.ReadLine());
            //Looping for each teacher's subject values.
            for (int i = 0; i < noOfteachers; i++)
            {
                //Taking input for teacher's name as key of each teacherDictionary.
                Console.Write("Enter name of the professor ");
                teacherName = Console.ReadLine();
                //Taking input for number of subjects taught by that teacher.
                Console.Write("Enter number of subjects taught by this professor ");
                noOfSubjects = Convert.ToInt32(Console.ReadLine());
                //subjectsTaught stores List of subjects taught by this teacher in the current loop.
                List<string> subjectsTaught = new List<string>();
                //Looping for storing each subject
                for (int j = 0; j < noOfSubjects; j++)
                {
                    //Takes input from user into List of subject Values.
                    Console.Write("Enter the subject taught by the professor ");
                    subjectsTaught.Add(Console.ReadLine());
                }
                //Adds each teacher's name and List of subjects taught by that teacher into teacherDictionary.
                teacherDictionary.Add(teacherName,subjectsTaught);
            }
            //Takes input for number of classes to store classDictionary.
            Console.Write("Enter number of classes ");
            noOfClasses = Convert.ToInt32(Console.ReadLine());
            //Looping for each class's values.
            for (int i = 0; i < noOfClasses; i++)
            {
                //Takes input for class's name as key of each classDictionary.
                Console.Write("Enter Class ");
                className = Console.ReadLine();
                //Taking input for number of lectures taught in that class.
                Console.Write("Enter number of lectures taught in this class ");
                subjectsInClass = Convert.ToInt32(Console.ReadLine());
                //classSchedule stores List of lectures taught in this class in the current loop.
                List<string> classSchedule = new List<string>();
                //Looping for storing each lecture
                for (int j = 0; j < subjectsInClass; j++)
                {
                    //Takes input from user into List of lecture Values.
                    Console.Write("Enter the name of lecture taught in the class ");
                    classSchedule.Add(Console.ReadLine());
                }
                //Adds each class's name and List of lectures taught in that class into classDictionary.
                classDictionary.Add(className,classSchedule);
            }
            //count variable define to check whether teacher is already alloted to 2 classes.
            int count = 2;
            //Looping for each value in classDictionary
            foreach (var x in classDictionary)
            {
                //Checks if teacher is alloted to classes, breaks the iteration.
                if (count==0)
                    break;
                else
                {
                    //subjectsFetch stores List of subjects corresponding to one iteration's key, i.e. a specefic class's subject values in the current loop.
                    List<string> subjectsFetch = (from y in classDictionary where y.Key == x.Key select x.Value).FirstOrDefault();
                    //teachersReplacedBySubjects stores teacher Replacement for each subject which afterwards adds to outputDictionary for showing output.
                    List<string> teachersReplacedBySubjects = new List<string>();
                    //Looping for each subject in the current iterated class.
                    foreach (string z in subjectsFetch)
                    {
                        //teacherFetched stores each teacher Value corresponding to each subject fetched from teacherDictionary with the help of current loop's value.
                        string teacherFetched = (from a in teacherDictionary where a.Value.Contains(z) select a.Key)
                            .FirstOrDefault();   
                        //Checks if teacher is already present in the List(teacherFetched).
                        if(teachersReplacedBySubjects.Contains(teacherFetched))
                            //Skips recurring of teacher names.
                            continue;
                        //Checks if outputDictionary contains this teacher in the other class. So that one teacher teacher in only 2 classes.
                        else if (outputDictionary.SelectMany(b => b.Value).Contains(teacherFetched))
                        {
                            //Adds teacherFetched correspoding to subject taught in that class
                            teachersReplacedBySubjects.Add(
                                teacherFetched);
                            //Deducts counter to make sure teacher is alloted to 2 classes(maximum).
                            count--;
                        }
                        else
                        {
                            /Adds teacherFetched correspoding to subject taught in that class
                            teachersReplacedBySubjects.Add(
                                teacherFetched);
                        }
                    }
                    //Adds class name and all the List of teacher's fetched from subjects taught in current class to output dictionary.
                    outputDictionary.Add(x.Key,teachersReplacedBySubjects);
                }
            }
            //Printing of Output starts
            //Looping for each output value in outputDictionary.
            foreach (var outPut in outputDictionary)
            {
                //Prints value for one class at a time.
                Console.Write("Timetable for Class {0} is ",outPut.Key);
                //Looping for each teacher present in the List of teachers as value in the outputDictionary.
                foreach (string z in outPut.Value)
                {
                    //Checks if teacher's value is last in the queue to print next line
                    if(z!=outPut.Value.Last()) 
                        Console.Write(" " + z + ", ");
                    //Else with print comma - separated values.
                    else
                        Console.WriteLine(" " + z);
                }
            }
        }
    }
}
