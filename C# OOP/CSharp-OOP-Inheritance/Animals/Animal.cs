namespace Animals
{
    using System;
    using System.Text;

    public abstract class Animal
    {
        private string name;
        private string gender;
        private int age;
        public Animal(string name,int age,string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            private set
            {
                if (value < 1)
                {
                    throw new Exception("Invalid input!");
                }
                age = value;
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            private set
            {
                gender = value;
            }
        }

        public virtual string ProduceSound()
        {
            return null;
        }
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"{this.Name} {this.Age} {this.Gender}");
            result.AppendLine(this.ProduceSound());

            return result.ToString().TrimEnd();
        }
    }
}
