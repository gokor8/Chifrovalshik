using System.IO;

namespace Chifrovalshik
{
    public abstract class State
    {
        protected readonly string FilePath;
        protected State(string filePath)
        {
            FilePath = filePath;
        }

        public string Expansion { get; protected set; }
        public abstract void ChangeState(ref State state, string password);
    }
}