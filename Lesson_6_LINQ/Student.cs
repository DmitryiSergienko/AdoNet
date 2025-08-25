namespace Lesson_6_LINQ
{
    public class Student
    {
        public int Age { get; set; }
        public string FN { get; set; }
        public string LN { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return $"{FN,15} {LN,15} {City,15} {Age,5}";
        }
    }
}