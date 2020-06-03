
namespace visusNET.FiniteStateMachine
{
    public struct Transition<T>
    {
        private State s;
        private T input;
        private State sNext;

        public State S
        {
            get { return this.s; }
        }

        public T Input
        {
            get { return this.input; }
        }

        public State SNext
        {
            get { return this.sNext; }
        }

        public Transition(State s, T input, State sNext)
        {
            this.s = s;
            this.input = input;
            this.sNext = sNext;
        }
    }
}
