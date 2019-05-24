namespace _12.Google
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Child
    {
        public Child(string childName,string childBirthDay)
        {
            this.ChildName = childName;
            this.ChildBirthday = childBirthDay;
        }
        public string ChildName { get; set; }
        public string ChildBirthday { get; set; }

        public override string ToString()
        {
            return $"{this.ChildName} {this.ChildBirthday}";
        }
    }
}
