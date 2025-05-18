namespace Lab7.Classes
{
    internal class Person : IComparable<Person>, ICloneable   
    {
        private string firstName;
        private string lastName;
        private DateTime birthDate;

        // Властивість для імені особи  
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Ім'я не може бути порожнім");
                firstName = value;
            }
        }


        // Властивість для прізвища особи
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Прізвище не може бути порожнім");
                lastName = value;
            }
        }


        /// Властивість для дати народження особи
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дата народження не може бути в майбутньому");
                birthDate = value;
            }
        }


        // Конструктор без параметрів
        public Person()
        {
            firstName = "Невідомо";
            lastName = "Невідомо";
            birthDate = new DateTime(1990, 1, 1);
        }


        // Конструктор з параметрами
        public Person(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }


        // Перевизначений метод ToString для форматування інформації про особу         
        public override string ToString()
        {
            return $"Особа: {FirstName} {LastName}, Дата народження: {BirthDate.ToShortDateString()}";
        }


        // Метод порівняння для інтерфейсу IComparable
        public int CompareTo(Person? other)
        {
            if (other == null) return 1;

            // Спочатку порівнюємо прізвища
            int lastNameComparison = LastName.CompareTo(other.LastName);
            if (lastNameComparison != 0)
            {
                return lastNameComparison;
            }

            // Потім порівнюємо імена
            int firstNameComparison = FirstName.CompareTo(other.FirstName);
            if (firstNameComparison != 0)
            {
                return firstNameComparison;
            }

            // Нарешті порівнюємо дати народження
            return BirthDate.CompareTo(other.BirthDate);
        }


        // Метод клонування для інтерфейсу ICloneable         
        public object Clone()
        {
            // Створюємо нову копію об'єкта Person з тими ж значеннями
            return new Person(FirstName, LastName, BirthDate);
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if (obj is Person other)
                return CompareTo(other);

            throw new ArgumentException("Об'єкт не є типом Person");
        }
        public override bool Equals(object? obj)                                               // Додала Equals, GetHashCode
        {
            if (obj is Person)
            {
                return ToString().Equals(((Person)obj).ToString());
            }
            return false;
        }

        public override int GetHashCode() { return ToString().GetHashCode(); }

    }
}

