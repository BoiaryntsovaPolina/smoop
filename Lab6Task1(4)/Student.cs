using System;

namespace Lab6Task1
{
    public class Student
    {
        public string? FullName { get; private set; }
        public string? Major { get; private set; }
        public int EnrollmentYear { get; private set; }
        public double Rating { get; private set; }
        public string[]? PossibleProfessions { get; private set; }

        public Student()
        {
            FullName = "Ім'я Прізвище";
            Major = "Невідомо";
            EnrollmentYear = 2020;
            Rating = 0.0;
            PossibleProfessions = new string[] { "Невідомо" };
        }

        public Student(string? fullName, string? major, int enrollmentYear, double rating)
        {
            FullName = fullName ?? "Ім'я Прізвище";
            Major = major ?? "Невідомо";

            if (enrollmentYear < 1900 || enrollmentYear > DateTime.Now.Year)
                enrollmentYear = DateTime.Now.Year;

            if (rating < 0 || rating > 100)
                rating = 0.0;

            EnrollmentYear = enrollmentYear;
            Rating = rating;
            PossibleProfessions = new string[] { "Невідомо" };
        }

        public Student(string? fullName, string? major, int enrollmentYear, double rating, string[] professions)
        {
            FullName = fullName ?? "Ім'я Прізвище";
            Major = major ?? "Невідомо";

            if (enrollmentYear < 1900 || enrollmentYear > DateTime.Now.Year)
                enrollmentYear = DateTime.Now.Year;

            if (rating < 0 || rating > 100)
                rating = 0.0;

            EnrollmentYear = enrollmentYear;
            Rating = rating;
            PossibleProfessions = professions ?? new string[] { "Невідомо" };
        }

        public virtual void Print()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            string professions = PossibleProfessions != null ? String.Join(", ", PossibleProfessions) : "Невідомо";
            return $"Студент: {FullName}, Спеціальність: {Major}, Рік вступу: {EnrollmentYear}, Рейтинг: {Rating}, Можливі професії: {professions}";
        }

        public virtual string ToShortString()
        {
            return $"Студент: {FullName}, Рейтинг: {Rating}";
        }

        public void ChangeMajor(string? newMajor)
        {
            Major = newMajor ?? Major;
        }

        public void ChangeRating(double newRating)
        {
            if (newRating >= 0 && newRating <= 100)
                Rating = newRating;
        }

        public virtual bool WantsThisProfession(string profession)
        {
            if (PossibleProfessions == null)
                return false;

            for (int i = 0; i < PossibleProfessions.Length; i++)
            {
                if (PossibleProfessions[i] == profession)
                    return true;
            }
            return false;
        }

        public void SetPossibleProfessions(string[] professions)
        {
            PossibleProfessions = professions;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Student other = (Student)obj;
            return FullName == other.FullName &&
                   Major == other.Major &&
                   EnrollmentYear == other.EnrollmentYear &&
                   Rating == other.Rating;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Major, EnrollmentYear, Rating);
        }
    }
}
