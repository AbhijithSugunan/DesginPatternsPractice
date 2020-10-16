using System;
using System.Collections.Generic;

namespace StratergyPattern
{
    interface IStratergy
    {
        object Action(object data);
    }


    class StratergyA<T> : IStratergy
    {
        public object Action(object data)
        {
            var list = data as List<T>;
            list.Sort();
            return list;
        }
    }

    class StratergyB<T> : IStratergy
    {
        public object Action(object data)
        {
            var list = data as List<T>;
            list.Sort();
            list.Reverse();
            return list;
        }
    }

    class Context<T> 
    {
        private IStratergy _stratergy;
        public Context(IStratergy stratergy)
        {
            _stratergy = stratergy;
        }

        public void ChangeStratergy(IStratergy stratergy)
        {
            _stratergy = stratergy;
        }

        public string Action(List<T> list)
        {
            var result = _stratergy.Action(list);
            if (result == null)
                return string.Empty;
            var parsedResponse = result as List<T>;
            return string.Join(", ", parsedResponse);
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context<int>(new StratergyA<int>());

            var numberList = new List<int> { 5, 6, 8, 4, 2, 3, 1 };
            Console.WriteLine(context.Action(numberList));

            //Changing stratergy
            context.ChangeStratergy(new StratergyB<int>());
            Console.WriteLine(context.Action(numberList));


        }
    }
}
