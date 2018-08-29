using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class TextMessage
    {
        private string code = string.Empty;
        private string messageBody = string.Empty;

        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public string MessageBody
        {
            get
            {
                return messageBody;
            }
            set
            {
                messageBody = value;
            }
        }
    }
}