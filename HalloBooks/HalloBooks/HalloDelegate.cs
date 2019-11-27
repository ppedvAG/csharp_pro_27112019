using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloBooks
{
    delegate void EinfachenDelegate();
    delegate void DelegateMitPara(string text);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfachenDelegate meineDele = EinfacheMethode;
            Action meinDeleAlsAction = EinfacheMethode;
            Action meinDeleAlsActionAno = delegate () { Console.WriteLine("Hallo"); };
            Action meinDeleAlsActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action meinDeleAlsActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAlsAction = MethodeMitPara;
            Action<string> deleMitParaAlsActionAno = (string txt) => { Console.WriteLine(txt); };
            Action<string> deleMitParaAlsActionAno2 = (txt) => Console.WriteLine(txt);
            Action<string> deleMitParaAlsActionAno3 = x => Console.WriteLine(x);

            CalcDelegate calcDele = Minus;
            Func<int, int, long> calcDeleAlsFunc = Sum;
            Func<int, int, long> calcDeleAlsFuncAno = (x, y) => { return x + y; };
            Func<int, int, long> calcDeleAlsFuncAno2 = (x, y) => x + y;

            List<string> texte = new List<string>();

            texte.Where(FilterNachB);
            texte.Where(x => x.StartsWith("b"));

            //Predicate<string> == Func<string,bool>
        }

        private bool FilterNachB(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Minus(int a, int b)
        {
            return a - b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string text)
        {
            Console.WriteLine(text);
        }

        private void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
