using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInterpretor
{
    public class CommandInterpretor
    {
        public void Run() {
            Console.WriteLine("Welcome to console command interpretor!");
            Console.WriteLine("\tcat [FILENAME] - write file's content");

            bool isExit = false;
            while (isExit) {
                Console.Write("> ");
                string commandText = Console.ReadLine();

                List<string> tokens = TokenizeCommand(commandText);
            }
        }

        private enum State {
            Default,
            InQuotes,
            ReadingWord
        };

        /* tokenize */
        private List<string> TokenizeCommand(string commandText)
        {
            var tokens = new List<string>();
            var state = State.Default;
            var token = "";

            void flushToken()
            {
                tokens.Add(token);
                token = "";
            }

            void addLetterToToken(char c)
            {
                token += c;
            }

            foreach (var c in commandText)
            {
                switch (state)
                {
                    case State.Default:
                        {
                            if (char.IsLetterOrDigit(c))
                            {
                                addLetterToToken(c);
                                state = State.ReadingWord;
                            }
                            else if (c == '=')
                            {
                                token = "=";
                                flushToken();
                            }
                            else if (c == '"')
                            {
                                state = State.InQuotes;
                            }
                            else if (c != ' ')
                            {
                                throw new ArgumentException($"Incorrect syntax: unexpected symbol '{c}'");
                            }
                            break;
                        }

                    case State.InQuotes:
                        {
                            if (c == '"')
                            {
                                flushToken();
                                state = State.Default;
                            }
                            else
                            {
                                addLetterToToken(c);
                            }
                            break;
                        }

                    case State.ReadingWord:
                        {
                            if (c == ' ')
                            {
                                flushToken();
                                state = State.Default;
                            }
                            else if (char.IsLetterOrDigit(c))
                            {
                                addLetterToToken(c);
                            }
                            else if (c == '"')
                            {
                                flushToken();
                                state = State.InQuotes;
                            }
                            else if (c == '=')
                            {
                                flushToken();
                                addLetterToToken(c);
                                flushToken();
                                state = State.Default;
                            }
                            else
                            {
                                throw new ArgumentException($"Incorrect syntax: unexpected symbol '{c}'");
                            }
                            break;
                        }
                }
            }

            if (state == State.InQuotes)
            {
                throw new ArgumentException("Expected closen quote.");
            }
            else if (state == State.ReadingWord)
            {
                flushToken();
            }

            return tokens;
        }
    }
}
