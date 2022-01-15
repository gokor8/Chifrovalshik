using System;
using System.Collections.Generic;
using System.IO;
using Chifrovalshik.States;

namespace Chifrovalshik
{
    public class FileCryptor
    {
        private string _filePath = Path.GetFullPath(@"..\..\..\") + @"TextFolder\Text";

        private List<State> states;

        private State CurrentState;

        public FileCryptor()
        {
            states = new() {new DecrytpedState(_filePath), new EncryptedState(_filePath) };
            findFileState();
        }

        private void findFileState()
        {
            foreach (var state in states)
            {
                if (File.Exists(_filePath + state.Expansion))
                {
                    CurrentState = state;
                    return;
                }
            }

            CurrentState = new DecrytpedState(_filePath);

            File.Create(_filePath + CurrentState.Expansion).Close();
            File.WriteAllText(_filePath + CurrentState.Expansion, "Шифр");
        } 

        public void ChangeFileType(string password)
        {
            CurrentState.ChangeState(ref CurrentState, password);
        }
    }
}