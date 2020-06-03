/*Generting the list of Transitions functions where delta:Q x E-->Q for the processing between the states.
 */

using System.Collections.Generic;


namespace visusNET.FiniteStateMachine
{
    public class TransitionFunction<T>
    {
        public LinkedList<Transition<T>> transitions;

        public TransitionFunction()
        {
            this.transitions = new LinkedList<Transition<T>>();
        }

        public void AddTransition(State s, T input, State sNext)
        {
            this.transitions.AddLast(new Transition<T>(s, input, sNext));
        }

        public State GetTransition(State s, T input)
        {
            LinkedListNode<Transition<T>> node = this.transitions.First;

            while (node != null)
            {
                Transition<T> t = node.Value;

                if (t.S.Equals(s) && t.Input.Equals(input))
                {
                    return t.SNext;
                }

                node = node.Next;
            }

            return null;
        }
    }
}
