
namespace visusNET.FiniteStateMachine
{
    public class State
    {
        public delegate void Callback();
        public Callback callback;
        private string name;

        public string Name
        {
            get { return this.name; }
        }

        public State(Callback callback, string name)
        {
            this.callback = callback;
            this.name = name;
        }

        public void Run()
        {
            this.callback();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is State)
            {
                State state = (State)obj;

                return this.callback.GetHashCode().Equals(state.callback.GetHashCode()) && this.name.Equals(state.Name);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
