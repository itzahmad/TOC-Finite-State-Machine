/* This is only how the States are being processed with the Transition Function in Moore Machine.
 */
using System;
using System.Threading;

namespace visusNET.FiniteStateMachine
{
    public class Test
    {
        [STAThread]
        public static void Main()
        {
            string[] sigma = new string[] { "ABC", "XYZ", "123" };

            State state1 = new State(State1, "State 1");
            State state2 = new State(State2, "State 2");
            State state3 = new State(State3, "State 3");
            State[] S = { state1, state2, state3 };

            TransitionFunction<string> delta = new TransitionFunction<string>();

            delta.AddTransition(state1, sigma[0], state2); // state1 --ABC--> state2
            delta.AddTransition(state2, sigma[1], state3); // state2 --XYZ--> state3
            delta.AddTransition(state3, sigma[2], state1); // state3 --123--> state1

            MooreMachine<string> machine = new MooreMachine<string>(sigma, S, state1, delta);
            machine.Delay = 1000;
            machine.Interval = 5000;

            machine.Analyze();

            Thread machineThread = new Thread(new ThreadStart(machine.Run));
            machineThread.Start();

            while (true)
            {
                machine.SetInput("ABC");
                Thread.Sleep(1000);
                machine.SetInput("XYZ");
                Thread.Sleep(1000);
                machine.SetInput("123");
                Thread.Sleep(1000);
            }
        }

        public static void State1()
        {
            Console.WriteLine("\n\tState 1 is running...");
        }

        public static void State2()
        {
            Console.WriteLine("\n\tState 2 is running...");
        }

        public static void State3()
        {
            Console.WriteLine("\n\tState 3 is running...");
        }
    }
}
