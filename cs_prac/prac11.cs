using System;

namespace prac11
{

    abstract class Telephone
    {
        protected string phonetype;
        abstract public void Ring();

        protected Telephone()
        {
            phonetype = "Analog";
        }
        public string Phonetype
        {
            set
            {
                phonetype = value;
            }
            get
            {
                return phonetype;
            }
        }
    }

    class ElectronicPhone : Telephone
    {
        public ElectronicPhone() : base()
        {
            phonetype = "Digital";
        }

        public override void Ring()
        {
            Console.WriteLine("Digital phone is ringing. fram <{0}>", phonetype);
        }
    }

    class TalkingPhone : Telephone
    {
        public TalkingPhone() : base()
        {
            phonetype = "Talking";
        }

        public override void Ring()
        {
            Console.WriteLine("Talking phone is ringing. fram <{0}>", phonetype);
        }
    }

    class Test
    {
        public static void Run()
        {
            TalkingPhone tp = new TalkingPhone();
            ElectronicPhone ep = new ElectronicPhone();
            tp.Ring();
            ep.Ring();
        }

        static void Main()
        {
            Test.Run();
        }
    }

}