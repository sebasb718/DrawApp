using System;
using System.Collections.Generic;
using Main;

class Command
    {
        Dictionary<char, int> AllowedCharactersAndParams = new Dictionary<char, int>()
        {
            {'C',2},
            {'L',4},
            {'R',4},
            {'B',3},
            {'Q',0}
        };

        public char CommandIdentifier
        {
            get
            {
                return cCommandIdentifier;
            }
        }
        private char cCommandIdentifier;

        public object[] CommandParameters
        {
            get
            {
                return oaCommandParameters;
            }
        }
        private object[] oaCommandParameters;

        public Command(string sCommand)
        {
            try
            {
                string[] sCommandSplit = sCommand.Split(' ');
                if (sCommandSplit[0].Length != 1)
                {
                    throw new Exception("Invalid Entry");
                }
                cCommandIdentifier = Convert.ToChar(sCommandSplit[0]);
                if (!AllowedCharactersAndParams.ContainsKey(cCommandIdentifier))
                {
                    throw new Exception("Invalid command");
                }

                oaCommandParameters = new object[sCommandSplit.Length - 1];
                for (int i = 0; i < sCommandSplit.Length - 1; i++)
                {
                    if (i == 2 && cCommandIdentifier == 'B')
                    {
                        if (sCommandSplit[i].Length != 1)
                        {
                            throw new Exception("Invalid color for Bucket");
                        }
                        else
                        {
                            oaCommandParameters[i] = Convert.ToChar(sCommandSplit[i + 1]);
                            continue;
                        }
                    }
                    int iPartialParameter = 0;
                    try
                    {
                        iPartialParameter = Convert.ToInt32(sCommandSplit[i + 1]);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Parameters are not numbers");
                    }
                    oaCommandParameters[i] = iPartialParameter;
                }

                if (AllowedCharactersAndParams[CommandIdentifier] != CommandParameters.Length)
                {
                    throw new Exception("Invalid number of parameters for the command entered");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
