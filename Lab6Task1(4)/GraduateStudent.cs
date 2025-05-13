
   namespace Lab6Task1
    {
        public class GraduateStudent : Student
        {
        public string? Specialization { get; set; }
        public string? Workplace { get; set; }

        
        public GraduateStudent(string? fullName, string? major, int yearOfAdmission, double rating, string? specialization, string? workplace, string[] professions)
            : base(fullName, major, yearOfAdmission, rating, professions)
        {
            Specialization = specialization ?? "Невідомо";
            Workplace = workplace ?? "Невідомо";
        }

        public GraduateStudent() : base()
        {
            Specialization =  "Невідомо";
            Workplace =  "Невідомо";
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Спеціалізація: {Specialization}");
            Console.WriteLine($"Місце роботи після ЗВО: {Workplace}");
        }

        public virtual bool IsProfessionMatchingMajor()
        {
            if (Specialization == null || Major == null)
                return false;

            
            return Specialization.ToLower().Contains(Major.ToLower()) ||
                   Major.ToLower().Contains(Specialization.ToLower());
        }

        public override bool WantsThisProfession(string profession)
        {
            
            if (Workplace != null && Workplace.Equals(profession, StringComparison.OrdinalIgnoreCase))
                return true;

            return base.WantsThisProfession(profession);
        }

       

        public override bool Equals(object? obj)
        {
            if (obj is GraduateStudent)
            {
                return ToString().Equals(((GraduateStudent)obj).ToString());
            }
            return false;
        }

        public override int GetHashCode() { return ToString().GetHashCode(); }

        public override string ToString()
        {
            return $"{base.ToString()}, Спеціалізація: {Specialization}, Місце роботи: {Workplace}";
        }
    }
}

