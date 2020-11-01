using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_lab
{
    public abstract class Task
    {
        protected byte number;

        protected string description;

        public abstract void Start();

        public byte getNumber()
        {
            return this.number;
        }

        public string getDescription()
        {
            return this.description;
        }
    }
}
