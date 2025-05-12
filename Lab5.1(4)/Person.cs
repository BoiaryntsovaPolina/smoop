namespace Lab5._1
{
    class Person
    {
        public string? Name { get; set; }
        public string? Contact { get; set; }

        public Person(string? name, string? contact)
        {
            Name = name;
            Contact = contact;
        }

        public Person() : this(null, null) { }

        public override string ToString()
        {
            return $"Постачальник: {Name ?? "Невідомий"}, Контакт: {Contact ?? "Невідомий"}";
        }
    }
}
