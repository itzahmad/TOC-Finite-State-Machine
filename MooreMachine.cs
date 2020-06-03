/* Moore machine is a finite state machine in which the next state is decided by the current state and current input symbol.
 * The output symbol at a given time depends only on the present state of the machine. 
 * 
 *                                          ***Copyright@Wani Mudasir***
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace visusNET.FiniteStateMachine
{
    public class MooreMachine<T>
    {
        private T[] sigma;
        private State[] S;
        private State s0;
        private TransitionFunction<T> delta;
        private T input;
        private int delay;
        private int interval;

        public int Delay
        {
            get {
                return this.delay;
            }
            set {
                this.delay = value;
            }
        }

        public int Interval
        {
            get {
                return this.interval;
            }
            set {
                this.interval = value;
            }
        }

        public MooreMachine(T[] sigma, State[] S, State s0, TransitionFunction<T> delta)//Signatures of Moore machine
        {
            this.sigma = sigma;
            this.S = S;
            this.s0 = s0;
            this.delta = delta;

            this.delay = 0;
            this.interval = 0;
        }

        public void Run()
        {
            State currentState = this.s0;// initializing the starting state

            if (this.delay > 0)
            {
                Thread.Sleep(this.delay);
            }

            while (true)
            {
                currentState.Run();

                if (this.HasInput())
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("\n\tGot input: " + this.GetInput());//get inputs and start process the states

                    State newState = this.delta.GetTransition(currentState, this.GetInput());//getting current state transition for processing of next state on behalf of the current input symbol.
                    currentState = newState != null ? newState : currentState;

                    this.ClearInput();
                }

                Thread.Sleep(this.interval);
            }
        }

        private bool HasInput()
        {
            return this.input != null;
        }

        private T GetInput()
        {
            return this.input;
        }

        private void ClearInput()
        {
            this.input = default(T);
        }

        public void SetInput(T input)
        {
            this.input = input;
        }

        public void Analyze()
        {
            bool deterministic = true;
            LinkedList<Transition<T>> transitions = new LinkedList<Transition<T>>();//initializing Transition objects for transitions id's

            foreach (State s in this.S)
            {
                foreach (T sigma in this.sigma)
                {
                    Console.Write("Checking " + s.Name + " with " + sigma + "... ");
                    /* Checking uniqueness of the computation after getting the transition id's.
                     * the machine goes to one state only at a time for a particular input character
                     */
                    if (this.delta.GetTransition(s, sigma) == null)
                    {
                        deterministic = false;

                        Console.WriteLine("Not found");

                        transitions.AddLast(new Transition<T>(s, sigma, null));
                    }
                    else
                    {
                        Console.WriteLine("Found");
                        Thread.Sleep(this.delay);
                    }
                }
            }

            if (!deterministic)// Checking transitions that can transmit any number of states for a particular input.
            {
                Console.WriteLine("\n\n\tThis Moore-Machine is a partially-specified deterministic finite state machine.");
                Thread.Sleep(this.delay);

                Console.WriteLine("\n\nFollowing transitions are missing:");

                LinkedListNode<Transition<T>> node = transitions.First;

                while (node != null)
                {
                    Transition<T> t = node.Value;

                    Console.WriteLine("(" + t.S.Name + ", \"" + t.Input + "\") -> undefined");

                    node = node.Next;
                }
            }
            else
            {
                Console.WriteLine("This Moore-Machine is a fully-specified deterministic finite state machine.");
            }
        }
    }
}
