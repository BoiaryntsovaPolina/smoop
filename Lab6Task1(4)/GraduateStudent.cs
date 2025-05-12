
   namespace Lab6Task1
    {
        public class GraduateStudent : Student
        {
        public string? Specialization { get; set; }
        public string? Workplace { get; set; }

        public GraduateStudent(string? fullName, string? major, int yearOfAdmission, double rating, string? specialization, string? workplace)
            : base(fullName, major, yearOfAdmission, rating)
        {
            Specialization = specialization ?? "Невідомо";
            Workplace = workplace ?? "Невідомо";
        }

        public GraduateStudent(string? fullName, string? major, int yearOfAdmission, double rating, string? specialization, string? workplace, string[] professions)
            : base(fullName, major, yearOfAdmission, rating, professions)
        {
            Specialization = specialization ?? "Невідомо";
            Workplace = workplace ?? "Невідомо";
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Спеціалізація: {Specialization}");
            Console.WriteLine($"Місце роботи після ЗВО: {Workplace}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Спеціалізація: {Specialization}, Місце роботи: {Workplace}";
        }

        public virtual bool IsProfessionMatchingMajor()
        {
            if (Specialization == null || Major == null)
                return false;

            // Simple check if specialization is related to the major
            return Specialization.ToLower().Contains(Major.ToLower()) ||
                   Major.ToLower().Contains(Specialization.ToLower());
        }

        public override bool WantsThisProfession(string profession)
        {
            // For graduates, we also check if current workplace matches the profession
            if (Workplace != null && Workplace.Equals(profession, StringComparison.OrdinalIgnoreCase))
                return true;

            return base.WantsThisProfession(profession);
        }

        public override bool Equals(object? obj)
        {
            if (!base.Equals(obj))
                return false;

            GraduateStudent? other = obj as GraduateStudent;
            if (other == null)
                return false;

            return Specialization == other.Specialization && Workplace == other.Workplace;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Specialization, Workplace);
        }
    }
    }
