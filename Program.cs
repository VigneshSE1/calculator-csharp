using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
       public static float expressionEvaluate(string inpExp)
        {
            char[] expression = inpExp.ToCharArray();

            int[] valueArray = new int[100];
            char[] operatorArray = new char[100];

            int topValueIndex = -1;
            int topOperatorIndex = 0;

            int space = 0;

            bool valueStackPush(int number)
            {
                valueArray[++topValueIndex] = number;
              // Console.WriteLine(valueArray[topValueIndex]);
                return true;
            }
            int valueStackPop()
            {
                int value = valueArray[topValueIndex--];
              // Console.WriteLine(value);
                return value;
            }
           
            bool operatorStackPush(char oper)
            {
                operatorArray[++topOperatorIndex] = oper;
                //Console.WriteLine(operatorArray[topOperatorIndex]);
                //Console.WriteLine("top operator index : " +topOperatorIndex);

                return true;
            }
            char operatorStackPop()
            {
                char ope_rator = operatorArray[topOperatorIndex--];
              // Console.WriteLine(ope_rator);
                return ope_rator;

            }
            char opertorStackPeek()
            {
                //Console.WriteLine(operatorArray[topOperatorIndex]);
                return operatorArray[topOperatorIndex];
            }


            for (int i = 0; i < expression.Length; i++)
            {

                switch (expression[i] == ' ' ? "space" :
                    expression[i] == '('?"leftPar":expression[i] == ')'?"rightPar":
                    (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/')?"operator":
                    (expression[i]>='0' && expression[i]<='9')?"number":"")
                {
                    case "space":
                       // space++;
                       // Console.WriteLine(space);
                        break;

                    case "number":
                       
                        string numericalString = "";

                        while (i < expression.Length && expression[i] >= '0' && expression[i] <= '9')
                        {
                            //Console.WriteLine("insidie space");
                            numericalString += expression[i++];
                        }
                   
                        valueStackPush(int.Parse(numericalString));

                   //   Console.WriteLine(valueArray[topValueIndex]);

                        numericalString = "";

                   //   Console.WriteLine("TopValueIndex : " + topValueIndex);

                        break;

                    case "leftPar":
                        
                        operatorStackPush(expression[i]);

                     //  Console.WriteLine("TopOperatorIndex : " + topOperatorIndex);

                        break;

                    case "rightPar":
                       
                    //  Console.WriteLine("inside rigthPar");
                        while(opertorStackPeek() != '(')
                        {
                            valueStackPush(operation(operatorStackPop(), valueStackPop(), valueStackPop()));
                        }
                        operatorStackPop();
                                             
                        break;

                    case "operator":
                       
                     // Console.WriteLine("operator inside");
                        while (topOperatorIndex > 0 && checkPrecedence(expression[i], opertorStackPeek()))
                        {
                          // Console.WriteLine("inside while");
                           valueStackPush(operation(operatorStackPop(),valueStackPop(), valueStackPop()));
                        }

                       
                        operatorStackPush(expression[i]);

                        break;

                    default:
                        space++;
                        break;
                }

            }
           // Console.WriteLine(topOperatorIndex);
            while (topOperatorIndex > 0)
            {
              //  Console.WriteLine("res");
               valueStackPush(operation(operatorStackPop(), valueStackPop(), valueStackPop()));
            }
           // Console.WriteLine("-----------------");
            return valueStackPop();
        }
        public static bool checkPrecedence(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
            {
                return false;
            }
            if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static int operation(char oper, int b, int a)
        {
            switch (oper)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                    {
                        throw new System.NotSupportedException("Cannot divide by zero");
                    }
                    return a / b;
            }
            return 0;
           
        }
       public static void Main(string[] args)
        {
            
            Console.WriteLine(expressionEvaluate("( 2 * 10 ) + 10 * 100 - 20 / 100"));

        }
    }
}
